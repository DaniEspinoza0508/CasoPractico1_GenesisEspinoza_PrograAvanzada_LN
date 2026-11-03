
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class CustomersGenerator
    {
        private readonly CustomerRepository repository;

        public CustomersGenerator()
        {
            repository = new CustomerRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen clientes registrados.";
            }

            for (int i = 1; i <= 10; i++)
            {
                Customer customer = new Customer();

                customer.FirstName = "Cliente " + i;
                customer.LastName = "Apellido " + i;
                customer.Email = "cliente" + i + "@correo.com";
                customer.PasswordHash = "HASH_PRUEBA_" + i;
                customer.Phone = "888800" + i.ToString("00");
                customer.BirthDate = new DateTime(1990 + i, 5, 10);
                customer.IsActive = true;
                customer.CreatedAt = DateTime.Now;
                customer.UpdatedAt = DateTime.Now;

                repository.Add(customer);
            }

            return "Se generaron 10 clientes correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any();
        }
    }
}
