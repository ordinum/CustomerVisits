using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CVisits.Models;
using CVisits.DAL;

namespace CVisits.Controllers
{
    public class AccionResponsableController : Controller
    {
        private CVisitsContext db = new CVisitsContext();

        //
        // GET: /AccionResponsable/

        public ActionResult Index()
        {
            return View(db.AccionResponsable.ToList());
        }

        //
        // GET: /AccionResponsable/Details/5

        public ActionResult Details(int id = 0)
        {
            AccionResponsable accionresponsable = db.AccionResponsable.Find(id);
            if (accionresponsable == null)
            {
                return HttpNotFound();
            }
            return View(accionresponsable);
        }

        //
        // GET: /AccionResponsable/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AccionResponsable/Create

        [HttpPost]
        public ActionResult Create(AccionResponsable accionresponsable)
        {
            if (ModelState.IsValid)
            {
                db.AccionResponsable.Add(accionresponsable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accionresponsable);
        }

        //
        // GET: /AccionResponsable/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AccionResponsable accionresponsable = db.AccionResponsable.Find(id);
            if (accionresponsable == null)
            {
                return HttpNotFound();
            }
            return View(accionresponsable);
        }

        //
        // POST: /AccionResponsable/Edit/5

        [HttpPost]
        public ActionResult Edit(AccionResponsable accionresponsable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accionresponsable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accionresponsable);
        }

        //
        // GET: /AccionResponsable/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AccionResponsable accionresponsable = db.AccionResponsable.Find(id);
            if (accionresponsable == null)
            {
                return HttpNotFound();
            }
            return View(accionresponsable);
        }

        //
        // POST: /AccionResponsable/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            AccionResponsable accionresponsable = db.AccionResponsable.Find(id);
            db.AccionResponsable.Remove(accionresponsable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}