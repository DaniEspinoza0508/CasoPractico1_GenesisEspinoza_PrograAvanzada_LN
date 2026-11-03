
using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    public class GenresGenerator
    {
        private readonly GenreRepository repository;

        public GenresGenerator()
        {
            repository = new GenreRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen géneros registrados.";
            }

            for (int i = 1; i <= 10; i++)
            {
                Genre genre = new Genre();

                genre.Name = "Género " + i;
                genre.Description = "Descripción del género " + i;

                repository.Add(genre);
            }

            return "Se generaron 10 géneros correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any();
        }
    }
}
