
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class ShoppingCartsGenerator
    {
        private readonly ShoppingCartRepository repository;
        private readonly CustomerRepository customerRepository;

        public ShoppingCartsGenerator()
        {
            repository = new ShoppingCartRepository();
            customerRepository = new CustomerRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen carritos de compras registrados.";
            }

            var customers = customerRepository.GetAll().ToList();

            if (!customers.Any())
            {
                return "Error: Primero deben existir clientes registrados.";
            }

            int total = 0;

            foreach (Customer customer in customers.Take(10))
            {
                ShoppingCart cart = new ShoppingCart();

                cart.CustomerId = customer.Id;
                cart.CreatedAt = DateTime.Now;
                cart.UpdatedAt = DateTime.Now;

                repository.Add(cart);

                total++;
            }

            return "Se generaron " + total +
                   " carritos de compras correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && customerRepository.GetAll().Any();
        }
    }
}
