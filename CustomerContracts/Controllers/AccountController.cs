using CustomerContracts.Core;
using CustomerContracts.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomerContracts.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                User user = null;
                using (UserRepository repository = new UserRepository())
                {
                    user = repository.Login(model.Email, model.Password);
                }

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);

                    var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.Role.IsAdmin.ToString());
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                    throw new Exception();

            }
            catch (Exception)
            {
                ModelState.AddModelError("Email", "The email and/or password entered is invalid.Please try again");
                ModelState.AddModelError("Password", "The email and/or password entered is invalid.Please try again");
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
