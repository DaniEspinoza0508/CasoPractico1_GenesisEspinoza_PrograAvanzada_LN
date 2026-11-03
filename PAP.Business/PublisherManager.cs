
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class PublisherManager
    {
        private readonly PublisherRepository _repository;

        public PublisherManager()
        {
            _repository = new PublisherRepository();
        }

        public IEnumerable<Publisher> GetAllPublishers()
        {
            return _repository.GetAll();
        }

        public Publisher GetPublisherById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddPublisher(Publisher publisher)
        {
            _repository.Add(publisher);
        }

        public void UpdatePublisher(Publisher publisher)
        {
            _repository.Update(publisher);
        }

        public void DeletePublisher(int id)
        {
            _repository.Delete(id);
        }
    }
}
