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
    public class MaquinaEquipo
    {
        //ID
        public int MaquinaEquipoID { get; set; }

        public string Tipo { get; set; }

        [Required]
        public string Descripcion { get; set; }



        //---------------------------------------------------
        // RELACIONES
        //

        //A muchos...        
    
    }
}