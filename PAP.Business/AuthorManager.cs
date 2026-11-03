
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class AuthorManager
    {
        private readonly AuthorRepository _repository;

        public AuthorManager()
        {
            _repository = new AuthorRepository();
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _repository.GetAll();
        }

        public Author GetAuthorById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddAuthor(Author author)
        {
            _repository.Add(author);
        }

        public void UpdateAuthor(Author author)
        {
            _repository.Update(author);
        }

        public void DeleteAuthor(int id)
        {
            _repository.Delete(id);
        }
    }
}

