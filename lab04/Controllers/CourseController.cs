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
            BigSchoolDBContext context = new BigSchoolDBContext();
            Course objCourse = new Course();
            objCourse.ListCategory = context.Category.ToList();
            return View(objCourse);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objcourse)
        {
            BigSchoolDBContext context = new BigSchoolDBContext();

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
        public ActionResult Attending()
        {
            BigSchoolDBContext context = new BigSchoolDBContext();
            ApplicationUser currenUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var listAttendance = context.Attendee.Where(p => p.Attendee1 == currenUser.Id).ToList();
            var courses = new List<Course>();
            foreach (Attendee temp in listAttendance)
            {
                Course objCourse = temp.Course;
                
                objCourse.LectureName = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(objCourse.LectureID).Name;
                courses.Add(objCourse);
            }
            return View(courses);
        }
        public ActionResult Mine()
        {
            ApplicationUser currenUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            BigSchoolDBContext context = new BigSchoolDBContext();
            var courses = context.Course.Where(c => c.LectureID == currenUser.Id && c.Datetime > DateTime.Now).ToList();
            foreach (Course i in courses)
            {
                i.LectureName = currenUser.Name;
            }
            return View(courses);
        }
    }
}