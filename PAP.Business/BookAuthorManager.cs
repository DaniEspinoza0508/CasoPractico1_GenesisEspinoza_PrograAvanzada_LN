
using PAP.Repositories;

namespace PAP.Business
{
    public class BookAuthorManager
    {
        private readonly BookAuthorRepository _repository;

        public BookAuthorManager()
        {
            _repository = new BookAuthorRepository();
        }

        public void AddAuthorToBook(int bookId, int authorId)
        {
            _repository.Add(bookId, authorId);
        }

        public void RemoveAuthorFromBook(int bookId, int authorId)
        {
            _repository.Delete(bookId, authorId);
        }
    }
}

