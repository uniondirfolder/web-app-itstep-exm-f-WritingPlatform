

namespace WP.WEB.Models.ViewModels
{
    public class UserWithCommentVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public int? WorkId { get; set; }
        public int Rank { get; set; }
        public bool IsDelete { get; set; }
    }
}