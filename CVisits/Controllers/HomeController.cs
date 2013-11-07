using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CVisits.DAL;
using CVisits.Models;
using CVisits.ViewModels;

namespace CVisits.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private CVisitsContext db = new CVisitsContext();

        public ActionResult Index()
        {
            DateTime Today = DateTime.Today;
            DateTime Next7Days = Today.AddDays(7);

            ViewBag.Today = Today.ToShortDateString();
            ViewBag.Next7Days = Next7Days.ToShortDateString();

            var viewModel = new IndexView();

            viewModel.Next7Visits = db.Visita.Where(v => v.FechaPlanificada > Today && v.FechaPlanificada < Next7Days).ToList();            
            
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
