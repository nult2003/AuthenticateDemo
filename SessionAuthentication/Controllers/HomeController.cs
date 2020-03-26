using SessionAuthentication.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SessionAuthentication.Controllers
{
    public class HomeController : Controller
    {
        [SessionAuthenticationAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [SessionAuthenticationAttribute]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [SessionAuthenticationAttribute]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            // Check thông tin user ở day, neu đúng thì sinh ra 1 sessionId và session này duoc giữ để cho request tiếp theo
            // giữ session này trong cookies, client se gui session này theo cookies ve cho  server
            Session["UserID"] = Guid.NewGuid();
            HttpCookie userCookie = new HttpCookie("UserID", HttpContext.Session["UserID"].ToString());
            HttpContext.Response.Cookies.Add(userCookie);
            return View("Index");
        }
    }
}