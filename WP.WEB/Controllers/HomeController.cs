

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WP.BusinessLayer.Interfaces;
using WP.BusinessLayer.ObjectValues;
using WP.BusinessLayer.Services;
using WP.WEB.Models;
using WP.WEB.Models.ViewModels;


namespace WP.WEB.Controllers
{
    public class HomeController : Controller
    {
        #region Fld & Ctor
        private readonly IUserBL _userBL;
        private readonly IWorkBL _workBL;
        private readonly IGenreBL _genreBL;
        private readonly IRatingBL _ratingBL;
        private readonly ICommentBL _commentBL;

        public HomeController(IUserBL userBL, IWorkBL workBL, IGenreBL genreBL, IRatingBL ratingBL, ICommentBL commentBL)
        {
            this._userBL = userBL;
            this._workBL = workBL;
            this._genreBL = genreBL;
            this._ratingBL = ratingBL;
            this._commentBL = commentBL;
        }
        #endregion
        [Authorize]
        public ActionResult Index(int page = 1)
        {
            int pageSize = 10;
            var writings = _workBL.GetWorks().Join(_userBL.GetUsers(),
                w => w.UserId,
                u => u.Id, (w, u) => new { Id = w.Id, Name = w.Title, DateOfPublication = w.DateOfPublication, Content = w.Content, IsDelete = u.IsDelete, Username = u.Login, GenreId = w.GenreId }).Join(_genreBL.GetGenres(),
                w => w.GenreId,
                g => g.Id, (w, g) => new UserWithWorkVM { Id = w.Id, Name = w.Name, DateOfPublication = w.DateOfPublication.Date, Content = w.Content, UserName = w.Username, GenreName = g.Name, IsDelete = w.IsDelete }).ToList();

            IEnumerable<UserWithWorkVM> writingsPerPages = writings
                .Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = writings.Count
            };
            WritingsVM wvm = new WritingsVM()
            {
                PageInfo = pageInfo,
                Writings = writingsPerPages
            };
            return View(wvm);
        }

        public ActionResult In() 
        {
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult Id(int id)
        {
            var writing = _workBL.GetWorks().Join(_userBL.GetUsers(),
                 w => w.UserId,
                 u => u.Id, (w, u) => new { Id = w.Id, Name = w.Title, DateOfPublication = w.DateOfPublication, Content = w.Content, IsDelete = u.IsDelete, Username = u.Login, GenreId = w.GenreId }).Join(_genreBL.GetGenres(),
                 w => w.GenreId,
                 g => g.Id, (w, g) => new UserWithWorkVM { Id = w.Id, Name = w.Name, DateOfPublication = w.DateOfPublication.Date, Content = w.Content, UserName = w.Username, GenreName = g.Name, IsDelete = w.IsDelete }).FirstOrDefault(item => item.Id == id);
            if (writing == null)
            {
                return HttpNotFound();
            }
            var ratings = _ratingBL.GetRatings().Where(q=>q.WorkId==writing.Id).ToList();
            
            double rating = ratings.Average(q=>q.Rank);
            
            var comments = _commentBL.GetComments().Join(_workBL.GetWorks(),
                            c => c.WorkId,
                            w => w.Id, (c, w) => new { Id = c.Id, Comment = c.Comment, UserId = c.UserId, WorkId = c.WorkId }).Join(_userBL.GetUsers(),
                            c => c.UserId,
                            u => u.Id, (c, u) => new UserWithCommentVM { Id = c.Id, UserName = u.Login, Comment = c.Comment, WorkId = c.WorkId, IsDelete = u.IsDelete }).ToList();
            var result = new WritingWithCommentsVM(writing, rating, comments);
            return View(result);
        }

        [HttpGet]
        public ActionResult Read(int id, int page = 1)
        {
            int pageSize = 2800;
            var writing = _workBL.GetWorks().Join(_userBL.GetUsers(),
                 w => w.UserId,
                 u => u.Id, (w, u) => new { Id = w.Id, Name = w.Title, DateOfPublication = w.DateOfPublication, Content = w.Content, IsDelete = u.IsDelete, Username = u.Login, GenreId = w.GenreId }).Join(_genreBL.GetGenres(),
                 w => w.GenreId,
                 g => g.Id, (w, g) => new UserWithWorkVM { Id = w.Id, Name = w.Name, DateOfPublication = w.DateOfPublication.Date, Content = w.Content, UserName = w.Username, GenreName = g.Name, IsDelete = w.IsDelete }).FirstOrDefault(item => item.Id == id);
            var content = writing.Content.ToCharArray();
            var contentPerPages = content.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = content.Length
            };
            WritingContentVM wcvm = new WritingContentVM()
            {
                PageInfo = pageInfo,
                Content = contentPerPages
            };
            return View(wcvm);
        }
    }
}