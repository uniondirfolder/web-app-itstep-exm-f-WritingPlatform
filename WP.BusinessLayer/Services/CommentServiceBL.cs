

using System.Collections.Generic;
using WP.BusinessLayer.Interfaces;
using WP.BusinessLayer.ObjectValues;
using WP.DataLayer.Entities;
using WP.DataLayer.UnitOfWork;

namespace WP.BusinessLayer.Services
{
    public class CommentServiceBL : ABaseServiceBL, ICommentBL
    {
        public CommentServiceBL(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public void Create(CommentBL comment)
        {
            if (null == comment) return;

            var item = new Comment() { Body = comment.Comment, UserId = comment.UserId, WorkId = comment.WorkId };
            Dbcontext.UowRepositoryComments.Create(item);
            Dbcontext.Save();
        }

        public void DeleteComment(int id)
        {
            if (0 >= id) return;

            Dbcontext.UowRepositoryComments.Delete(id);
            Dbcontext.Save();
        }

        public void Dispose()
        {
            Dbcontext.Dispose();
        }

        public CommentBL GetComment(int id)
        {
            if (0 >= id) return new CommentBL();
            
            return AutoMapperBL<Comment, CommentBL>.Map(Dbcontext.UowRepositoryComments.Get, id);
        }

        public IEnumerable<CommentBL> GetComments()
        {
            return AutoMapperBL<IEnumerable<Comment>, List<CommentBL>>.Map(Dbcontext.UowRepositoryComments.GetAll());
        }

        public void Update(CommentBL comment)
        {
            if (null == comment) return;

            var item = AutoMapperBL<CommentBL, Comment>.Map(comment);
            Dbcontext.UowRepositoryComments.Update(item);
            Dbcontext.Save();
        }
    }
}
