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
    public class Visita
    {

        public Visita()
        {
            this.FechaIngreso = DateTime.Now;
        }        

        //ID
        public int VisitaID { get; set; }

        public DateTime FechaIngreso { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Fecha Planificación")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "Debe ingresar una fecha de planificación")]
        public DateTime FechaPlanificada { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Fecha Término")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar una fecha de término")]
        public DateTime FechaTermino { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Required]
        public bool EsTodoElDia { get; set; }

        //---------------------------------------------------
        // RELACIONES
        //

        //A uno...
        public int EstadoVisitaID { get; set; }
        public int LineaProductoID { get; set; }
        public int TipoVisitaID { get; set; }
        public int ClienteID { get; set; }
        public int UserId { get; set; }

        public virtual EstadoVisita EstadoVisita { get; set; }
        public virtual LineaProducto LineaProducto { get; set; }
        public virtual TipoVisita TipoVisita { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        //A muchos...
        public virtual ICollection<Reporte> Reporte { get; set; }

    }
}