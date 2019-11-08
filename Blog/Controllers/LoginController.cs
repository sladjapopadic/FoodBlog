using Blog.Data;
using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(User userModel)
        {
            using (BlogContext db = new BlogContext())
            {
                var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Pogresno korisnicko ime ili lozinka.";
                    return View("LoginForm", userModel);
                }
                else
                {
                    Session["Id"] = userDetails.Id;
                    Session["Username"] = userDetails.Username;
                    Session["IsAdmin"] = userDetails.IsAdmin;
                    return RedirectToAction("Index","Home");
                }
            }
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["Id"];
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}