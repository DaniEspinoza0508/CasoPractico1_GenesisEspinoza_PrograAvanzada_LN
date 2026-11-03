
using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    public class PurchaseOrderItemsGenerator
    {
        private readonly PurchaseOrderItemRepository repository;
        private readonly PurchaseOrderRepository purchaseOrderRepository;
        private readonly BookRepository bookRepository;

        public PurchaseOrderItemsGenerator()
        {
            repository = new PurchaseOrderItemRepository();
            purchaseOrderRepository = new PurchaseOrderRepository();
            bookRepository = new BookRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen detalles de órdenes de compra.";
            }

            var purchaseOrders = purchaseOrderRepository.GetAll().ToList();
            var books = bookRepository.GetAll().ToList();

            if (!purchaseOrders.Any())
            {
                return "Error: Primero deben existir órdenes de compra.";
            }

            if (!books.Any())
            {
                return "Error: Primero deben existir libros registrados.";
            }

            int total = 0;

            foreach (PurchaseOrder purchaseOrder in purchaseOrders.Take(10))
            {
                Book book = books[total % books.Count];

                PurchaseOrderItem item = new PurchaseOrderItem();

                item.PurchaseOrderId = purchaseOrder.Id;
                item.BookId = book.Id;
                item.Quantity = 5;
                item.UnitCost = book.Price;

                repository.Add(item);

                total++;
            }

            return "Se generaron " + total +
                   " detalles de órdenes de compra correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && purchaseOrderRepository.GetAll().Any()
                && bookRepository.GetAll().Any();
        }
    }
}
