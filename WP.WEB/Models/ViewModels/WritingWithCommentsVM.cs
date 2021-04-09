using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP.WEB.Models.ViewModels
{
    public class WritingWithCommentsVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
        public IEnumerable<UserWithCommentVM> Comments { get; set; }
        public WritingWithCommentsVM(UserWithWorkVM user, double rating,
            IEnumerable<UserWithCommentVM> comments)
        {
            Id = user.Id;
            Rating = rating;
            Comments = comments;
            UserId = user.Id;
            Title = user.UserName;
            Genre = user.GenreName;
            Content = user.Content;
        }
        public WritingWithCommentsVM(
            int writingId,
            double rating,
            IEnumerable<UserWithCommentVM> comments, int userId, string title, string genre, string content)
        {
            Id = writingId;
            Rating = rating;
            Comments = comments;
            UserId = userId;
            Title = title;
            Genre = genre;
            Content = content;
        }
    }
}