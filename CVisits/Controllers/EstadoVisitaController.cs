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
    public class EstadoVisitaController : Controller
    {
        private CVisitsContext db = new CVisitsContext();

        //
        // GET: /EstadoVisita/

        public ActionResult Index()
        {
            return View(db.EstadoVisita.ToList());
        }

        //
        // GET: /EstadoVisita/Details/5

        public ActionResult Details(int id = 0)
        {
            EstadoVisita estadovisita = db.EstadoVisita.Find(id);
            if (estadovisita == null)
            {
                return HttpNotFound();
            }
            return View(estadovisita);
        }

        //
        // GET: /EstadoVisita/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /EstadoVisita/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EstadoVisita estadovisita)
        {
            if (ModelState.IsValid)
            {
                db.EstadoVisita.Add(estadovisita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadovisita);
        }

        //
        // GET: /EstadoVisita/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EstadoVisita estadovisita = db.EstadoVisita.Find(id);
            if (estadovisita == null)
            {
                return HttpNotFound();
            }
            return View(estadovisita);
        }

        //
        // POST: /EstadoVisita/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EstadoVisita estadovisita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadovisita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadovisita);
        }

        //
        // GET: /EstadoVisita/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EstadoVisita estadovisita = db.EstadoVisita.Find(id);
            if (estadovisita == null)
            {
                return HttpNotFound();
            }
            return View(estadovisita);
        }

        //
        // POST: /EstadoVisita/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoVisita estadovisita = db.EstadoVisita.Find(id);
            db.EstadoVisita.Remove(estadovisita);
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