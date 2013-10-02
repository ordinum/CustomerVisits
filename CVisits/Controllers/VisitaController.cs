using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CVisits.Models;
using CVisits.DAL;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;



namespace CVisits.Controllers
{
    public class VisitaController : Controller
    {
        private CVisitsContext db = new CVisitsContext();

        //
        // GET: /Visita/

        public ActionResult Index()
        {
            var visita = db.Visita.Include(v => v.EstadoVisita).Include(v => v.LineaProducto).Include(v => v.TipoVisita).Include(v => v.Cliente).Include(v => v.UserProfile);
            return View(visita.ToList());
        }

        //
        // GET: /Visita/Details/5

        public ActionResult Details(int id = 0)
        {
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        //
        // GET: /Visita/Create

        public ActionResult Create()
        {
            ViewBag.EstadoVisitaID = new SelectList(db.EstadoVisita, "EstadoVisitaID", "Descripcion");
            ViewBag.LineaProductoID = new SelectList(db.LineaProducto, "LineaProductoID", "Descripcion");
            ViewBag.TipoVisitaID = new SelectList(db.TipoVisita, "TipoVisitaID", "Descripcion");
            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Descripcion");
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Visita/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Visita visita)
        {
            if (ModelState.IsValid)
            {
                db.Visita.Add(visita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EstadoVisitaID = new SelectList(db.EstadoVisita, "EstadoVisitaID", "Descripcion", visita.EstadoVisitaID);
            ViewBag.LineaProductoID = new SelectList(db.LineaProducto, "LineaProductoID", "Descripcion", visita.LineaProductoID);
            ViewBag.TipoVisitaID = new SelectList(db.TipoVisita, "TipoVisitaID", "Descripcion", visita.TipoVisitaID);
            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Descripcion", visita.ClienteID);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", visita.UserId);
            return View(visita);
        }

        //
        // GET: /Visita/WeekPlanner
        public ActionResult WeekPlanner()
        {
            ViewBag.EstadoVisitaID = new SelectList(db.EstadoVisita, "EstadoVisitaID", "Descripcion");
            ViewBag.LineaProductoID = new SelectList(db.LineaProducto, "LineaProductoID", "Descripcion");
            ViewBag.TipoVisitaID = new SelectList(db.TipoVisita, "TipoVisitaID", "Descripcion");
            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Descripcion");
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Visita/WeekPlanner
        [HttpPost]
        public ActionResult WeekPlanner(IList<Visita> WeekVisits, int userid)
        {
            if (ModelState.IsValid)
            {
                foreach (Visita visit in WeekVisits)
                {
                    var visitRecord = new Visita();

                    visitRecord.ClienteID = visit.ClienteID;
                    visitRecord.EstadoVisitaID = visit.EstadoVisitaID;
                    visitRecord.LineaProductoID = visit.LineaProductoID;
                    visitRecord.TipoVisitaID = visit.TipoVisitaID;                    
                    visitRecord.Descripcion = visit.Descripcion;
                    visitRecord.EsTodoElDia = visit.EsTodoElDia;
                    visitRecord.FechaIngreso = DateTime.Today;//Ingresa el día actual
                    visitRecord.FechaPlanificada = visit.FechaPlanificada;
                    visitRecord.FechaTermino = visit.FechaTermino;
                    visitRecord.UserId = userid;//Guarda ID de usuario                    

                    db.Visita.Add(visitRecord);

                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }


            ViewBag.EstadoVisitaID = new SelectList(db.EstadoVisita, "EstadoVisitaID", "Descripcion");
            ViewBag.LineaProductoID = new SelectList(db.LineaProducto, "LineaProductoID", "Descripcion");
            ViewBag.TipoVisitaID = new SelectList(db.TipoVisita, "TipoVisitaID", "Descripcion");
            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Descripcion");
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName");
            
            return View(WeekVisits);
            
        }

        //
        // GET: /Visita/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoVisitaID = new SelectList(db.EstadoVisita, "EstadoVisitaID", "Descripcion", visita.EstadoVisitaID);
            ViewBag.LineaProductoID = new SelectList(db.LineaProducto, "LineaProductoID", "Descripcion", visita.LineaProductoID);
            ViewBag.TipoVisitaID = new SelectList(db.TipoVisita, "TipoVisitaID", "Descripcion", visita.TipoVisitaID);
            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Descripcion", visita.ClienteID);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", visita.UserId);
            return View(visita);
        }

        //
        // POST: /Visita/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Visita visita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstadoVisitaID = new SelectList(db.EstadoVisita, "EstadoVisitaID", "Descripcion", visita.EstadoVisitaID);
            ViewBag.LineaProductoID = new SelectList(db.LineaProducto, "LineaProductoID", "Descripcion", visita.LineaProductoID);
            ViewBag.TipoVisitaID = new SelectList(db.TipoVisita, "TipoVisitaID", "Descripcion", visita.TipoVisitaID);
            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Descripcion", visita.ClienteID);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", visita.UserId);
            return View(visita);
        }

        //
        // GET: /Visita/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        //
        // POST: /Visita/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visita visita = db.Visita.Find(id);
            db.Visita.Remove(visita);
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