using lab04.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace lab04.Controllers
{
    public class AttendancesController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Attend(Course attendanceDto)
        {
            var userID = User.Identity.GetUserId();
            BigschoolDBContext context = new BigschoolDBContext();
            if (context.Attendee.Any(p => p.Attendee1 == userID && p.CourseID ==
            attendanceDto.id))

                if (context.Attendee.Any(p => p.Attendee1 == userID && p.CourseID == attendanceDto.id))
            {
                context.Attendee.Remove(context.Attendee.SingleOrDefault(p =>
 p.Attendee1 == userID && p.CourseID == attendanceDto.id));
                context.SaveChanges();
                return Ok("cancel");
            }
            var Attendee = new Attendee()
            {
                CourseID = attendanceDto.id,
                Attendee1 = User.Identity.GetUserId()
            };

            context.Attendee.Add(Attendee);
            context.SaveChanges();
            return Ok();
        }
    }
}
