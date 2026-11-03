
using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    public class AddressesGenerator
    {
        private readonly AddressRepository repository;
        private readonly CustomerRepository customerRepository;

        public AddressesGenerator()
        {
            repository = new AddressRepository();
            customerRepository = new CustomerRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen direcciones registradas.";
            }

            var customers = customerRepository.GetAll().ToList();

            if (!customers.Any())
            {
                return "Error: Primero debe generar los clientes.";
            }

            int total = 0;

            foreach (Customer customer in customers.Take(10))
            {
                Address address = new Address();

                address.CustomerId = customer.Id;
                address.AddressType = "Casa";
                address.Street = "Calle " + (total + 1);
                address.City = "San José";
                address.State = "San José";
                address.PostalCode = "10101";
                address.Country = "Costa Rica";
                address.IsDefault = true;

                repository.Add(address);

                total++;
            }

            return "Se generaron " + total +
                   " direcciones correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && customerRepository.GetAll().Any();
        }
    }
}
