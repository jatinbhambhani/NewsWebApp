using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace _5032_MVC_News.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "News Portal was the first of its kind when it launched in 2000, and today the News Portal Network is the largest and most watched regional news network in the country. Since the first year of programming, News Portal has had the honor of receiving numerous industry and community awards.";

            return View();
        }

        [Authorize]
        public ActionResult Documentation()
        {
            ViewBag.Message = "Author Details: Sanya Guglani";

            return View();
        }


        public ActionResult Sent()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}