using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TokenBaseAuthetication.JwtAuth;
using TokenBaseAuthetication.Models;

namespace TokenBaseAuthetication.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            IAuthContainerModel model = GetJwtContainerModel("Nhat", "Nhat@gmail.com");
            IAuthService authService = new JwtService(model.SecretKey);
            string token = authService.GenerateToken(model);

            if (!authService.IsTokenValid(token))
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                List<Claim> claims = authService.GetTokenClaims(token).ToList();
                string name = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name)).Value;
                string email = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Email)).Value;
            }
        }
        public ActionResult Index()
        {
            return View();
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

        private JwtContainerModel GetJwtContainerModel(string name, string email)
        {
            return new JwtContainerModel()
            {
                Claims = new System.Security.Claims.Claim[]
                {
                    new System.Security.Claims.Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email)
                }
            };
        }
    }
}