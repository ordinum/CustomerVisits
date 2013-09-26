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

        public ActionResult Backend()
        {
            return new Dpc().CallBack(this);
        }

        class Dpc : DayPilotCalendar
        {
            CVisitsContext db = new CVisitsContext();

            protected override void OnInit(InitArgs e)
            {
                Update(CallBackUpdateType.Full);
            }

            //Redimensionado de Evento...
            protected override void OnEventResize(EventResizeArgs e)
            {
                //Convierte el ID del plugin a entero...
                int idevento = Convert.ToInt32(e.Id);
                
                //Edición de entrada...
                Visita visita = db.Visita.Find(idevento);
                visita.FechaPlanificada = Convert.ToDateTime(e.NewStart);//Conversiones String a Fecha
                visita.FechaTermino = Convert.ToDateTime(e.NewEnd);
                db.Entry(visita).State = EntityState.Modified;//Cambio de estado
                db.SaveChanges();//Guarda cambios...
                Update();//Actualiza calendario...

            }

            //Movimiento de Evento...
            protected override void OnEventMove(EventMoveArgs e)
            {
                int idevento = Convert.ToInt32(e.Id);

                //Edición de entrada...
                Visita visita = db.Visita.Find(idevento);
                visita.FechaPlanificada = Convert.ToDateTime(e.NewStart);//Conversiones String a Fecha
                visita.FechaTermino = Convert.ToDateTime(e.NewEnd);
                db.Entry(visita).State = EntityState.Modified;//Cambio de estado
                db.SaveChanges();//Guarda cambios...
                Update();//Actualiza calendario...
            }

            //Creación de Evento...
            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                //Instancia para nuevo objeto de tipo VISITA
                Visita visita = new Visita();

                //Asignación valores...
                visita.FechaIngreso = DateTime.Today;
                visita.FechaPlanificada = e.Start;
                visita.FechaTermino = e.End;
                visita.Descripcion = (string)e.Data["name"];
                visita.LineaProductoID = 1;
                visita.TipoVisitaID = 1;
                visita.UserId = 1;
                visita.EstadoVisitaID = 1;
                visita.ClienteID = 1;

                db.Visita.Add(visita);
                db.SaveChanges();
                Update();
                
            }

            //Al terminar, actualiza calendario...
            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                Events = from ev in db.Visita select ev;

                DataIdField = "VisitaID";
                DataTextField = "Descripcion";
                DataStartField = "FechaPlanificada";
                DataEndField = "FechaTermino";
            }
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