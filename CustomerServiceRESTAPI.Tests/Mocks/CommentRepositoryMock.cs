using System;
using System.Linq;
using CustomerServiceRESTAPI.Services;
using CustomerServiceRESTAPI.Entities;
using System.Collections.Generic;

namespace CustomerServiceRESTAPI.Tests.Mocks
{
    public class CommentRepositoryMock : IDBRepository<Comment>
    {
        public static Comment TestComment = new Comment()
        {
            Content = "test comment content",
            Id = 800, 
            AgentId = HRServiceMock.TestAgent.Id,
            ClientId = ClientRepositoryMock.TestClient.Id
        };

        List<Comment> _comments = new List<Comment>() {
            TestComment
        };

        public void Add(Comment comment)
        {
            _comments.Add(comment);
        }

        public Comment Get(int id)
        {
            return _comments.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _comments;
        }

        public void Update(Comment comment)
        {
            var commentIndex = _comments.FindIndex(c => c.Id == comment.Id);
            if (commentIndex == -1) return;

            _comments[commentIndex] = comment;
        }

        public void Delete(Comment comment)
        {
            var commentIndex = _comments.FindIndex(c => c.Id == comment.Id);
            if (commentIndex == -1) return;

            _comments.RemoveAt(commentIndex);
        }

        public bool Save()
        {
            return true;
        }


    }
}
