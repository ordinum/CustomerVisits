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
    public class Pais
    {
        //ID
        public int PaisID { get; set; }

        [Required]
        public string Nombre { get; set; }



        //---------------------------------------------------
        // RELACIONES
        //
        
        //A muchos...
        public virtual ICollection<Reporte> Reporte { get; set; }


    }
}