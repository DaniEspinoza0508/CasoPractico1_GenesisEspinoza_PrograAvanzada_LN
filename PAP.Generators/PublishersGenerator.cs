
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class PublishersGenerator
    {
        private readonly PublisherRepository repository;

        public PublishersGenerator()
        {
            repository = new PublisherRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen editoriales registradas.";
            }

            for (int i = 1; i <= 10; i++)
            {
                Publisher publisher = new Publisher();

                publisher.Name = "Editorial " + i;
                publisher.Country = "Costa Rica";
                publisher.Website = "https://editorial" + i + ".com";
                publisher.Email = "editorial" + i + "@correo.com";
                publisher.CreatedAt = DateTime.Now;

                repository.Add(publisher);
            }

            return "Se generaron 10 editoriales correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any();
        }
    }
}
