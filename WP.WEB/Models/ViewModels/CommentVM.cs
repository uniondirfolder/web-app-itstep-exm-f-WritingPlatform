

using System.ComponentModel.DataAnnotations;

namespace WP.WEB.Models.ViewModels
{
    public class CommentVM
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Comment { get; set; }

        public int? UserId { get; set; }

        public int? WorkId { get; set; }
    }
}