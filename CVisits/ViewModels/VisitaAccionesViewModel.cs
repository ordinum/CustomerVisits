using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CVisits.Models;

namespace CVisits.ViewModels
{
    public class VisitaAccionesViewModel
    {

        public VisitaAccionesViewModel()
        {
            this.Visita = new Visita();
            this.Acciones = new List<Accion>() { new Accion() };
        }

        public Visita Visita { get; set; }
        public List<Accion> Acciones { get; set; }        

    }
}