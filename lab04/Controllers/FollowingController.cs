using lab04.Models;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace lab04.Controllers
{

    public class FollowingController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Follow(Following follow)
        {
            //user login là nguoi82 theo dỏi, followeID là người dc theo doi
            var userID = User.Identity.GetUserId();
            if (userID == null)
                return BadRequest("Please login first!");
            if (userID == follow.FlloweeID)
                return BadRequest("Can not follow my selft!");
            BigschoolDBContext context = new BigschoolDBContext();
            Following find = context.Following.FirstOrDefault(p => p.FllowerID == userID && p.FlloweeID == follow.FlloweeID);
            if (find != null)
            {
                context.Following.Remove(context.Following.SingleOrDefault(p =>p.FllowerID == userID && p.FlloweeID == follow.FlloweeID));
                context.SaveChanges();
                return Ok("cancel");
            }

            follow.FllowerID = userID;
            context.Following.Add(follow);
            context.SaveChanges();
            return Ok();
        }
    }
}
