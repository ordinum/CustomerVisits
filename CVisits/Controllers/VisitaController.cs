using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CVisits.Models;
using CVisits.ViewModels;
using CVisits.DAL;
using CVisits.Mailers;


namespace CVisits.Controllers
{
    public class VisitaController : Controller
    {
        private CVisitsContext db = new CVisitsContext();


        private IUserMailer _userMailer = new UserMailer();
        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }

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

                //Notify's Client that a Visit has been scheduled via email...
                if (visita.NotificaCliente.Equals(true))
                {
                    //Get data first... VisitScheduled(string clientsmail, string clientsname, string description, string type, string visitorname, DateTime visitsdate)                
                    var clientsmail = db.ClienteContacto.Where(c => c.ClienteID == visita.ClienteID).Select(c => c.Email).SingleOrDefault().ToString();
                    var clientsname = db.Cliente.Where(c => c.ClienteID == visita.ClienteID).Select(c => c.Descripcion).SingleOrDefault().ToString();
                    var visittype = db.TipoVisita.Where(t => t.TipoVisitaID == visita.TipoVisitaID).Select(t => t.Descripcion).SingleOrDefault().ToString();
                    var visitorname = db.UserProfiles.Where(u => u.UserId == visita.UserId).Select(u => u.UserName).SingleOrDefault().ToString();

                    UserMailer.VisitScheduled(clientsmail, clientsname, visita.Descripcion, visittype, visitorname, visita.FechaPlanificada).Send();//...Sends mail
                }
                                
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
        // GET: /Vsita/VisitsDaysSelection
        public ActionResult VisitsDaysSelection()
        {
            return View();
        }
      
        //
        // GET: /Visita/WeekPlanner
        public ActionResult WeekPlanner(DateTime? StartDate, DateTime? EndDate)
        {
            var estadovisita = db.EstadoVisita.ToList();
            var lineaproducto = db.LineaProducto.ToList();


            

            ViewBag.EstadoVisitaID = db.EstadoVisita.ToList();
            ViewBag.LineaProductoID = db.LineaProducto.ToList();
            ViewBag.TipoVisitaID = db.TipoVisita.ToList();
            ViewBag.ClienteID = db.Cliente.ToList();
            ViewBag.UserId = db.UserProfiles.ToList();

            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

            return View();
        }

        //
        // POST: /Visita/WeekPlanner
        [HttpPost]
        public ActionResult WeekPlanner(IList<Visita> WeekVisits)
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
                    visitRecord.FechaIngreso = visit.FechaIngreso;
                    visitRecord.FechaPlanificada = visit.FechaPlanificada;
                    visitRecord.FechaTermino = visit.FechaTermino;
                    visitRecord.UserId = visit.UserId;

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

            VisitaEditaData viewModel = new VisitaEditaData();

            //Asigna VISITA a ViewModel...
            viewModel.Visita = visita;
            viewModel.Acciones = db.Accion.Where(a => a.VisitaID == id).ToList();
            
            if (visita == null)
            {
                return HttpNotFound();
            }

            ViewBag.EstadoVisitaID = new SelectList(db.EstadoVisita, "EstadoVisitaID", "Descripcion", visita.EstadoVisitaID);
            ViewBag.LineaProductoID = new SelectList(db.LineaProducto, "LineaProductoID", "Descripcion", visita.LineaProductoID);
            ViewBag.TipoVisitaID = new SelectList(db.TipoVisita, "TipoVisitaID", "Descripcion", visita.TipoVisitaID);
            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Descripcion", visita.ClienteID);
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", visita.UserId);
            

            return View(viewModel);
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


        // GET: /AsignarAcciones?VisitaID=
        public ActionResult AsignarAcciones(int VisitaID)
        {
            VisitaAccionesViewModel viewModel = new VisitaAccionesViewModel();

            var visita = db.Visita.Find(VisitaID);

            viewModel.Visita = visita;

            ViewBag.Cliente = db.Cliente.Where(c => c.ClienteID == visita.ClienteID).Select(c => c.Descripcion).Single().ToString();
            ViewBag.LineaProducto = db.LineaProducto.Where(c => c.LineaProductoID == visita.LineaProductoID).Select(c => c.Descripcion).Single().ToString();
            ViewBag.TipoVisita = db.TipoVisita.Where(t => t.TipoVisitaID == visita.TipoVisitaID).Select(t => t.Descripcion).Single().ToString();

                    
            ViewBag.AccionResponsableID = new SelectList(db.AccionResponsable, "AccionResponsableID", "Nombre");
            ViewBag.AccionEstadoID = new SelectList(db.AccionEstado, "AccionEstadoID", "Descripcion");
             
            return View(viewModel);
        }

        [HttpPost]        
        public ActionResult AsignarAcciones(VisitaAccionesViewModel viewModel)
        {
            int VisitaID = viewModel.Visita.VisitaID;

            foreach (var item in viewModel.Acciones)
            {
                Accion accion = new Accion();
                accion.AccionEstadoID = item.AccionEstadoID;
                accion.AccionResponsableID = item.AccionResponsableID;
                accion.FechaAccion = item.FechaAccion;
                accion.Descripcion = item.Descripcion;
                accion.VisitaID = VisitaID;

                db.Accion.Add(accion);
            }

            db.SaveChanges();

            return RedirectToAction("Edit", "Visita", new { id = viewModel.Visita.VisitaID });
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