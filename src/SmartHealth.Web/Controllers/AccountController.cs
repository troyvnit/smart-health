using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SmartHealth.Core.Domain.Dtos;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Bussiness;

namespace SmartHealth.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IService<User> userService;

        public AccountController(IService<User> userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(UserDto user, string returnUrl)
        {
            if (userService.GetAll().Any(a => a.Username == user.Username && a.Password == user.Password))
            {
                FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
                FormsAuthentication.RedirectFromLoginPage(user.Id.ToString(), user.RememberMe);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Article");
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View("SignIn", user);
        }

    }
}
