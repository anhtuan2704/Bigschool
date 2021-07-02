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
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Create()
        {
            BigschoolContext context = new BigschoolContext();
            Course objCourse = new Course();
            objCourse.ListCategory = context.Category.ToList();
            return View(objCourse);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objcourse)
        {
            BigschoolContext context = new BigschoolContext();

            ModelState.Remove("LectureID");
            if (!ModelState.IsValid)
            {
                objcourse.ListCategory = context.Category.ToList();
                return View("Create", objcourse);
            }
            //lấy ID user
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            objcourse.LectureID = user.Id;
            //add vào csdl

            context.Course.Add(objcourse);
            context.SaveChanges();

            //trở về home
            return RedirectToAction("Index", "Home");
        }
    }
}