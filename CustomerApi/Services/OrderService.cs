using System;
using CustomerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CustomerApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _ordersCollection;

        public OrderService(IOptions<DatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dbSettings.Value.DatabaseName);

            _ordersCollection = mongoDatabase.GetCollection<Order>(
                dbSettings.Value.OrdersCollectionName);
        }

        //public async Task<List<Order>> GetAsync() =>
        //await _ordersCollection.Find(_ => true).ToListAsync();

        public async Task<IEnumerable<Order>> GetAsync()
        {
            // Define the pipeline for the aggregation query
            var pipeline = new BsonDocument[]
            {
              new BsonDocument("$lookup", new BsonDocument
              {
                { "from", "Customer" },
                { "localField", "CustomerId" },
                { "foreignField", "_id" },
                { "as", "order_customer" }
              }),
              new BsonDocument("$unwind", "$order_customer"),
              new BsonDocument("$project", new BsonDocument
              {
                { "_id", 1 },
                { "CustomerId", 1},
                { "OrderDate",1 },
                { "Total",1 },
                { "FirstName", "$order_customer.FirstName" },
                { "LastName", "$order_customer.LastName" },
                { "Phone", "$order_customer.Phone" },
                { "Street", "$order_customer.Street" },
                { "ZipCode", "$order_customer.ZipCode" },
              })
            };

            var results = await _ordersCollection.Aggregate<Order>(pipeline).ToListAsync();

            return results;
        }

        public async Task<Order?> GetAsync(string id) =>
            await _ordersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Order newOrder) =>
            await _ordersCollection.InsertOneAsync(newOrder);

        public async Task UpdateAsync(string id, Order updatedBook) =>
            await _ordersCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _ordersCollection.DeleteOneAsync(x => x.Id == id);
    }
}

