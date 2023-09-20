using Domains.Entities;
using Services.Repositories;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Category Create(Category category)
        {
            throw new NotImplementedException();
        }

        public Category Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories() => _categoryRepository.GetAll();



        public Category GetCategory(Guid id)
        {
            throw new NotImplementedException();
        }

        public Category Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
