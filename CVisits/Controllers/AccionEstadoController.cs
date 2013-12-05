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
    public class AccionEstadoController : Controller
    {
        private CVisitsContext db = new CVisitsContext();

        //
        // GET: /AccionEstado/

        public ActionResult Index()
        {
            return View(db.AccionEstado.ToList());
        }

        //
        // GET: /AccionEstado/Details/5

        public ActionResult Details(int id = 0)
        {
            AccionEstado accionestado = db.AccionEstado.Find(id);
            if (accionestado == null)
            {
                return HttpNotFound();
            }
            return View(accionestado);
        }

        //
        // GET: /AccionEstado/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AccionEstado/Create

        [HttpPost]
        public ActionResult Create(AccionEstado accionestado)
        {
            if (ModelState.IsValid)
            {
                db.AccionEstado.Add(accionestado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accionestado);
        }

        //
        // GET: /AccionEstado/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AccionEstado accionestado = db.AccionEstado.Find(id);
            if (accionestado == null)
            {
                return HttpNotFound();
            }
            return View(accionestado);
        }

        //
        // POST: /AccionEstado/Edit/5

        [HttpPost]
        public ActionResult Edit(AccionEstado accionestado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accionestado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accionestado);
        }

        //
        // GET: /AccionEstado/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AccionEstado accionestado = db.AccionEstado.Find(id);
            if (accionestado == null)
            {
                return HttpNotFound();
            }
            return View(accionestado);
        }

        //
        // POST: /AccionEstado/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            AccionEstado accionestado = db.AccionEstado.Find(id);
            db.AccionEstado.Remove(accionestado);
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