using System;
using CustomerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CustomerApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customersCollection;

        public CustomerService(IOptions<DatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dbSettings.Value.DatabaseName);

            _customersCollection = mongoDatabase.GetCollection<Customer>(
                dbSettings.Value.CustomersCollectionName);
        }

        public async Task<List<Customer>> GetAsync() =>
        await _customersCollection.Find(_ => true).ToListAsync();

        public async Task<Customer?> GetAsync(string id) =>
            await _customersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Customer newCustomer) =>
            await _customersCollection.InsertOneAsync(newCustomer);

        public async Task UpdateAsync(string id, Customer updatedCustomer) =>
            await _customersCollection.ReplaceOneAsync(x => x.Id == id, updatedCustomer);

        public async Task RemoveAsync(string id) =>
            await _customersCollection.DeleteOneAsync(x => x.Id == id);
    }
}

