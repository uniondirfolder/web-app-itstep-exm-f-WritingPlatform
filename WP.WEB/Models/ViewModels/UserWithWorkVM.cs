using System;


namespace WP.WEB.Models.ViewModels
{
    public class UserWithWorkVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string GenreName { get; set; }
        public bool IsDelete { get; set; }
    }
}