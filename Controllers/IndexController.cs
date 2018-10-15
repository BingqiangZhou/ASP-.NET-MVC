using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.Models;

namespace Model.Controllers
{
    public class IndexController : Controller
    {
        private MVCEntities db = new MVCEntities();

        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignIn()
        {
            return View("SignIn");
        }

        [HttpPost]
        public ActionResult SignIn(string Email,string Password)
        {
            //System.Diagnostics.Debug.WriteLine(Email + Password);
            User user = db.Users.Find(Email);

            if (user != null && user.Password.Equals(Password))
            {
                Response.Cookies["Name"].Value = user.Name;
                Response.Cookies["Name"].Expires = DateTime.Now.AddHours(1);
                Response.Cookies["Email"].Value = user.Email;
                Response.Cookies["Email"].Expires = DateTime.Now.AddHours(1);
                return RedirectToAction("../Main");
            }
            ViewBag.ErrorMessage = "Verification failed!";
            return View("SignIn");
        }

        public ActionResult SignUp()
        {
            return View("SignUp");
        }
        [HttpPost]
        public ActionResult SignUp(string Email,string Name,string Password)
        {
            User user = db.Users.Find(Email);
            if(user != null)
            {
                ViewBag.ErrorMessage = "User already exist!";
                return View("SignUp");
            }
            user = new User();
            user.Email = Email;
            user.Name = Name;
            user.Password = Password;
            user.Rank = 1;
            user.Status = 1;
            db.Users.Add(user);
            db.SaveChanges();
            return View("Index");
        }
        public ActionResult ForgetPassword()
        {
            return View("ForgetPassword");
        }

        [HttpPost]
        public ActionResult ForgetPassword(string Email, string Name, string Password)
        {
            User user = db.Users.Find(Email);
            if (user == null)
            {
                ViewBag.ErrorMessage = "User is not exist!";
                return View("ForgetPassword");
            }
            if(! Name.Equals(user.Name))
            {
                ViewBag.ErrorMessage = "Verification failed!";
                return View("ForgetPassword");
            }
            user.Password = Password;
            db.SaveChanges();
            return View("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}