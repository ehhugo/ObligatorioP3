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
        public IWebHostEnvironment WebHostEnvironment { get; private set; }

        public PlantasController (IManejadorPlantas manejador)
        {
            ManejadorPlantas = manejador;
        }

        // GET: PlantasController
        public ActionResult Index()
        {
            IEnumerable<Planta> listaPlantas = ManejadorPlantas.TraerTodasLasPlantas();
            return View(listaPlantas);
        }

        // GET: PlantasController/Details/5
        public ActionResult Details(int idPlanta)
        {
            Planta plantaBuscada = ManejadorPlantas.BuscarPlantaPorId(idPlanta);
            return View(plantaBuscada);
        }

        // GET: PlantasController/Create
        public ActionResult Create()
        {
            ViewModelPlanta vmp = new ViewModelPlanta();
            vmp.Tipos = ManejadorPlantas.TraerTodosLosTipos();
            vmp.Ambientes = ManejadorPlantas.TraerTodosLosAmbientes();
            vmp.Iluminaciones = ManejadorPlantas.TraerTodasLasIluminaciones();
            return View(vmp);
        }

        // POST: PlantasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelPlanta vmp)
        {
            try
            {
                string nombreArchivo = vmp.Imagen.FileName;
                nombreArchivo = vmp.Planta.IdPlanta + "_" + nombreArchivo;
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
                return View();
            }
        }

        // GET: PlantasController/Edit/5
        public ActionResult Edit(int id)
        {
            Planta plantaAEditar = ManejadorPlantas.BuscarPlantaPorId(id);
            return View(plantaAEditar);
        }

        // POST: PlantasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Planta plantaAEditar)
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

        // GET: PlantasController/Delete/5
        public ActionResult Delete(int id)
        {
            Planta plantaABorrar = ManejadorPlantas.BuscarPlantaPorId(id);
            return View(plantaABorrar);
        }

        // POST: PlantasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Planta plantaABorrar)
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
    }
}
