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
    public class ClienteFaena
    {

        //ID
        public int ClienteFaenaID { get; set; }

        [Required]
        public string Descripcion { get; set; }



        //---------------------------------------------------
        // RELACIONES
        //

        //A uno...
        public int ClienteID { get; set; }

        public virtual Cliente Cliente { get; set; }

    }
}