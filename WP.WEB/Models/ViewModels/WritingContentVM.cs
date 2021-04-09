
using System.Collections.Generic;


namespace WP.WEB.Models.ViewModels
{
    public class WritingContentVM
    {
        public IEnumerable<char> Content { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}