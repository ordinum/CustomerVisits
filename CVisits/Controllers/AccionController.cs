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
    public class AccionController : Controller
    {
        private CVisitsContext db = new CVisitsContext();

        //
        // GET: /Accion/

        public ActionResult Index()
        {
            var accion = db.Accion.Include(a => a.AccionResponsable).Include(a => a.AccionEstado);
            return View(accion.ToList());
        }

        //
        // GET: /Accion/Details/5

        public ActionResult Details(int id = 0)
        {
            Accion accion = db.Accion.Find(id);
            if (accion == null)
            {
                return HttpNotFound();
            }
            return View(accion);
        }

        //
        // GET: /Accion/Create

        public ActionResult Create()
        {            
            ViewBag.AccionResponsableID = new SelectList(db.AccionResponsable, "AccionResponsableID", "Nombre");
            ViewBag.AccionEstadoID = new SelectList(db.AccionEstado, "AccionEstadoID", "Descripcion");
            return View();
        }

        //
        // POST: /Accion/Create

        [HttpPost]
        public ActionResult Create(Accion accion)
        {
            if (ModelState.IsValid)
            {
                db.Accion.Add(accion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.AccionResponsableID = new SelectList(db.AccionResponsable, "AccionResponsableID", "Nombre", accion.AccionResponsableID);
            ViewBag.AccionEstadoID = new SelectList(db.AccionEstado, "AccionEstadoID", "Descripcion", accion.AccionEstadoID);
            return View(accion);
        }

        //
        // GET: /Accion/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Accion accion = db.Accion.Find(id);
            if (accion == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.AccionResponsableID = new SelectList(db.AccionResponsable, "AccionResponsableID", "Nombre", accion.AccionResponsableID);
            ViewBag.AccionEstadoID = new SelectList(db.AccionEstado, "AccionEstadoID", "Descripcion", accion.AccionEstadoID);
            return View(accion);
        }

        //
        // POST: /Accion/Edit/5

        [HttpPost]
        public ActionResult Edit(Accion accion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.AccionResponsableID = new SelectList(db.AccionResponsable, "AccionResponsableID", "Nombre", accion.AccionResponsableID);
            ViewBag.AccionEstadoID = new SelectList(db.AccionEstado, "AccionEstadoID", "Descripcion", accion.AccionEstadoID);
            return View(accion);
        }

        //
        // GET: /Accion/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Accion accion = db.Accion.Find(id);
            if (accion == null)
            {
                return HttpNotFound();
            }
            return View(accion);
        }

        //
        // POST: /Accion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Accion accion = db.Accion.Find(id);
            db.Accion.Remove(accion);
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