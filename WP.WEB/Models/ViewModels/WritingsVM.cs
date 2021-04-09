
using System.Collections.Generic;


namespace WP.WEB.Models.ViewModels
{
    public class WritingsVM
    {
        public IEnumerable<UserWithWorkVM> Writings { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}