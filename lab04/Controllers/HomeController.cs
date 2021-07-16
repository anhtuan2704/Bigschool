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
            BigschoolDBContext context = new BigschoolDBContext();
            var upcommingCourse = context.Course.Where(p => p.Datetime > DateTime.Now).OrderBy(p => p.Datetime).ToList();
            var userID = User.Identity.GetUserId();
            foreach (Course i in upcommingCourse)
            {
                // tìm name của user từ lectureID
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(i.LectureID);
                i.Name = user.Name;
                
                //lấy danh sách tham gia khóa học
                if(userID != null)
                {
                    i.isLogin = true;
                    //kiểm tra user đó chưa tham gia khóa học
                    Attendee find = context.Attendee.FirstOrDefault(p => p.CourseID == i.id && p.Attendee1 == userID);
                    if (find == null)
                        i.isShowGoing = true;
                    //ktra user đã theo dõi giảng viên của khóa hoc?
                    Following findFollow = context.Following.FirstOrDefault(p => p.FllowerID == userID && p.FlloweeID == i.LectureID);
                    if (findFollow == null)
                        i.isShowFollow = true;
                }
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