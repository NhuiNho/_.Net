using API_MongoDB.Models;

namespace API_MongoDB.Services.Categories
{
    public interface ICategoriesService
    {
        List<Category> Get();
        Category GetById(string id);
        Category Create(Category category);
        void Update(string id, Category category);
        void Remove(string id);

    }
}
