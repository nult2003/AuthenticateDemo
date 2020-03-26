using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace SessionAuthentication.Filters
{
    public class SessionAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public SessionAuthenticationAttribute()
        {
        }

        //
        public SessionAuthenticationAttribute(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            // Check Session is Empty then set as Result is HttpUnauthorizedResult 
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserID"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                // Kiem tra client gửi thong tin dang nhap thanh cong cua lan truoc, neu dung thi pass
                string sessionlogin = filterContext.HttpContext.Request.Cookies["UserID"].Value;
                string currentLogin = filterContext.HttpContext.Session["UserID"].ToString();
                if (sessionlogin != currentLogin)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }                
            }
        }

        // Runs after the onAuthentication method
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            // We are checking Result is null or result is HttpUnauthorizedResult
            // if yes then we are Redirect to Error View
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error"
                };
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}