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
    public class AccionResponsable
    {
        //ID
        public int AccionResponsableID { get; set; }

        [Required]
        public string Nombre { get; set; }

        [EmailAddress]
        public string Email { get; set; }



        //---------------------------------------------------
        // RELACIONES
        //

        //A muchos...
        public virtual ICollection<Accion> Accion { get; set; }


    }
}