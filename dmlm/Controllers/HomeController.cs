using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dmlm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Logistics Manager";
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
            // TODO get these from the user object in the db
            ViewBag.Message = "Dashboard";
            ViewBag.Title = "Dashboard";
            ViewBag.Layout = "~/Views/Shared/_SawyerLayout.cshtml";
            ViewBag.ServiceProviderName = "Sawyer Brewing Company";
            return View();
        }
    }
}