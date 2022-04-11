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
    public class TiposController : Controller
    {

        public IManejadorTipos ManejadorTipos { get; set; }

        public TiposController(IManejadorTipos manejador)
        {
            ManejadorTipos = manejador;
        }

        // GET: TipoController
        public ActionResult Index()
        {
            IEnumerable<Tipo> tipos = ManejadorTipos.TraerTodos();
            if (tipos.Count() == 0)
            {            
                ViewBag.Resultado = "No hay tipos de plantas ingresados.";                
            }
            return View(tipos);
        }

        // GET: TipoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TipoController/Create
        public ActionResult Create()
        {
            Tipo tipo = new Tipo();
            return View(tipo);
        }

        // POST: TipoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tipo tipo)
        {
            try
            {
                bool ok = ManejadorTipos.AgregarNuevoTipo(tipo);

                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Resultado = "No se pudo ingresar el tipo.";
                    return View(tipo);
                }
            }
            catch
            {
                return View(tipo);
            }
        }

        // GET: TipoController/Edit/5
        public ActionResult Edit(string name)
        {
            Tipo tipo= ManejadorTipos.BuscarTipoPorNombre(name);
            return View(tipo);
        }

        // POST: TipoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tipo tipo)
        {
            try
            {
                bool ok = ManejadorTipos.ActualizarDescripcionTipo(tipo);
                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Resultado = "No se pudo realizar la actualización de la descripción.";
                    return View(tipo);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoController/Delete/5
        public ActionResult Delete(string name)
        {
            Tipo tipo = ManejadorTipos.BuscarTipoPorNombre(name);
            return View(tipo);
        }

        // POST: TipoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Tipo tipo)
        {
            try
            {
                bool ok = ManejadorTipos.DarDeBajaTipo(tipo.Nombre);

                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Resultado = "No se pudo eliminar el tipo";
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
