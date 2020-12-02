using DiseñoChatMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DiseñoChatMVC.Controllers
{
    public class ChatController : Controller
    {
        public  List<Usuario> InfoContactos = new List<Usuario>(); //info para la lista de contactos
        public  List<Mensaje> SalaMensajes = new List<Mensaje>();
        public List<Sala> InfoSalas = new List<Sala>();

        


        // GET: User
        public async Task<ActionResult> Contactos() //Lista de contactos 
        {
            
            string json = await GetApiJson("http://localhost:5000/api/user");
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json);

            foreach (var usuario in usuarios)
            {
                Usuario usuario1 = new Usuario()
                {
                    Id = usuario.Id,
                    NombreUsuario = usuario.NombreUsuario,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido
                };
                InfoContactos.Add(usuario1);
            }

            return View(InfoContactos);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public async Task<ActionResult> Login(FormCollection collection)
        {
            string json = await GetApiJson("http://localhost:5000/api/user");
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json);

            try
            {
                foreach (var usuario in usuarios)
                {
                     if (collection["NombreUsuario"] == usuario.NombreUsuario && collection["Password"] == usuario.Password)
                     {
                         return RedirectToAction("Contactos",usuario.Id);// ingresa
                     }
                }
                return View("Error");  //Contraseña Incorrecta o usuario no existente
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
        public async Task<ActionResult> Registro(FormCollection collection)
        {
            string json = await GetApiJson("http://localhost:5000/api/user");
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json);

            try
            {

                Usuario Registrar = new Usuario()
                {
                    NombreUsuario = collection["NombreUsuario"],
                    Password = collection["Password"],
                    Nombre = collection["Nombre"],
                    Apellido = collection["Apellido"]
                };
                // ver si el NombreUsuario ya ha sido registrado
                foreach (var usuario in usuarios)
                {
                    if (Registrar.NombreUsuario==usuario.NombreUsuario)
                    {
                        return View("UsuarioExistente");
                    }
                }
                InfoContactos.Add(Registrar);
                // mandar a DB
                await PostApiJsonUsuario("http://localhost:5000/api/user", Registrar);



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
        public ActionResult UsuarioExistente()
        {
            return View();
        }
        public async Task<ActionResult> Chat()
        {
            string json = await GetApiJson("http://localhost:5000/api/message");
            var mensajes = JsonConvert.DeserializeObject<List<Mensaje>>(json);
            await GetSalas();

            foreach (var mensaje in mensajes)
            {
                Mensaje mensajeSala = new Mensaje()
                {
                    Contenido = mensaje.Contenido,
                    Sala = mensaje.Sala,
                    Emisor = mensaje.Emisor,
                    HoraActual = mensaje.HoraActual
                };
                //------------------------------------
                foreach (var IdSala in InfoSalas)
                {
                    if (mensajeSala.Sala ==  IdSala.Id)
                    {
                        SalaMensajes.Add(mensajeSala);
                        
                    }

                }

            }

            return View(SalaMensajes);
        }
        public ActionResult Mensajeria()
        {
            return View();
        }
         
        // POST: User/Create
        [HttpPost]
        public async Task<ActionResult> Mensajeria(FormCollection collection)
        {

            try
            {
                await GetSalas();
                foreach (var sala in InfoSalas)
                {
                    Mensaje Enviar = new Mensaje()
                    {
                        Contenido = collection["Contenido"],
                        Usuario1 = sala.Usuario1,
                        Usuario2 = sala.Usuario2,
                        Sala = sala.Id,
                        HoraActual = DateTime.Now.ToString("hh:mm:ss")
                    };
                SalaMensajes.Add(Enviar);
                await PostApiJsonMensaje("http://localhost:5000/api/message", Enviar);

                }
                

                return RedirectToAction("Chat");
            }
            catch
            {
                return View();
            }
        }


        //Lectura base de datos
        public static async Task<string> GetApiJson(string url)
        {
            var http = new HttpClient();
            string respuesta = await http.GetStringAsync(url);

            return respuesta;
        }
        //Escritura base de datos
        public static async Task PostApiJsonUsuario(string url, Usuario json)
        {
            var http = new HttpClient();
            var enviar = await http.PostAsJsonAsync(url, json);

            enviar.EnsureSuccessStatusCode();
        }
        public static async Task PostApiJsonSala(string url, Sala json)
        {
            var http = new HttpClient();
            var enviar = await http.PostAsJsonAsync(url, json);

            enviar.EnsureSuccessStatusCode();
        }
        public static async Task PostApiJsonMensaje(string url, Mensaje json)
        {
            var http = new HttpClient();
            var enviar = await http.PostAsJsonAsync(url, json);

            enviar.EnsureSuccessStatusCode();
        }



        public async Task GetSalas()
        {
            //va a tener 2 usuarios 
            string json = await GetApiJson("http://localhost:5000/api/room");
            var salas = JsonConvert.DeserializeObject<List<Sala>>(json);

            foreach (var sala in salas)
            {
                Sala salaUsuarios = new Sala()
                {
                    Id = sala.Id,
                    Usuario1 = sala.Usuario1,
                    Usuario2 = sala.Usuario2
                };
                InfoSalas.Add(salaUsuarios);
            }
        }

        public async Task dummy()
        {
            string json = await GetApiJson("http://localhost:5000/api/room");
            var salas = JsonConvert.DeserializeObject<List<Sala>>(json);

            foreach (var sala in salas)
            {
                Sala salaUsuarios = new Sala()
                {
                    Id = sala.Id,
                    Usuario1 = sala.Usuario1,
                    Usuario2 = sala.Usuario2
                };
                InfoSalas.Add(salaUsuarios);
            }


        }
        
    }
}