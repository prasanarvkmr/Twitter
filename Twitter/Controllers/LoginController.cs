using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Models;
using Twitter.DataAccess;

namespace Twitter.Controllers
{
    public class LoginController : Controller
    {
        public DataRepository DataAccess { get; set; }

        public LoginController()
        {
            if (DataAccess == null)
            {
                DataAccess = new DataRepository();
            }
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login loginDetails)
        {
            if(ModelState.IsValid)
            {
                var user = DataAccess.IsUserValid(loginDetails);
                if (user != null)
                {
                    this.Session.Add("User", user);
                    return this.RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("Error-Key", "Invalid Credentials");
            return this.View("Index", loginDetails);
        }

        public ActionResult Register()
        {
            return this.View("Register");
        }

        [HttpPost]
        public ActionResult Register(Regsiter registerDetails)
        {
            if (ModelState.IsValid)
            {
                var user = DataAccess.RegisterPerson(registerDetails);
                if (user != null)
                {
                    this.Session.Add("Login", registerDetails);
                    this.Session.Add("User", user);
                    return this.RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("Error-Key", "Invalid Credentials");
            return this.View("Index", registerDetails);
        }
    }
}