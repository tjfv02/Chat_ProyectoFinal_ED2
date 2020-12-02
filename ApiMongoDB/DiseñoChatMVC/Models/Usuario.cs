using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiseñoChatMVC.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}