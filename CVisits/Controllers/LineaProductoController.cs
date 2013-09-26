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
    public class LineaProductoController : Controller
    {
        private CVisitsContext db = new CVisitsContext();

        //
        // GET: /LineaProducto/

        public ActionResult Index()
        {
            return View(db.LineaProducto.ToList());
        }

        //
        // GET: /LineaProducto/Details/5

        public ActionResult Details(int id = 0)
        {
            LineaProducto lineaproducto = db.LineaProducto.Find(id);
            if (lineaproducto == null)
            {
                return HttpNotFound();
            }
            return View(lineaproducto);
        }

        //
        // GET: /LineaProducto/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LineaProducto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LineaProducto lineaproducto)
        {
            if (ModelState.IsValid)
            {
                db.LineaProducto.Add(lineaproducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lineaproducto);
        }

        //
        // GET: /LineaProducto/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LineaProducto lineaproducto = db.LineaProducto.Find(id);
            if (lineaproducto == null)
            {
                return HttpNotFound();
            }
            return View(lineaproducto);
        }

        //
        // POST: /LineaProducto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LineaProducto lineaproducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lineaproducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lineaproducto);
        }

        //
        // GET: /LineaProducto/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LineaProducto lineaproducto = db.LineaProducto.Find(id);
            if (lineaproducto == null)
            {
                return HttpNotFound();
            }
            return View(lineaproducto);
        }

        //
        // POST: /LineaProducto/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LineaProducto lineaproducto = db.LineaProducto.Find(id);
            db.LineaProducto.Remove(lineaproducto);
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