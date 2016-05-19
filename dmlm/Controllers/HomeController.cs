using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace dmlm.Controllers
{
    public class HomeController : Controller
    {
        private dmlmEntities db = new dmlmEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Logistics Manager";
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Dashboard");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Dashboard()
        {
            var username = User.Identity.GetUserName().ToLower();
            var user = db.Users.Where(u => u.emailAddress.ToLower() == username).FirstOrDefault();
            if (user == null)
                return View();
            var pageModel = new Models.PageModel().GetPage(user);

            // TODO get these from the user object in the db
            ViewBag.Message = "Dashboard";
            ViewBag.Title = "Dashboard";
            ViewBag.Layout = pageModel.Layout;

           HttpContext.Cache.Add("pageModel", pageModel,
             null, DateTime.UtcNow.AddHours(10), Cache.NoSlidingExpiration,
             CacheItemPriority.Normal, null);
            return View(pageModel);
        }

        public ActionResult Agent()
        {
            var username = User.Identity.GetUserName().ToLower();
            var user = db.Users.Where(u => u.emailAddress.ToLower() == username).FirstOrDefault();
            if (user == null)
                return View();
            var pageModel = new Models.PageModel().GetPage(user);

            // TODO get these from the user object in the db
            ViewBag.Message = "Dashboard";
            ViewBag.Title = "Dashboard";
            ViewBag.Layout = pageModel.Layout;
            return View(pageModel);
        }
    }
}