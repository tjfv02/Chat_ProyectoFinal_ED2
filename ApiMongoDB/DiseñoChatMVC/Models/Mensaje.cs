using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiseñoChatMVC.Models
{
    public class Mensaje
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public string Usuario1 { get; set; }
        public string Usuario2 { get; set; }
        public string HoraAcual { get; set; }

    }
}