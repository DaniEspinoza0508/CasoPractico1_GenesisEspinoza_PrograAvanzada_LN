
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class InventoryGenerator
    {
        private readonly InventoryRepository repository;
        private readonly BookRepository bookRepository;

        public InventoryGenerator()
        {
            repository = new InventoryRepository();
            bookRepository = new BookRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen registros de inventario.";
            }

            var books = bookRepository.GetAll().ToList();

            if (!books.Any())
            {
                return "Error: Primero deben existir libros registrados.";
            }

            int total = 0;

            foreach (Book book in books.Take(10))
            {
                Inventory inventory = new Inventory();

                inventory.BookId = book.Id;
                inventory.QuantityInStock = 20;
                inventory.ReorderLevel = 5;
                inventory.LastRestockedAt = DateTime.Now;

                repository.Add(inventory);

                total++;
            }

            return "Se generaron " + total +
                   " registros de inventario correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && bookRepository.GetAll().Any();
        }
    }
}
