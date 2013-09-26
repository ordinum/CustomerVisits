using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVisits.DAL;

namespace CVisits.Models
{
    public class TipoVisita
    {
        //ID
        public int TipoVisitaID { get; set; }

        [Required]
        public string Descripcion { get; set; }


        //---------------------------------------------------
        // RELACIONES
        //

        //A muchos...
        public virtual ICollection<Reporte> Reporte { get; set; }
        public virtual ICollection<Visita> Visita { get; set; }
    }
}