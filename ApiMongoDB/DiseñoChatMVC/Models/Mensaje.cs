﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiseñoChatMVC.Models
{
    public class Mensaje
    {
        public string Id { get; set; }
        public string Contenido { get; set; }
        public string Usuario1 { get; set; }
        public string Usuario2 { get; set; }
        public string HoraActual { get; set; }
        public string Sala { get; set; } //Id de la sala
        public string Emisor { get; set; } // id usuario que escribe
    }
}