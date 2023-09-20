using Domains.Entities;

namespace Services.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(Guid id);
        Category Create(Category category);
        Category Update(Category category);
        Category Delete(Guid id);
    }
}
