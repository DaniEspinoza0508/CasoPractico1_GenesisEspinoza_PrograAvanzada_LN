
using PAP.DataAccess;
using PAP.Repositories;
using System.Collections.Generic;

namespace PAP.Business
{
    public class ReviewManager
    {
        private readonly ReviewRepository _repository;

        public ReviewManager()
        {
            _repository = new ReviewRepository();
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return _repository.GetAll();
        }

        public Review GetReviewById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddReview(Review review)
        {
            _repository.Add(review);
        }

        public void UpdateReview(Review review)
        {
            _repository.Update(review);
        }

        public void DeleteReview(int id)
        {
            _repository.Delete(id);
        }
    }
}
