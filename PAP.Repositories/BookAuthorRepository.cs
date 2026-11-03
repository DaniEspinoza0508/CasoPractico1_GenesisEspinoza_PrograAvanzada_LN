using PAP.DataAccess;
using System.Linq;

namespace PAP.Repositories
{
    public class BookAuthorRepository
    {
        private BookStoreDBEntities context;

        public BookAuthorRepository()
        {
            context = new BookStoreDBEntities();
        }

        public void Add(int bookId, int authorId)
        {
            Book book = context.Books.Find(bookId);
            Author author = context.Authors.Find(authorId);

            if (book != null && author != null)
            {
                if (!book.Authors.Any(a => a.Id == authorId))
                {
                    book.Authors.Add(author);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int bookId, int authorId)
        {
            Book book = context.Books.Find(bookId);

            if (book != null)
            {
                Author author = book.Authors
                    .FirstOrDefault(a => a.Id == authorId);

                if (author != null)
                {
                    book.Authors.Remove(author);
                    context.SaveChanges();
                }
            }
        }
    }
}

