using System;
using System.Collections.Generic;
using System.Linq;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceRESTAPI.Services
{
    public class ReviewRepository : IDBRepository<Review>
    {
        private Context _context;

        public ReviewRepository(Context context)
        {
            _context = context;
        }

        public void Add(Review review)
        {
            _context.Add(review);
        }

        public Review Get(int Id)
        {
            return _context.Reviews.Include(r => r.Client).FirstOrDefault(t => t.Id == Id);
        }

        public IEnumerable<Review> GetAll()
        {
            return _context.Reviews.Include(r => r.Client).OrderBy(t => t.DateCreated).ToList();
        }

        public void Update(Review review)
        {
            _context.Update(review);
        }

        public void Delete(Review review)
        {
            _context.Remove(review);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
