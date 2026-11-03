
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class CartItemsGenerator
    {
        private readonly CartItemRepository repository;
        private readonly ShoppingCartRepository cartRepository;
        private readonly BookRepository bookRepository;

        public CartItemsGenerator()
        {
            repository = new CartItemRepository();
            cartRepository = new ShoppingCartRepository();
            bookRepository = new BookRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen productos en los carritos.";
            }

            var carts = cartRepository.GetAll().ToList();
            var books = bookRepository.GetAll().ToList();

            if (!carts.Any())
            {
                return "Error: Primero deben existir carritos de compras.";
            }

            if (!books.Any())
            {
                return "Error: Primero deben existir libros registrados.";
            }

            int total = 0;

            foreach (ShoppingCart cart in carts.Take(10))
            {
                Book book = books[total % books.Count];

                CartItem item = new CartItem();

                item.CartId = cart.Id;
                item.BookId = book.Id;
                item.Quantity = 2;
                item.AddedAt = DateTime.Now;

                repository.Add(item);

                total++;
            }

            return "Se generaron " + total +
                   " productos en los carritos correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && cartRepository.GetAll().Any()
                && bookRepository.GetAll().Any();
        }
    }
}
