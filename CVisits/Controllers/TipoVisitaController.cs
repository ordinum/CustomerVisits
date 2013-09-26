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
    public class TipoVisitaController : Controller
    {
        private CVisitsContext db = new CVisitsContext();

        //
        // GET: /TipoVisita/

        public ActionResult Index()
        {
            return View(db.TipoVisita.ToList());
        }

        //
        // GET: /TipoVisita/Details/5

        public ActionResult Details(int id = 0)
        {
            TipoVisita tipovisita = db.TipoVisita.Find(id);
            if (tipovisita == null)
            {
                return HttpNotFound();
            }
            return View(tipovisita);
        }

        //
        // GET: /TipoVisita/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TipoVisita/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoVisita tipovisita)
        {
            if (ModelState.IsValid)
            {
                db.TipoVisita.Add(tipovisita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipovisita);
        }

        //
        // GET: /TipoVisita/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TipoVisita tipovisita = db.TipoVisita.Find(id);
            if (tipovisita == null)
            {
                return HttpNotFound();
            }
            return View(tipovisita);
        }

        //
        // POST: /TipoVisita/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoVisita tipovisita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipovisita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipovisita);
        }

        //
        // GET: /TipoVisita/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TipoVisita tipovisita = db.TipoVisita.Find(id);
            if (tipovisita == null)
            {
                return HttpNotFound();
            }
            return View(tipovisita);
        }

        //
        // POST: /TipoVisita/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoVisita tipovisita = db.TipoVisita.Find(id);
            db.TipoVisita.Remove(tipovisita);
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