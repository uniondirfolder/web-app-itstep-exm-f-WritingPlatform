using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP.WEB.Models.ViewModels
{
    public class RatingVM
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public int? WorkId { get; set; }
        public int? UserId { get; set; }
    }
}