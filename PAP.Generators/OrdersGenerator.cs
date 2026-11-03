
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class OrdersGenerator
    {
        private readonly OrderRepository repository;
        private readonly CustomerRepository customerRepository;
        private readonly AddressRepository addressRepository;

        public OrdersGenerator()
        {
            repository = new OrderRepository();
            customerRepository = new CustomerRepository();
            addressRepository = new AddressRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen pedidos registrados.";
            }

            var customers = customerRepository.GetAll().ToList();
            var addresses = addressRepository.GetAll().ToList();

            if (!customers.Any())
            {
                return "Error: Primero deben existir clientes registrados.";
            }

            if (!addresses.Any())
            {
                return "Error: Primero deben existir direcciones registradas.";
            }

            for (int i = 1; i <= 10; i++)
            {
                Customer customer = customers[(i - 1) % customers.Count];

                Address address = addresses
                    .FirstOrDefault(a => a.CustomerId == customer.Id);

                Order order = new Order();

                order.CustomerId = customer.Id;
                order.ShippingAddressId = address == null
                    ? (int?)null
                    : address.Id;

                order.OrderDate = DateTime.Now.AddDays(-i);
                order.Status = "Pending";
                order.Subtotal = 10000m + (i * 1000m);
                order.TaxAmount = order.Subtotal * 0.13m;
                order.ShippingCost = 1500m;

                order.Total = order.Subtotal
                    + order.TaxAmount
                    + order.ShippingCost;

                order.Notes = "Pedido de prueba " + i;
                order.UpdatedAt = DateTime.Now;

                repository.Add(order);
            }

            return "Se generaron 10 pedidos correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && customerRepository.GetAll().Any()
                && addressRepository.GetAll().Any();
        }
    }
}
