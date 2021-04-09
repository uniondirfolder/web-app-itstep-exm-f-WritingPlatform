using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WP.BusinessLayer.Interfaces;
using WP.BusinessLayer.ObjectValues;
using WP.BusinessLayer.Services;
using WP.WEB.Models.ViewModels;

namespace WP.WEB.Controllers
{
    public class CompositionController : Controller
    {
        #region Fld & Ctor
        IUserBL userBL;
        IWorkBL workBL;
        IGenreBL genreBL;
        IRatingBL ratingBL;
        ICommentBL commentBL;

        public CompositionController(IUserBL userBL, IWorkBL workBL, IGenreBL genreBL, IRatingBL ratingBL, ICommentBL commentBL)
        {
            this.userBL = userBL;
            this.workBL = workBL;
            this.genreBL = genreBL;
            this.ratingBL = ratingBL;
            this.commentBL = commentBL;
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region HttpPost
        [HttpPost]
        public ActionResult Index(string filter)
        {
            ViewBag.filter = filter;
            return View();
        }

        [HttpPost]
        public ActionResult Create(WorkVM model)
        {
            HttpCookie cookieReq = Request.Cookies["Localhost cookie"];

            int ids;
            if (null != cookieReq)
            {
                ids = Convert.ToInt32(cookieReq["ids"]);
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToActionPermanent("Index", "Works");
            }

            model.UserId = ids;
            model.DateOfPublication = DateTime.Now;
            if (ModelState.IsValid)
            {
                WorkBL newWork = AutoMapperBL<WorkVM, WorkBL>.Map(model);

                workBL.Create(newWork);
                return RedirectToActionPermanent("Index", "Works");
            }
            return View();

        }

        [HttpPost]
        public ActionResult Update(WorkVM user)
        {
            WorkBL newUser = AutoMapperBL<WorkVM, WorkBL>.Map(user);
            workBL.Update(newUser);
            return RedirectToActionPermanent("Index", "Works");
        }

        [HttpPost]
        public ActionResult Details(WorkVM user)
        {
            HttpCookie cookieReq = Request.Cookies["Localhost cookie"];

            int ids = 0;
            if (cookieReq != null)
            {
                ids = Convert.ToInt32(cookieReq["ids"]);
            }

            WorkBL newUser = AutoMapperBL<WorkVM, WorkBL>.Map(user);

            CommentBL newComment = new CommentBL();

            newComment.Comment = newUser.Title;
            newComment.WorkId = newUser.Id;
            newComment.UserId = ids;


            return RedirectToActionPermanent("Index", "Works");
        }

        #endregion

        public ActionResult PersonalProfile()
        {
            return View();
        }
        public ActionResult GetWorks(string filter = null)
        {

            var workList = workBL.GetWorks().Join(userBL.GetUsers(),
                w => w.UserId,
                u => u.Id, (w, u) => new { Id = w.Id, Name = w.Title, DateOfPublication = w.DateOfPublication, Content = w.Content, IsDelete = u.IsDelete, Username = u.Login, GenreId = w.GenreId }).Join(genreBL.GetGenres(),
                w => w.GenreId,
                g => g.Id, (w, g) => new UserWithWorkVM { Id = w.Id, Name = w.Name, DateOfPublication = w.DateOfPublication.Date, Content = w.Content, UserName = w.Username, GenreName = g.Name, IsDelete = w.IsDelete }).ToList();

            foreach (var item in workList)
            {
                if (item.IsDelete == true)
                {
                    item.UserName = "anonymous";
                }
            }


            ViewBag.WorksList = filter == null ? workList : workList.Where(x => x.Name.Contains(filter) || x.GenreName.Contains(filter) || x.UserName.Contains(filter));

            return View("_WorksTable", filter == null ? AutoMapperBL<IEnumerable<WorkBL>, List<WorkVM>>.Map(workBL.GetWorks()) : AutoMapperBL<IEnumerable<WorkBL>, List<WorkVM>>.Map(workBL.GetWorks().Where(x => x.Title.Contains(filter)).ToList()));

        }
        public ActionResult GetComments(WorkVM work)
        {
            HttpCookie cookieReqs = Request.Cookies["Localhost cookie"];
            int idsWork = work.Id;

            int idsUser = 0;
            if (cookieReqs != null)
            {
                idsUser = Convert.ToInt32(cookieReqs["ids"]);
            }
            else
            {
                FormsAuthentication.SignOut();
            }

            if (work.UserId != 0)
            {

                RatingBL userRatingForWork = ratingBL.GetRatings().Where(x => x.UserId == idsUser && x.WorkId == idsWork).FirstOrDefault();

                RatingBL newRating = new RatingBL
                {
                    Rank = work.UserId,
                    UserId = idsUser,
                    WorkId = idsWork
                };

                if (userRatingForWork != null)
                {
                    RatingBL old = ratingBL.GetRating(userRatingForWork.Id);
                    ratingBL.DeleteRating(old.Id);
                    ratingBL.Create(newRating);
                }
                else
                {
                    ratingBL.Create(newRating);
                }
            }



            if (work.Name != null)
            {
                CommentBL newComment = new CommentBL();
                newComment.Comment = work.Name;
                newComment.UserId = idsUser;
                newComment.WorkId = idsWork;
                CommentBL test = commentBL.GetComments().Where(x => x.UserId == idsUser && x.WorkId == idsWork).FirstOrDefault();

                if (null != commentBL.GetComments().Where(x => x.UserId == idsUser && x.WorkId == idsWork).FirstOrDefault())
                {
                    CommentBL old = commentBL.GetComment(test.Id);
                    commentBL.DeleteComment(old.Id);
                    commentBL.Create(newComment);

                }
                else
                {
                    commentBL.Create(newComment);
                }
            }

            var commentList = commentBL.GetComments().Join(workBL.GetWorks(),
            c => c.WorkId,
            w => w.Id, (c, w) => new { Id = c.Id, Comment = c.Comment, UserId = c.UserId, WorkId = c.WorkId }).Join(userBL.GetUsers(),
            c => c.UserId,
            u => u.Id, (c, u) => new UserWithCommentVM { Id = c.Id, UserName = u.Login, Comment = c.Comment, WorkId = c.WorkId, IsDelete = u.IsDelete }).ToList();

            foreach (var item in commentList)
            {
                if (item.IsDelete == true)
                {
                    item.UserName = "anonymous";
                }
            }
            ViewBag.CommentsList = commentList.Where(c => c.WorkId == idsWork).ToList();



            List<RatingBL> lst = ratingBL.GetRatings().Where(x => x.WorkId == idsWork).Join(userBL.GetUsers(),
               c => c.UserId,
               u => u.Id, (c, u) => new RatingBL { Id = c.Id, Rank = c.Rank, UserId = c.UserId, WorkId = c.WorkId, IsDeleteCheck = u.IsDelete }).Where(x => x.IsDeleteCheck == false).ToList();

            if (lst.Count != 0)
            {
                int sum = 0;
                int rating = 0;
                foreach (var item in lst)
                {
                    sum += item.Rank;
                }
                rating = sum / lst.Count;
                ViewBag.Ratings = rating;
            }
            else
            {
                ViewBag.Ratings = 0;
            }
            return View("_CommentsTable");
        }
        public ActionResult Details(int id)
        {
            WorkVM work = AutoMapperBL<WorkBL, WorkVM>.Map(workBL.GetWork, id);
            work.Name = "";
            work.UserId = 1;
            return View(work);
        }
        public ActionResult Create()
        {
            ViewBag.GenreList = new SelectList(genreBL.GetGenres().ToList(), "Id", "Name");
            return View();
        }
        public ActionResult Update(int id)
        {
            ViewBag.GenreList = new SelectList(genreBL.GetGenres().ToList(), "Id", "Name");
            WorkVM work = AutoMapperBL<WorkBL, WorkVM>.Map(workBL.GetWork, id);
            return View(work);
        }
        public ActionResult Delete(int id)
        {
            HttpCookie cookieReqs = Request.Cookies["Localhost cookie"];

            int idsUser = 0;

            int thisWorkUserId = workBL.GetWork(id).UserId;

            if (cookieReqs != null)
            {
                idsUser = Convert.ToInt32(cookieReqs["ids"]);
                if (idsUser == thisWorkUserId)
                {
                    List<CommentBL> workCommentsList = commentBL.GetComments().Where(x => x.WorkId == id).ToList();
                    List<RatingBL> workRatingList = ratingBL.GetRatings().Where(x => x.WorkId == id).ToList();
                    foreach (var item in workCommentsList)
                    {
                        commentBL.DeleteComment(item.Id);
                    }
                    foreach (var item in workRatingList)
                    {
                        ratingBL.DeleteRating(item.Id);
                    }
                    workBL.DeleteWork(id);

                    return RedirectToAction("Index", "Works");
                }
                else
                {
                    return RedirectToAction("Index", "Works");
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Works");
            }
        }

    }
}