
using PAP.DataAccess;
using PAP.Repositories;
using System;
using System.Linq;

namespace PAP.Generators
{
    public class BooksGenerator
    {
        private readonly BookRepository repository;
        private readonly CategoryRepository categoryRepository;
        private readonly GenreRepository genreRepository;
        private readonly PublisherRepository publisherRepository;

        public BooksGenerator()
        {
            repository = new BookRepository();
            categoryRepository = new CategoryRepository();
            genreRepository = new GenreRepository();
            publisherRepository = new PublisherRepository();
        }

        public string Generate()
        {
            if (repository.GetAll().Any())
            {
                return "Error: Ya existen libros registrados.";
            }

            var categories = categoryRepository.GetAll().ToList();
            var genres = genreRepository.GetAll().ToList();
            var publishers = publisherRepository.GetAll().ToList();

            if (!categories.Any())
            {
                return "Error: Primero deben existir categorías.";
            }

            if (!genres.Any())
            {
                return "Error: Primero deben existir géneros.";
            }

            if (!publishers.Any())
            {
                return "Error: Primero deben existir editoriales.";
            }

            for (int i = 1; i <= 10; i++)
            {
                Category category = categories[(i - 1) % categories.Count];
                Genre genre = genres[(i - 1) % genres.Count];
                Publisher publisher = publishers[(i - 1) % publishers.Count];

                Book book = new Book();

                book.Name = "Libro " + i;
                book.Title = "Título de prueba " + i;
                book.Category = category.Name;
                book.Publisher = publisher.Name;
                book.Author = "Autor de prueba " + i;
                book.Description = "Descripción del libro de prueba " + i;

                book.PublicationDate = new DateTime(2020 + (i % 6), 1, 15);
                book.Height = 20m;
                book.Width = 15m;
                book.Price = 5000m + (i * 1000m);

                book.IsBestSeller = i % 2 == 0;
                book.Genre = genre.Name;
                book.IsInStock = true;

                book.CreatedAt = DateTime.Now;
                book.UpdatedAt = DateTime.Now;

                book.PublisherId = publisher.Id;
                book.CategoryId = category.Id;
                book.GenreId = genre.Id;

                book.ISBN = "978000000" + i.ToString("000");
                book.Pages = 100 + (i * 20);
                book.Language = "Español";
                book.CoverImageUrl = "";

                repository.Add(book);
            }

            return "Se generaron 10 libros correctamente.";
        }

        public bool CanGenerate()
        {
            return !repository.GetAll().Any()
                && categoryRepository.GetAll().Any()
                && genreRepository.GetAll().Any()
                && publisherRepository.GetAll().Any();
        }
    }
}
