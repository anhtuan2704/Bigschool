using lab04.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
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
            BigschoolDBContext context = new BigschoolDBContext();
            Course objCourse = new Course();
            objCourse.ListCategory = context.Category.ToList();
            return View(objCourse);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course objcourse)
        {
            BigschoolDBContext context = new BigschoolDBContext();

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
            BigschoolDBContext context = new BigschoolDBContext();
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
            BigschoolDBContext context = new BigschoolDBContext();
            var courses = context.Course.Where(c => c.LectureID == currenUser.Id && c.Datetime > DateTime.Now).ToList();
            foreach (Course i in courses)
            {
                i.LectureName = currenUser.Name;
            }
            return View(courses);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            BigschoolDBContext context = new BigschoolDBContext();
            Course c = context.Course.SingleOrDefault(p => p.id == id);
            c.ListCategory = context.Category.ToList();
            return View(c);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Course c)
        {
            BigschoolDBContext context = new BigschoolDBContext();
                Course edit = context.Course.SingleOrDefault(p => p.id == c.id);

                if (edit != null)
                {
                    context.Course.AddOrUpdate(c);
                    context.SaveChanges();

                }
                return RedirectToAction("Mine");

        }
       
        [Authorize]
        public ActionResult Delete(int id)
        {
            BigschoolDBContext context = new BigschoolDBContext();
            Course delete = context.Course.SingleOrDefault(p => p.id == id);
            return View(delete);
        }
        [HttpPost]
        public ActionResult DeleteCourse(int id)
        {


            BigschoolDBContext context = new BigschoolDBContext();
                Course delete = context.Course.SingleOrDefault(p => p.id == id);
                if (delete != null)
                {
                    context.Course.Remove(delete);
                    context.SaveChanges();

                }
            return RedirectToAction("Mine");
        }
        public ActionResult LectureiamGoing()
        {
            ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            BigschoolDBContext context = new BigschoolDBContext();
            //danh sách giảng viên được theo dõi bởi người dùng (đăng nhập) hiện tại
            var listFollwee = context.Following.Where(p => p.FllowerID ==

            currentUser.Id).ToList();

            //danh sách các khóa học mà người dùng đã đăng ký
            var listAttendances = context.Attendee.Where(p => p.Attendee1 ==

            currentUser.Id).ToList();

            var courses = new List<Course>();
            foreach (var course in listAttendances)

            {
                foreach (var item in listFollwee)
                {
                    if (item.FlloweeID == course.Course.LectureID)
                    {
                        Course objCourse = course.Course;
                        objCourse.LectureName =
                        System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                        .FindById(objCourse.LectureID).Name;
                        courses.Add(objCourse);
                    }
                }
            }
            return View(courses);
        }  
    }

}