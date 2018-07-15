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
    public class OperationsController : Controller
    {
        private GcomptesContext db = new GcomptesContext();

        // GET: Operations
        public ActionResult Index()
        {
            var operations = db.Operations.Include(o => o.Compte);
            return View(operations.ToList());
        }

        // GET: Operations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.Operations.Find(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            return View(operation);
        }

        // GET: Operations/Create
        public ActionResult Create()
        {
            ViewBag.CompteID = new SelectList(db.Comptes, "CompteID", "CompteID");
            return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OperationID,typeOperation,CompteID,montant")] Operation operation)
        {
            operation.dateOperation = DateTime.Now;
            if (ModelState.IsValid)
            {
                Compte compte = db.Comptes.Where(s => s.ClientID == operation.CompteID).First();
                if (operation.typeOperation.ToString() == "Retrait")
                {
                    if (compte.solde >= operation.montant)
                    {
                        compte.solde = compte.solde - operation.montant;
                        db.Entry(compte).State = EntityState.Modified;
                        db.SaveChanges();
                        
                    }

                    else
                    {
                        ModelState.AddModelError("montant", "Solde insuffisante pour retirer");
                        ViewBag.CompteID = new SelectList(db.Comptes, "CompteID", "CompteID", operation.CompteID);
                        return View(operation);
                    }
                }
                else if (operation.typeOperation.ToString() == "Versement")
                {
                    compte.solde = compte.solde + operation.montant;
                    db.Entry(compte).State = EntityState.Modified;
                    db.SaveChanges();
                }
                db.Operations.Add(operation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompteID = new SelectList(db.Comptes, "CompteID", "CompteID", operation.CompteID);
            return View(operation);
        }

        // GET: Operations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.Operations.Find(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompteID = new SelectList(db.Comptes, "CompteID", "CompteID", operation.CompteID);
            return View(operation);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OperationID,typeOperation,CompteID,dateOperation,montant")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompteID = new SelectList(db.Comptes, "CompteID", "CompteID", operation.CompteID);
            return View(operation);
        }

        // GET: Operations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.Operations.Find(id);
            if (operation == null)
            {
                return HttpNotFound();
            }
            return View(operation);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Operation operation = db.Operations.Find(id);
            db.Operations.Remove(operation);
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
