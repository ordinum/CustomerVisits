using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CVisits.Models;

namespace CVisits.ViewModels
{
    public class IndexView
    {
        public IEnumerable<Visita> Next7Visits { get; set; }
    }
}