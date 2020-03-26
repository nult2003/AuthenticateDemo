using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseAuthentication.Filters
{
    // Scenario: trong mo hinh noi bo (internal) => thong tin user duoc quan ly boi admin
    // Chung ta se xay dung table user tren dabase => tao form quan ly thong tin dang nhap (username va password)
    // client: duoc admin cung cap tai khoan (user name va password) de dang nhap vao he thong
    public class BasicAuthenticationAttribute: ActionFilterAttribute
    {
        public string BasicRealm { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
        // Mo phong scenario:
        // hard code username va password tu action filter duoc xem nhu la username va password tu database
        // User name va password nay se so sanh voi thong tin dang nhap cua client nhap vao
        public BasicAuthenticationAttribute(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            // base authentication
            if (!string.IsNullOrEmpty(auth))
            {
                var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var user = new { Name = cred[0], Pass = cred[1] };
                if(user.Name == UserName && user.Pass == Password)
                {
                    return;
                }
            }

            // session authentication


            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", BasicRealm ?? "Ryadel"));
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}