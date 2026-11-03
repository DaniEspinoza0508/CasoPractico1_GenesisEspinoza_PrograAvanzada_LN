
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class AuthorsGenerator
    {
        private readonly AuthorRepository repository;

        public AuthorsGenerator()
        {
            repository = new AuthorRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen autores registrados.";
            }

            for (int i = 1; i <= 10; i++)
            {
                Author author = new Author();

                author.FirstName = "Nombre " + i;
                author.LastName = "Apellido " + i;
                author.Biography = "Biografía del autor " + i;
                author.BirthDate = new DateTime(1980 + i, 1, 15);
                author.Country = "Costa Rica";
                author.CreatedAt = DateTime.Now;

                repository.Add(author);
            }

            return "Se generaron 10 autores correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any();
        }
    }
}
