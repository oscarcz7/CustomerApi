using System;
using CustomerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CustomerApi.Services
{
    public class ProductService: IProductService
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductService(IOptions<DatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dbSettings.Value.DatabaseName);

            _productsCollection = mongoDatabase.GetCollection<Product>(
                dbSettings.Value.ProductsCollectionName);
        }

        //public async Task<List<Product>> GetAsync() =>
        //await _productsCollection.Find(_ => true).ToListAsync();

        public async Task<IEnumerable<Product>> GetAsync()
        {
            // Define the pipeline for the aggregation query
            var pipeline = new BsonDocument[]
            {
              new BsonDocument("$lookup", new BsonDocument
              {
                { "from", "Category" },
                { "localField", "CategoryId" },
                { "foreignField", "_id" },
                { "as", "product_category" }
              }),
              new BsonDocument("$unwind", "$product_category"),
              new BsonDocument("$project", new BsonDocument
              {
                { "_id", 1 },
                { "CategoryId", 1},
                { "ProductName",1 },
                { "CategoryName", "$product_category.CategoryName" }
              })
            };

            var results = await _productsCollection.Aggregate<Product>(pipeline).ToListAsync();

            return results;
        }


        public async Task<Product?> GetById(string id) =>
            await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product newProduct) =>
            await _productsCollection.InsertOneAsync(newProduct);

        public async Task UpdateAsync(string id, Product updatedProduct) =>
            await _productsCollection.ReplaceOneAsync(x => x.Id == id, updatedProduct);

        public async Task RemoveAsync(string id) =>
            await _productsCollection.DeleteOneAsync(x => x.Id == id);
    }
}

