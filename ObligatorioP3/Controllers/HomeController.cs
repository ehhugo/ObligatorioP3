using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ObligatorioP3.Models;
using Microsoft.AspNetCore.Http;
using Dominio.Entidades;
using CasosUso;

namespace ObligatorioP3.Controllers
{
    public class HomeController : Controller
    {
        public IManejadorUsuarios ManejadorUsuarios { get; set; }

        public HomeController(IManejadorUsuarios manejador)
        {
            ManejadorUsuarios = manejador;
        }
        /*
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        */
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UL") == null)
            {             
            Usuario usuario = new Usuario();
                return View(usuario);           
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(Usuario u)
        {
            if (HttpContext.Session.GetString("UL") == null)
            {
                Usuario usuarioLogueado = ManejadorUsuarios.UsuarioALoguear(u);

                if (usuarioLogueado != null)
                {

                    HttpContext.Session.SetString("UL", usuarioLogueado.Mail);
                    HttpContext.Session.SetString("UPass", usuarioLogueado.Contrasenia);
                    HttpContext.Session.SetInt32("Uid", usuarioLogueado.idUsuario);
                    //ViewBag.msg = "Logueado correctamente";
                    return RedirectToAction("Index", "Tipos");

                }
                else
                {
                    ViewBag.msg = "Error al iniciar sesión, mail o contraseña incorrectos.";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Tipos");
            }
        }


        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logout(string n)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                HttpContext.Session.Clear();

                return RedirectToAction("Login", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}
