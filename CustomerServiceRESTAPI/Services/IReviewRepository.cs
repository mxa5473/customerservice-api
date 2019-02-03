using System;
using System.Collections.Generic;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;

namespace CustomerServiceRESTAPI.Services
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetReviews();
        Review GetReview(int Id);
        void AddReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(Review review);
        bool Save();
    }
}
