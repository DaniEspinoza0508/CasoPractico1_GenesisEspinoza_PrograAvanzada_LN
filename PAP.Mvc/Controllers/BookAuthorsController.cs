using PAP.DataAccess;
using PAP.Repositories;
using System.Linq;

namespace PAP.Generators
{
    public class BookAuthorsGenerator
    {
        private readonly BookAuthorRepository repository;
        private readonly BookRepository bookRepository;
        private readonly AuthorRepository authorRepository;

        public BookAuthorsGenerator()
        {
            repository = new BookAuthorRepository();
            bookRepository = new BookRepository();
            authorRepository = new AuthorRepository();
        }

        public string Generate()
        {
            var books = bookRepository.GetAll().ToList();
            var authors = authorRepository.GetAll().ToList();

            if (!books.Any())
            {
                return "Error: Primero deben existir libros registrados.";
            }

            if (!authors.Any())
            {
                return "Error: Primero deben existir autores registrados.";
            }

            bool existingRelations = books.Any(
                b => b.Authors.Any());

            if (existingRelations)
            {
                return "Error: Ya existen relaciones entre libros y autores.";
            }

            int total = 0;

            foreach (Book book in books.Take(10))
            {
                Author author = authors[total % authors.Count];

                repository.Add(book.Id, author.Id);

                total++;
            }

            return "Se generaron " + total +
                   " relaciones entre libros y autores correctamente.";
        }

        public bool CanGenerate()
        {
            var books = bookRepository.GetAll().ToList();
            var authors = authorRepository.GetAll().ToList();

            if (!books.Any() || !authors.Any())
            {
                return false;
            }

            return !books.Any(b => b.Authors.Any());
        }
    }
}

s