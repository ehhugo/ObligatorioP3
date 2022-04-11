using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasosUso;
using Dominio.Entidades;

namespace ObligatorioP3.Controllers
{
    public class PlantasController : Controller
    {
        public IManejadorPlantas ManejadorPlantas { get; set; }
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
            Planta planta = new Planta();
            return View(planta);
        }

        // POST: PlantasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Planta plantaACrear)
        {
            try
            {
                bool creadaOK = ManejadorPlantas.AgregarNuevaPlanta(plantaACrear);
                if (creadaOK)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Error al dar el alta";
                    return View(plantaACrear);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantasController/Edit/5
        public ActionResult Edit(int idPlantaAEditar)
        {
            Planta plantaAEditar = ManejadorPlantas.BuscarPlantaPorId(idPlantaAEditar);
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
        public ActionResult Delete(int idPlanta)
        {
            Planta plantaABorrar = ManejadorPlantas.BuscarPlantaPorId(idPlanta);
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
