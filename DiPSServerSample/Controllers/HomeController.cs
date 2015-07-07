using DiPSServerSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiPSServerSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.TitlePage = "Chat app with SSE";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userName)
        {
            //add the cookie
            Response.Cookies.Add(new HttpCookie("chatUser", userName));
            //ChatHelper.UserLogin(userName);
            //inform others that a new user has logged in
            Broker.PublishMessage("userlogin", new { UserName = userName });
            //redirect to the chat page
            return RedirectToAction("Index", "Chat", new { });
        }
    }
}