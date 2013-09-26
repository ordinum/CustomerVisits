using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CVisits.DAL;

namespace CVisits.Models
{
    public class Reporte
    {
        
        //ID
        public int ReporteID { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Inicio")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar una fecha")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Término")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar una fecha")]
        public DateTime FechaTermino { get; set; }

        
        public int BussinesPotential { get; set; }

        public string CFC { get; set; }

        [Required]
        public string Descripcion { get; set; }


        //---------------------------------------------------
        // RELACIONES
        //


        //A uno...
        public int VisitaID { get; set; }
        public int TipoVisitaID { get; set; }
        public int MaquinaEquipoID { get; set; }                           
        public int PaisID { get; set; }
        public int UserId { get; set; }

        public virtual Visita Visita { get; set; }
        public virtual TipoVisita TipoVisita { get; set; }
        public virtual MaquinaEquipo MaquinaEquipo { get; set; }        
        public virtual Pais Pais { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        
        //A muchos...
        public virtual ICollection<Accion> Accion { get; set; }


    }
}