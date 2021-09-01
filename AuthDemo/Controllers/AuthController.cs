using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthDemo.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(string uid, string pwd)
        {
            bool status = false;
            
            if (uid.ToLower() == "admin" && pwd == "123")
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,uid),
                };

                var userIdentity = new ClaimsIdentity(userClaims, "User Identiy");
                var userPrinciple = new ClaimsPrincipal(new[] { userIdentity });

                HttpContext.SignInAsync(userPrinciple);
                status = userPrinciple.Identity.IsAuthenticated;
            }

            return Json(new { status });
        }


        public IActionResult logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("login");
        }


    }
}
