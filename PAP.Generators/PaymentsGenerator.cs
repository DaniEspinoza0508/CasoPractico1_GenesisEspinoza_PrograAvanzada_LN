
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class PaymentsGenerator
    {
        private readonly PaymentRepository repository;
        private readonly OrderRepository orderRepository;

        public PaymentsGenerator()
        {
            repository = new PaymentRepository();
            orderRepository = new OrderRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen pagos registrados.";
            }

            var orders = orderRepository.GetAll().ToList();

            if (!orders.Any())
            {
                return "Error: Primero deben existir pedidos registrados.";
            }

            int total = 0;

            foreach (Order order in orders.Take(10))
            {
                Payment payment = new Payment();

                payment.OrderId = order.Id;
                payment.PaymentMethod = "Card";
                payment.Amount = order.Total;
                payment.PaymentDate = DateTime.Now;
                payment.Status = "Completed";
                payment.TransactionRef = "PAGO-" + order.Id;

                repository.Add(payment);

                total++;
            }

            return "Se generaron " + total +
                   " pagos correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && orderRepository.GetAll().Any();
        }
    }
}
