
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class ReviewsGenerator
    {
        private readonly ReviewRepository repository;
        private readonly BookRepository bookRepository;
        private readonly CustomerRepository customerRepository;

        public ReviewsGenerator()
        {
            repository = new ReviewRepository();
            bookRepository = new BookRepository();
            customerRepository = new CustomerRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen reseñas registradas.";
            }

            var books = bookRepository.GetAll().ToList();
            var customers = customerRepository.GetAll().ToList();

            if (!books.Any())
            {
                return "Error: Primero deben existir libros registrados.";
            }

            if (!customers.Any())
            {
                return "Error: Primero deben existir clientes registrados.";
            }

            for (int i = 1; i <= 10; i++)
            {
                Book book = books[(i - 1) % books.Count];
                Customer customer = customers[(i - 1) % customers.Count];

                Review review = new Review();

                review.BookId = book.Id;
                review.CustomerId = customer.Id;
                review.Rating = (byte)((i - 1) % 5 + 1);
                review.Comment = "Reseña de prueba del libro " + i;
                review.CreatedAt = DateTime.Now.AddDays(-i);

                repository.Add(review);
            }

            return "Se generaron 10 reseñas correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && bookRepository.GetAll().Any()
                && customerRepository.GetAll().Any();
        }
    }
}
