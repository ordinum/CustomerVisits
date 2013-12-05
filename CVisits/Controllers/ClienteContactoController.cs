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
    public class ClienteContactoController : Controller
    {
        private CVisitsContext db = new CVisitsContext();

        //
        // GET: /ClienteContacto/

        public ActionResult Index()
        {
            var clientecontacto = db.ClienteContacto.Include(c => c.Cliente);
            return View(clientecontacto.ToList());
        }

        //
        // GET: /ClienteContacto/Details/5

        public ActionResult Details(int id = 0)
        {
            ClienteContacto clientecontacto = db.ClienteContacto.Find(id);
            if (clientecontacto == null)
            {
                return HttpNotFound();
            }
            return View(clientecontacto);
        }

        //
        // GET: /ClienteContacto/Create

        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Descripcion");
            return View();
        }

        //
        // POST: /ClienteContacto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteContacto clientecontacto)
        {
            if (ModelState.IsValid)
            {
                db.ClienteContacto.Add(clientecontacto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Descripcion", clientecontacto.ClienteID);
            return View(clientecontacto);
        }

        //
        // GET: /ClienteContacto/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ClienteContacto clientecontacto = db.ClienteContacto.Find(id);
            if (clientecontacto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Descripcion", clientecontacto.ClienteID);
            return View(clientecontacto);
        }

        //
        // POST: /ClienteContacto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteContacto clientecontacto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientecontacto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Descripcion", clientecontacto.ClienteID);
            return View(clientecontacto);
        }

        //
        // GET: /ClienteContacto/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ClienteContacto clientecontacto = db.ClienteContacto.Find(id);
            if (clientecontacto == null)
            {
                return HttpNotFound();
            }
            return View(clientecontacto);
        }

        //
        // POST: /ClienteContacto/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClienteContacto clientecontacto = db.ClienteContacto.Find(id);
            db.ClienteContacto.Remove(clientecontacto);
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