
using CustomerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CustomerApi.Services;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;

    public CategoryService(
        IOptions<DatabaseSettings> dbSettings)
    {

        var mongoClient = new MongoClient(
            dbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbSettings.Value.DatabaseName);

        _categoryCollection = mongoDatabase.GetCollection<Category>(
            dbSettings.Value.CategoriesCollectionName);
    }

    public async Task<IEnumerable<Category>> GetAsync() =>
        await _categoryCollection.Find(_ => true).ToListAsync();

    public async Task<Category?> GetAsync(string id) =>
        await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Category newCategory) =>
        await _categoryCollection.InsertOneAsync(newCategory);

    public async Task UpdateAsync(string id, Category updatedCategory) =>
        await _categoryCollection.ReplaceOneAsync(x => x.Id == id, updatedCategory);

    public async Task RemoveAsync(string id) =>
        await _categoryCollection.DeleteOneAsync(x => x.Id == id);
}