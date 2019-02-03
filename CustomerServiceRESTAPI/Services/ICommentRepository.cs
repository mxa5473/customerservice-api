using System;
using System.Collections.Generic;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;

namespace CustomerServiceRESTAPI.Services
{
    public interface ICommentRepository
    {
            IEnumerable<Comment> GetReviews();
            Comment GetComment(int Id);
            void UpdateComment(Comment comment);
            void DeleteComment(Comment comment);
            void Save();
        }
}

