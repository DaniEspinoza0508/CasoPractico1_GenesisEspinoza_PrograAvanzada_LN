
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class GenreManager
    {
        private readonly GenreRepository _repository;

        public GenreManager()
        {
            _repository = new GenreRepository();
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _repository.GetAll();
        }

        public Genre GetGenreById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddGenre(Genre genre)
        {
            _repository.Add(genre);
        }

        public void UpdateGenre(Genre genre)
        {
            _repository.Update(genre);
        }

        public void DeleteGenre(int id)
        {
            _repository.Delete(id);
        }
    }
}

