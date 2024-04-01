using API_MongoDB.Models;
using API_MongoDB.Models.Database;
using MongoDB.Driver;

namespace API_MongoDB.Services.Categories
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoriesService(ITCHStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _categories = database.GetCollection<Category>("categories");
        }
        public Category Create(Category category)
        {
            _categories.InsertOne(category);
            return category;
        }

        public List<Category> Get()
        {
            return _categories.Find(s => true).ToList();
        }

        public Category GetById(string id)
        {
            return _categories.Find(s => s.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _categories.DeleteOne(s => s.Id == id);
        }

        public void Update(string id, Category category)
        {
            _categories.ReplaceOne(s => s.Id == id, category);
        }
    }
}
