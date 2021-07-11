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
        //[HttpGet]
        //  public ActionResult Edit(int id) 
        //  {
        //      BigSchoolDBContext context = new BigSchoolDBContext();
        //      Course courses = context.Course.SingleOrDefault(p => p.id == id);
        //      Course objCourse = new Course();
        //      objCourse.ListCategory = context.Category.ToList();
        //      if (courses == null)
        //      {
        //          return HttpNotFound();

        //      }
        //      return View(objCourse);
        //  }
        //  [Authorize]
        //  [HttpPost]
        //  public ActionResult Edit(Course course)
        //  {
        //      BigSchoolDBContext context = new BigSchoolDBContext();
        //      Course courseUpdate = context.Course.SingleOrDefault(p => p.id == course.id);

        //      if (courseUpdate != null)
        //      {
        //          context.Course.AddOrUpdate(course);
        //          context.SaveChanges();
        //      }
        //      return RedirectToAction("Mine");
        //  }
        //  public ActionResult Delete(int id)
        //  {
        //      BigSchoolDBContext context = new BigSchoolDBContext();
        //      Course courses = context.Course.SingleOrDefault(p => p.id == id);
        //      if (courses == null)
        //      {
        //          return HttpNotFound();
        //      }
        //      return View(courses);
        //  }
        //  [Authorize]
        //  [HttpPost]

        //  public ActionResult DeleteBook(int id)
        //  {
        //      BigSchoolDBContext context = new BigSchoolDBContext();
        //      Course courses = context.Course.SingleOrDefault(p => p.id == id);
        //      if (courses != null)
        //      {
        //          context.Course.Remove(courses);
        //          context.SaveChanges();

        //      }
        //      return RedirectToAction("Mine");
        //  }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            BigSchoolDBContext context = new BigSchoolDBContext();
            Course c = context.Course.SingleOrDefault(p => p.id == id);
            c.ListCategory = context.Category.ToList();
            return View(c);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Course c)
        {
           
            try

            {

                BigSchoolDBContext context = new BigSchoolDBContext();
                Course edit = context.Course.SingleOrDefault(p => p.id == c.id);

                if (edit != null)
                {
                    context.Course.AddOrUpdate(c);
                    context.SaveChanges();

                }
                return RedirectToAction("Mine");

            }

            catch (DbEntityValidationException ex)

            {

                // Retrieve the error messages as a list of strings.

                var errorMessages = ex.EntityValidationErrors

                .SelectMany(x => x.ValidationErrors)

                .Select(x => x.ErrorMessage);

                // Join the list to a single string.

                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

            }
        }
        //[Authorize]
        //public ActionResult Delete(int id)
        //{
        //    BigSchoolDBContext context = new BigSchoolDBContext();
        //    Course delete = context.Course.SingleOrDefault(p => p.id == id);
        //    return View(delete);
        //}
        //[HttpPost]
        //public ActionResult DeleteCourse(int id)
        //{
        //    BigSchoolDBContext context = new BigSchoolDBContext();
        //    Course delete = context.Course.SingleOrDefault(p => p.id == id);

        //    if (delete != null)
        //    {
        //        context.Course.Remove(delete);
        //        context.SaveChanges();

        //    }
        //    return RedirectToAction("Mine");
        //}
        [Authorize]
        public ActionResult Delete(int id)
        {
            BigSchoolDBContext context = new BigSchoolDBContext();
            Course delete = context.Course.SingleOrDefault(p => p.id == id);
            return View(delete);
        }
        [HttpPost]
        public ActionResult DeleteCourse(int id)
        {
           
            try

            {
                BigSchoolDBContext context = new BigSchoolDBContext();
                Course delete = context.Course.SingleOrDefault(p => p.id == id);
                if (delete != null)
                {
                    context.Course.Remove(delete);
                    context.SaveChanges();

                }
              


            }

            catch (DbEntityValidationException ex)

            {

                // Retrieve the error messages as a list of strings.

                var errorMessages = ex.EntityValidationErrors

                .SelectMany(x => x.ValidationErrors)

                .Select(x => x.ErrorMessage);

                // Join the list to a single string.

                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

            }
            return RedirectToAction("Mine");
        }
          
    }

}