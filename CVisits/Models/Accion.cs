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
    public class Accion
    {
        //ID
        public int AccionID { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Acción")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe ingresar una fecha")]
        public DateTime FechaAccion { get; set; }

        [Required]
        public string Descripcion { get; set; }



        //---------------------------------------------------
        // RELACIONES
        //

        //A uno...        
        public int AccionResponsableID { get; set; }
        public int AccionEstadoID { get; set; }
        public int VisitaID { get; set; }
        
        public virtual AccionResponsable AccionResponsable { get; set; }
        public virtual AccionEstado AccionEstado { get; set; }
        public virtual Visita Visita { get; set; }

        //A muchos...                
    
    }
}