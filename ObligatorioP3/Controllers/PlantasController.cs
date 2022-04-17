using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasosUso;
using Dominio.Entidades;
using ObligatorioP3.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ObligatorioP3.Controllers
{
    public class PlantasController : Controller
    {
        public IManejadorPlantas ManejadorPlantas { get; set; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        public PlantasController(IManejadorPlantas manejador, IWebHostEnvironment whenv)
        {
            ManejadorPlantas = manejador;
            WebHostEnvironment = whenv;
        }

        // GET: PlantasController
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                IEnumerable<Planta> listaPlantas = ManejadorPlantas.TraerTodasLasPlantas();
                return View(listaPlantas);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: PlantasController/Details/5
        public ActionResult Details(int idPlanta)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                Planta plantaBuscada = ManejadorPlantas.BuscarPlantaPorId(idPlanta);
                return View(plantaBuscada);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: PlantasController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                ViewModelPlanta vmp = new ViewModelPlanta();
                vmp.Tipos = ManejadorPlantas.TraerTodosLosTipos();
                vmp.Ambientes = ManejadorPlantas.TraerTodosLosAmbientes();
                vmp.Iluminaciones = ManejadorPlantas.TraerTodasLasIluminaciones();
                return View(vmp);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: PlantasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelPlanta vmp)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                try
                {
                    //string nombreArchivo = vmp.Imagen.FileName;
                    string nombreArchivo = vmp.Planta.NombreCientifico + "001.jpg";

                    if (nombreArchivo.Contains(" "))
                    {
                        nombreArchivo = nombreArchivo.Replace(" ", "_");
                    }

                    vmp.Planta.Foto = nombreArchivo;

                    bool creadaOK = ManejadorPlantas.AgregarNuevaPlanta(vmp.Planta, vmp.IdTipoSeleccionado, vmp.IdAmbienteSeleccionado, vmp.IdIluminacionSeleccionada);
                    if (creadaOK)
                    {

                        string rutaRaizApp = WebHostEnvironment.WebRootPath;
                        rutaRaizApp = Path.Combine(rutaRaizApp, "imagenes");
                        string rutaCompleta = Path.Combine(rutaRaizApp, nombreArchivo);
                        FileStream stream = new FileStream(rutaCompleta, FileMode.Create);
                        vmp.Imagen.CopyTo(stream);

                        ViewBag.Resultado = "Planta dada de alta correctamente";
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        ViewBag.Resultado = "Error al dar el alta";
                        return View(vmp);
                    }
                }
                catch
                {
                    return View(vmp);

                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: PlantasController/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                Planta plantaAEditar = ManejadorPlantas.BuscarPlantaPorId(id);
                return View(plantaAEditar);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: PlantasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Planta plantaAEditar)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                try
                {
                    bool modificadoOK = ManejadorPlantas.ActualizarPlanta(plantaAEditar);
                    if (modificadoOK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = "No se pudo editar la planta";
                    }
                    return View(plantaAEditar);
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: PlantasController/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                Planta plantaABorrar = ManejadorPlantas.BuscarPlantaPorId(id);
                return View(plantaABorrar);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: PlantasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Planta plantaABorrar)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                try
                {
                    bool eliminadoOK = ManejadorPlantas.DarDeBajaPlanta(plantaABorrar.IdPlanta);
                    if (eliminadoOK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = "No se pudo eliminar la planta";
                        return View();
                    }
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult BuscarPorTexto(string texto)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                IEnumerable<Planta> plantasBuscada = ManejadorPlantas.BuscarPlantasPorTexto(texto);
                return View(plantasBuscada);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}
