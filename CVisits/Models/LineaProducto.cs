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
    public class LineaProducto
    {
        //ID
        public int LineaProductoID { get; set; }
        
        [Required]
        public string Descripcion { get; set; }



        //---------------------------------------------------
        // RELACIONES
        //

        //A muchos...
        public virtual ICollection<Visita> Visita { get; set; }

    }
}