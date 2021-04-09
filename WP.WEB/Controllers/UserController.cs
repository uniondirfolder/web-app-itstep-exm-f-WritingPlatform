
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
    public class UserController : Controller
    {
        #region Fld & Ctor
        private readonly IUserBL _userBL;
        private readonly IAuthentificationBL _authentificationBL;
        private readonly IWorkBL _workBL;

        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
        }
        public UserController(IUserBL userBL, IAuthentificationBL authentificationBL, IWorkBL workBL)
        {
            _userBL = userBL;
            _authentificationBL = authentificationBL;
            _workBL = workBL;
        }
        #endregion

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new UserVM());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login (UserVM userVM, string rtnUrl) 
        {
            if (ModelState.IsValid) 
            {
                if(_authentificationBL.CheckLogin(userVM.Login, userVM.Password)) 
                {
                    if(_authentificationBL.GetUserStatus(userVM.Login, userVM.Password) == false) 
                    {
                        var id = _authentificationBL.GetUserId(userVM.Login, userVM.Password);

                        if (0!=id) 
                        {
                            var cookie = new HttpCookie("Localhost cokie");
                            cookie["ids"] = id.ToString();
                            cookie.Expires = DateTime.Now.AddHours(1);
                            Response.Cookies.Add(cookie);
                        }

                        FormsAuthentication.SetAuthCookie(userVM.Login, false);
                        return Redirect(rtnUrl ?? Url.Action("Index", "Home"));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Your account deleted");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                    return View();
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            return View(AutoMapperBL<IEnumerable<UserBL>, List<UserVM>>.Map(_userBL.GetUsers().Where(x => x.IsDelete == false)));
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(UserVM user)
        {
            if (ModelState.IsValid)
            {
                var item = AutoMapperBL<UserVM, UserBL>.Map(user);
                item.IsDelete = false;
                HttpCookie cookie = new HttpCookie("Localhost cookie");
                cookie["ids"] = _authentificationBL.GetUserId(user.Login, user.Password).ToString();
                cookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(cookie);
                _userBL.Create(item);
                return RedirectToActionPermanent("Index", "Users");
            }
            return View("Create");
        }

        public ActionResult Update(int id)
        {
            var item = AutoMapperBL<UserBL, UserVM>.Map(_userBL.GetUser, id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Update(UserVM user)
        {
            var item = AutoMapperBL<UserVM, UserBL>.Map(user);
            _userBL.Update(item);
            return RedirectToActionPermanent("Index", "Users");
        }

        public ActionResult Delete(int id)
        {
            _userBL.DeleteUser(id);
            return RedirectToAction("Index", "Users");
        }

        public ActionResult PersonalProfile()
        {
            HttpCookie cookieReq = Request.Cookies["Localhost cookie"];

            int ids = 0;
            if (cookieReq != null)
            {
                ids = Convert.ToInt32(cookieReq["ids"]);
            }

            var item = AutoMapperBL<UserBL, UserVM>.Map(_userBL.GetUser, ids);
            ViewBag.UserWorksList = _workBL.GetWorks().Where(x => x.UserId == ids).ToList();
            return View(item);
        }

        [HttpPost]
        public ActionResult PersonalProfile(UserVM user)
        {
            var item = AutoMapperBL<UserVM, UserBL>.Map(user);
            _userBL.Update(item);
            return RedirectToActionPermanent("PersonalProfile", "Users");
        }

        public ActionResult DeleteAccount()
        {
            HttpCookie cookieReq = Request.Cookies["Localhost cookie"];

            int ids = 0;
            if (cookieReq != null)
            {
                ids = Convert.ToInt32(cookieReq["ids"]);
            }
            var old = AutoMapperBL<UserBL, UserVM>.Map(_userBL.GetUser, ids);

            old.IsDelete = true;

            UserBL @new = AutoMapperBL<UserVM, UserBL>.Map(old);

            _userBL.Update(@new);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult CheckUserName([Bind(Prefix = "Login")] string username)
        {
            return Json(!_userBL.GetUsers().Any(u => u.Login == username), JsonRequestBehavior.AllowGet);
        }
    }
}