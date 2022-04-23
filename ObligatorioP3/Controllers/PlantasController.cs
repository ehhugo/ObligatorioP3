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

        #region Create
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
                    if (!ManejadorPlantas.BuscarPlantaPorNombreCientifico(vmp.Planta.NombreCientifico))
                    {
                        //string nombreArchivo = vmp.Imagen.FileName;
                        string extension = Path.GetExtension(vmp.Imagen.FileName);
                        string nombreArchivo="";

                        if (extension.Contains("jpg") || extension.Contains("png"))
                        {
                        nombreArchivo = vmp.Planta.NombreCientifico + $"001{extension}";
                        }

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
                    else
                    {
                        ViewBag.Resultado = "Error al dar el alta, el nombre científico ya existe.";
                        vmp.Tipos = ManejadorPlantas.TraerTodosLosTipos();
                        vmp.Ambientes = ManejadorPlantas.TraerTodosLosAmbientes();
                        vmp.Iluminaciones = ManejadorPlantas.TraerTodasLasIluminaciones();
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
        #endregion

        #region Edit
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
        #endregion

        #region Delete
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

        #endregion

        #region Buscar por Texto, Tipo y Ambiente
        public ActionResult BuscarPorTexto()
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarPorTexto(string textoBuscado)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                if (textoBuscado != null)
                {
                    try
                    {
                        IEnumerable<Planta> plantasBuscadas = ManejadorPlantas.BuscarPlantasPorTexto(textoBuscado);
                        if (plantasBuscadas.Count() > 0)
                        {
                            return View(plantasBuscadas);
                        }
                        else
                        {
                            ViewBag.Resultado = "No se encontraron plantas";
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
                    ViewBag.Resultado = "Ingrese texto.";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult BuscarPorTipo()
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                ViewBag.Tipos = ManejadorPlantas.TraerTodosLosTipos();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: PlantasController/BuscarPorTipo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarPorTipo(int IdTipoSeleccionado)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                ViewBag.Tipos = ManejadorPlantas.TraerTodosLosTipos();
                try
                {
                    if (IdTipoSeleccionado != 0)
                    {
                        IEnumerable<Planta> plantasBuscadas = ManejadorPlantas.BuscarPlantasPorTipo(IdTipoSeleccionado);
                        if (plantasBuscadas.Count() > 0)
                        {
                            ViewBag.Plantas = plantasBuscadas;
                            return View(ViewBag.Plantas, ViewBag.Tipos);
                        }
                        else
                        {
                            ViewBag.Resultado = "No se encontraron plantas";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Resultado = "Seleccione un tipo";
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

        public ActionResult BuscarPorAmbiente()
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                ViewBag.Ambientes = ManejadorPlantas.TraerTodosLosAmbientes();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: PlantasController/BuscarPorTexto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarPorAmbiente(int IdAmbienteSeleccionado)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                ViewBag.Ambientes = ManejadorPlantas.TraerTodosLosAmbientes();
                try
                {
                    if (IdAmbienteSeleccionado != 0)
                    {
                        IEnumerable<Planta> plantasBuscadas = ManejadorPlantas.BuscarPlantasPorAmbiente(IdAmbienteSeleccionado);
                        if (plantasBuscadas.Count() > 0)
                        {
                            ViewModelPlanta vmp = new ViewModelPlanta();
                            vmp.Ambientes = ManejadorPlantas.TraerTodosLosAmbientes();
                            ViewBag.Ambientes = vmp.Ambientes;
                            ViewBag.Plantas = plantasBuscadas;
                            return View(ViewBag.Plantas, ViewBag.Ambientes);
                        }
                        else
                        {
                            ViewBag.Resultado = "No se encontraron plantas";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Resultado = "Seleccione un ambiente";
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
        #endregion

        #region Plantas mas alta y Planta mas baja
        public ActionResult BuscarPlantasMasBajas()
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarPlantasMasBajas(double centimetros)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                if (centimetros > 0)
                {
                    try
                    {
                        IEnumerable<Planta> plantasBuscadas = ManejadorPlantas.BuscarPlantasMasBajas(centimetros);
                        if (plantasBuscadas.Count() > 0)
                        {
                            return View(plantasBuscadas);
                        }
                        else
                        {
                            ViewBag.Resultado = "No se encontraron plantas";
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
                    ViewBag.Resultado = "Ingrese un número válido";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }


        public ActionResult BuscarPlantasMasAltas()
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarPlantasMasAltas(double centimetros)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                if (centimetros > 0)
                {
                    try
                    {
                        IEnumerable<Planta> plantasBuscadas = ManejadorPlantas.BuscarPlantasMasAltas(centimetros);
                        if (plantasBuscadas.Count() > 0)
                        {
                            return View(plantasBuscadas);
                        }
                        else
                        {
                            ViewBag.Resultado = "No se encontraron plantas";
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
                    ViewBag.Resultado = "Ingrese un número válido";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        #endregion



        public ActionResult Cuidados(int id)
        {
            if (HttpContext.Session.GetString("UL") != null)
            {
                Planta plantaCuidados = ManejadorPlantas.BuscarPlantaPorId(id);
                return View(plantaCuidados);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}
