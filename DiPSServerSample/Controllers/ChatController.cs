using DiPSServerSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiPSServerSample.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            ViewBag.UserName = Request.Cookies["chatUser"].Value;
            return View();
        }


        /// <summary>
        /// Get on line users
        /// </summary>
        [HttpPost]
        public JsonResult GetUsers()
        {
            return Json(ChatHelper.OnlineUsers);
        }
    }
}