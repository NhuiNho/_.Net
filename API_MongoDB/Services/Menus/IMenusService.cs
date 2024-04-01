using API_MongoDB.Models;

namespace API_MongoDB.Services.Menus
{
    public interface IMenusService
    {
        List<Menu> Get();
        Menu GetById(string id);
        Menu Create(Menu menu);
        void Update(string id, Menu menu);
        void Remove(string id);

    }
}
