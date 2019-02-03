using System;
using System.Collections.Generic;
using System.Linq;
using CustomerServiceRESTAPI.Entities;
using CustomerServiceRESTAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceRESTAPI.Services
{
    public class CommentRepository : IDBRepository<Comment>
    {
        private Context _context;

        public CommentRepository(Context context)
        {
            _context = context;
        }

        public void Add(Comment comment){
            _context.Comments.Add(comment);
        }

        public Comment Get(int id)
        {
            return _context.Comments.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments.ToList(); 
        }

        public void Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
        }

        public void Update(Comment comment)
        {
            _context.Comments.Update(comment);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0; 
        }
    }
}
