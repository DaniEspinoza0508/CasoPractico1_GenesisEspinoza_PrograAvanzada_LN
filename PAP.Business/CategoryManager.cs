
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class CategoryManager
    {
        private readonly CategoryRepository _repository;

        public CategoryManager()
        {
            _repository = new CategoryRepository();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _repository.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddCategory(Category category)
        {
            _repository.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            _repository.Update(category);
        }

        public void DeleteCategory(int id)
        {
            _repository.Delete(id);
        }
    }
}

