using API_MongoDB.Models;
using API_MongoDB.Models.Database;
using MongoDB.Driver;

namespace API_MongoDB.Services.Menus
{
    public class MenusService : IMenusService
    {
        private readonly IMongoCollection<Menu> _menus;

        public MenusService(ITCHStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _menus = database.GetCollection<Menu>("menus");
        }
        public Menu Create(Menu menu)
        {
            _menus.InsertOne(menu);
            return menu;
        }

        public List<Menu> Get()
        {
            return _menus.Find(s => true).ToList();
        }

        public Menu GetById(string id)
        {
            return _menus.Find(s => s.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _menus.DeleteOne(s => s.Id == id);
        }

        public void Update(string id, Menu menu)
        {
            _menus.ReplaceOne(s => s.Id == id, menu);
        }
    }
}
