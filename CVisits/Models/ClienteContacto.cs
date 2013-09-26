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
    public class ClienteContacto
    {
        //ID
        public int ClienteContactoID { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public string Telefono { get; set; }



        //---------------------------------------------------
        // RELACIONES
        //

        //A uno...
        public int ClienteID { get; set; }

        public virtual Cliente Cliente { get; set; }

    }
}