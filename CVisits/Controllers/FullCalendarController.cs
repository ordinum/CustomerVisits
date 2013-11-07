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
    public class FullCalendarController : Controller
    {
        private CVisitsContext db = new CVisitsContext();

        //
        // GET: /FullCalendar/

        public ActionResult Index()
        {
            var visita = db.Visita.Include(v => v.EstadoVisita).Include(v => v.LineaProducto).Include(v => v.TipoVisita).Include(v => v.Cliente).Include(v => v.UserProfile);
            return View(visita.ToList());
        }


        // <summary>
        // Metodo que devuelve un Array de eventos en formato Json
        // summary>
        // <param name="start">Star Dateparam>
        // <param name="end">End Dateparam>
        // <returns>returns>
        public JsonResult GetEvents(double start, double end)
        {
            var startDateTime = FromUnixTimestamp(start);
            var endDateTime = FromUnixTimestamp(end);
            
            //Conexion a la Base de Datos
            //var events = from reps in context.CalEntries
            //             where reps.StartDateTime > startDateTime && reps.EndDateTime < endDateTime
            //             select reps;


            var events = db.Visita.Where(e => e.FechaPlanificada > startDateTime && e.FechaTermino < endDateTime);


            var clientList = new List<object>();
            foreach (var e in events)
            {
                clientList.Add(new
                {
                    //id = e.CalEntriesId,
                    //title = e.Description,
                    //description = e.Description,
                    //start = ConvertToTimestamp(e.StartDateTime),
                    //end = ConvertToTimestamp(e.EndDateTime),
                    //allDay = e.isAllDay,

                    id = e.VisitaID,
                    title = e.Descripcion,
                    description = e.Descripcion,
                    start = ConvertToTimestamp(e.FechaPlanificada),
                    end = ConvertToTimestamp(e.FechaTermino),    
                    allDay = e.EsTodoElDia,

                });
            } return Json(clientList.ToArray(), JsonRequestBehavior.AllowGet);
        }


        //Get all events by type...
        public JsonResult GetEventsByType(double start, double end, int typeid)
        {
            var startDateTime = FromUnixTimestamp(start);
            var endDateTime = FromUnixTimestamp(end);            

            var events = db.Visita.Where(e => e.FechaPlanificada > startDateTime && e.FechaTermino < endDateTime && e.TipoVisitaID == typeid);

            var clientList = new List<object>();

            foreach (var e in events)
            {
                clientList.Add(new
                {                    
                    id = e.VisitaID,
                    title = e.Descripcion,
                    description = e.Descripcion,
                    start = ConvertToTimestamp(e.FechaPlanificada),
                    end = ConvertToTimestamp(e.FechaTermino),
                    allDay = e.EsTodoElDia,

                });
            } return Json(clientList.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MoveEvent(double days, double minutes, int id)
        {
            
            var evento = db.Visita.Find(id);           

            evento.FechaPlanificada = evento.FechaPlanificada.AddDays(days);
            evento.FechaPlanificada = evento.FechaPlanificada.AddMinutes(minutes);
            evento.FechaTermino = evento.FechaTermino.AddDays(days);
            evento.FechaTermino = evento.FechaTermino.AddMinutes(minutes);

            
            db.Entry(evento).State = EntityState.Modified;
            db.SaveChanges();

            return Json("Done", JsonRequestBehavior.AllowGet);
        }

        public JsonResult MoveResize(double days, double minutes, int id)
        {

            var evento = db.Visita.Find(id);

            //evento.FechaPlanificada = evento.FechaPlanificada.AddDays(days);
            //evento.FechaPlanificada = evento.FechaPlanificada.AddMinutes(minutes);
            evento.FechaTermino = evento.FechaTermino.AddDays(days);
            evento.FechaTermino = evento.FechaTermino.AddMinutes(minutes);


            db.Entry(evento).State = EntityState.Modified;
            db.SaveChanges();
            return Json("Done", JsonRequestBehavior.AllowGet);
        }


        // <summary>
        // Convierte de Unix Timestamp a Datetime
        // summary>
        // <param name="timestamp">Date to convertparam>
        // <returns>returns>
        private static DateTime FromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        // <summary>
        // convierte de DateTime a UNIX Timestamp
        // summary>
        // <param name="value">Date to convertparam>
        // <returns>returns>
        private static double ConvertToTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            //return the total seconds (which is a UNIX timestamp)
            return (double)span.TotalSeconds;
        } 




        //
        // GET: /FullCalendar/Details/5

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
        // GET: /FullCalendar/Create

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
        // POST: /FullCalendar/Create

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
        // GET: /FullCalendar/Edit/5

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
        // POST: /FullCalendar/Edit/5

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
        // GET: /FullCalendar/Delete/5

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
        // POST: /FullCalendar/Delete/5

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