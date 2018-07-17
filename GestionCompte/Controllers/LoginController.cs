using GestionCompte.Models;
using GestionCompte.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GestionCompte.Controllers
{
    public class LoginController : Controller
    {

        private GcomptesContext db = new GcomptesContext();

        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users users, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                using (GcomptesContext db = new GcomptesContext())
                {
                    var us = db.Users.Where(u => u.Username.Equals(users.Username) && u.Password.Equals(users.Password)).FirstOrDefault();
                    if (us != null)
                    {
                        FormsAuthentication.SetAuthCookie(us.UsersID.ToString(), false);
                        Session["username"] = us.Username.ToString();
                        if (!string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                            return Redirect(ReturnUrl);
                    }
                    ModelState.AddModelError("Users.Username", "Username et/ou Password incorrect(s)");
                }
            }
            return View(users);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UsersID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}