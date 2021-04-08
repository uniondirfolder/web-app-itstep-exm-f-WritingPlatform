
using System;

namespace WP.BusinessLayer.ObjectValues
{
    public class WorkBL
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string Content { get; set; }
        public int GenreId { get; set; }
        public int UserId { get; set; }
    }
}
