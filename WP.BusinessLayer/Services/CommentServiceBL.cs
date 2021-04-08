

using System.Collections.Generic;
using WP.BusinessLayer.Interfaces;
using WP.BusinessLayer.ObjectValues;
using WP.DataLayer.Entities;
using WP.DataLayer.UnitOfWork;

namespace WP.BusinessLayer.Services
{
    public class CommentServiceBL : ICommentBL
    {
        private IUnitOfWork _db;

        public CommentServiceBL(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        public void Create(CommentBL comment)
        {
            var item = new Comment() { Body = comment.Comment, UserId = comment.UserId, WorkId = comment.WorkId };
            _db.UowRepositoryComments.Create(item);
            _db.Save();
        }

        public void DeleteComment(int id)
        {
            _db.UowRepositoryComments.Delete(id);
            _db.Save();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public CommentBL GetComment(int id)
        {
            return AutoMapperBL<Comment, CommentBL>.Map(_db.UowRepositoryComments.Get, id);
        }

        public IEnumerable<CommentBL> GetComments()
        {
            return AutoMapperBL<IEnumerable<Comment>, List<CommentBL>>.Map(_db.UowRepositoryComments.GetAll());
        }

        public void Update(CommentBL comment)
        {
            var item = AutoMapperBL<CommentBL, Comment>.Map(comment);
            _db.UowRepositoryComments.Update(item);
            _db.Save();
        }
    }
}
