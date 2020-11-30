using DiseñoChatMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiseñoChatMVC.Controllers
{
    public class ChatController : Controller
    {
        public static List<Usuario> InfoContactos = new List<Usuario>(); //info para la lista de contactos
        public static List<Usuario> RegistroContactos = new List<Usuario>(); //Todos los contactos Registrados
        public static List<Mensaje> Mensajes = new List<Mensaje>();



        // GET: User
        public ActionResult Contactos() //Lista de contactos 
        {
            return View(InfoContactos);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost] // pruebas jsjs
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //    Usuario Agregar = new Usuario()
                //    {
                //        NombreUsuario = collection["NombreUsuario"],
                //        Nombre = collection["Nombre"],
                //        Apellido = collection["Apellido"]
                //    };
                //    InfoContactos.Add(Agregar);
                return RedirectToAction("Contactos");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                Usuario Loguear = new Usuario()
                {
                    NombreUsuario = collection["NombreUsuario"],
                    Password = collection["Password"]
                };

                //ver si ya esta el usuario registrado 

                if ((Loguear.NombreUsuario == InfoContactos[0].NombreUsuario && Loguear.Password == InfoContactos[0].Password) || (Loguear.NombreUsuario == " " && Loguear.Password == " "))
                {
                    return RedirectToAction("Contactos");// ingresa
                }
                else
                {
                    return View("Error"); //Contraseña Incorrecta o usuario no existente
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Registro()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Registro(FormCollection collection)
        {
            try
            {
                Usuario Registrar = new Usuario()
                {
                    Id = InfoContactos.Count,
                    NombreUsuario = collection["NombreUsuario"],
                    Password = collection["Password"],
                    Nombre = collection["Nombre"],
                    Apellido = collection["Apellido"]
                };

                InfoContactos.Add(Registrar);
                //RegistroContactos.Add(Registrar);

                return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View(Mensajes);
        }
        public ActionResult Mensajeria()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Mensajeria(FormCollection collection)
        {
            try
            {
                Mensaje Enviar = new Mensaje()
                {
                    Contenido = collection["Contenido"],
                    Usuario1 = "Tú", //collection["Usuario1"],
                    Usuario2 = collection["Usuario2"],
                    HoraAcual = DateTime.Now.ToString("hh:mm:ss")
                };
                Mensajes.Add(Enviar);

                return RedirectToAction("Chat");
            }
            catch
            {
                return View();
            }
        }
    }
}