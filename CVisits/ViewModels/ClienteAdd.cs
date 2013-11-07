using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CVisits.Models;

namespace CVisits.ViewModels
{
    public class ClienteAdd
    {
        public Cliente Cliente { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }
    }
}