using lab04.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab04.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BigschoolContext context = new BigschoolContext();
            var upcommingCourse = context.Course.Where(p => p.Datetime > DateTime.Now).OrderBy(p => p.Datetime).ToList();
            foreach (Course i in upcommingCourse)
            {
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(i.LectureID);
                i.Name = user.Name;

            }
            return View(upcommingCourse);
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
    }
}