using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionCompte.Models;
using GestionCompte.Models.DAL;

namespace GestionCompte.Controllers
{
    public class ComptesController : Controller
    {
        private GcomptesContext db = new GcomptesContext();

        // GET: Comptes
        public ActionResult Index()
        {
            var comptes = db.Comptes.Include(c => c.client);
            return View(comptes.ToList());
        }

        // GET: Comptes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compte compte = db.Comptes.Find(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Comptes/Create
        public ActionResult Create()
        {
            ViewData["dateCreation"] = DateTime.Now;
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "nomClient");
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        // POST: Comptes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompteID,ClientID,dateCreation,solde")] Compte compte)
        {
            compte.dateCreation = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Comptes.Add(compte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "nomClient", compte.ClientID);
            return View(compte);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Comptes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compte compte = db.Comptes.Find(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "nomClient", compte.ClientID);
            return View(compte);
        }

        [Authorize(Roles = "ADMIN")]
        // POST: Comptes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompteID,ClientID,dateCreation,solde")] Compte compte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "nomClient", compte.ClientID);
            return View(compte);
        }

        [Authorize(Roles = "ADMIN")]
        // GET: Comptes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compte compte = db.Comptes.Find(id);
            if (compte == null)
            {
                return HttpNotFound();
            }
            return View(compte);
        }

        [Authorize(Roles = "ADMIN")]
        // POST: Comptes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compte compte = db.Comptes.Find(id);
            db.Comptes.Remove(compte);
            db.SaveChanges();
            return RedirectToAction("Index");
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
