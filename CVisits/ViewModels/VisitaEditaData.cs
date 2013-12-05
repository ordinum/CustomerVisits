using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CVisits.Models;

namespace CVisits.ViewModels
{
    public class VisitaEditaData
    {
        public Visita Visita { get; set; }
        public ICollection<Accion> Acciones { get; set; }

    }
}