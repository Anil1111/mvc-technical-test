using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using NLog;
using RoomBooking.Business;
using RoomBooking.Data.Entities;
using RoomBooking.Models.Authentication;

namespace RoomBooking.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private ILogger Log = LogManager.GetCurrentClassLogger();
        private readonly IAuthenticationBusiness _authenticationBusiness;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AuthenticationController(IAuthenticationBusiness authenticationBusiness)
        {
            _authenticationBusiness = authenticationBusiness;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _authenticationBusiness.TryLoginAsync(model.Email, model.Password);
            if (user != null)
            {
                SignIn(user, model.RememberMe);
                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            Log.Info($"Failed login attempt for {model.Email}");
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private void SignIn(AppUser user, bool rememberMe)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name)
            };
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            ctx.Authentication.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddHours(8),
                IsPersistent = rememberMe
            }, identity);
            
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }
    }
}