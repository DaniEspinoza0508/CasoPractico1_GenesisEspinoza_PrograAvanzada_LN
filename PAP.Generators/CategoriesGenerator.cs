
using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    public class CategoriesGenerator
    {
        private readonly CategoryRepository repository;

        public CategoriesGenerator()
        {
            repository = new CategoryRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen categorías registradas.";
            }

            for (int i = 1; i <= 10; i++)
            {
                Category category = new Category();

                category.Name = "Categoría " + i;
                category.Description = "Descripción de la categoría " + i;

                repository.Add(category);
            }

            return "Se generaron 10 categorías correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any();
        }
    }
}
