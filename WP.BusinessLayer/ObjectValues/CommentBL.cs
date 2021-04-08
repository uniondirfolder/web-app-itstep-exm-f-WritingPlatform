
namespace WP.BusinessLayer.ObjectValues
{
    public class CommentBL
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int? UserId { get; set; }
        public int? WorkId { get; set; }
    }
}
