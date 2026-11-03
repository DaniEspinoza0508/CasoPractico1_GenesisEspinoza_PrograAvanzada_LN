
using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    public class OrderItemsGenerator
    {
        private readonly OrderItemRepository repository;
        private readonly OrderRepository orderRepository;
        private readonly BookRepository bookRepository;

        public OrderItemsGenerator()
        {
            repository = new OrderItemRepository();
            orderRepository = new OrderRepository();
            bookRepository = new BookRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen detalles de pedidos registrados.";
            }

            var orders = orderRepository.GetAll().ToList();
            var books = bookRepository.GetAll().ToList();

            if (!orders.Any())
            {
                return "Error: Primero deben existir pedidos registrados.";
            }

            if (!books.Any())
            {
                return "Error: Primero deben existir libros registrados.";
            }

            int total = 0;

            foreach (Order order in orders.Take(10))
            {
                Book book = books[total % books.Count];

                OrderItem item = new OrderItem();

                item.OrderId = order.Id;
                item.BookId = book.Id;
                item.Quantity = 2;
                item.UnitPrice = book.Price;
                item.Discount = 0m;

                repository.Add(item);

                total++;
            }

            return "Se generaron " + total +
                   " detalles de pedidos correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && orderRepository.GetAll().Any()
                && bookRepository.GetAll().Any();
        }
    }
}
