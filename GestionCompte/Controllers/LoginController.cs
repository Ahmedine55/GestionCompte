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

        // GET: Login
        public ActionResult Index()
        {
            UsersViewModel viewModel = new UsersViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (int.TryParse(HttpContext.User.Identity.Name, out int id))
                    viewModel.Users = db.Users.FirstOrDefault(u => u.UsersID == id);
            }
            return View(viewModel);
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UsersViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (GcomptesContext db = new GcomptesContext())
                {
                    Users us = db.Users.FirstOrDefault(u => u.Username.Equals(viewModel.Users.Username) && u.Password.Equals(viewModel.Users.Password));
                    if (us != null)
                    {
                        FormsAuthentication.SetAuthCookie(us.UsersID.ToString(), false);
                        //Session["UsersID"] = obj.UsersID.ToString();
                        //Session["UserName"] = obj.Username.ToString();
                        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        return Redirect("/");
                    }
                    ModelState.AddModelError("Users.Username", "Username et/ou Password incorrect(s)");
                }
            }
            return View(viewModel);
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

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}