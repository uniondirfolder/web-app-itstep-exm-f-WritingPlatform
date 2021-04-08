

using System.Collections.Generic;
using WP.BusinessLayer.ObjectValues;

namespace WP.BusinessLayer.Interfaces
{
    public interface ICommentBL
    {
        void Create(CommentBL comment);
        void Update(CommentBL comment);
        CommentBL GetComment(int id);
        IEnumerable<CommentBL> GetComments();
        void DeleteComment(int id);
        void Dispose();
    }
}
