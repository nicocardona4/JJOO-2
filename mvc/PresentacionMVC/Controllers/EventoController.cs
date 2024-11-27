using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentacionMVC.Models;
using System.Net.Http.Headers;
using System.Security.Policy;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PresentacionMVC.Controllers
{
    public class EventoController : Controller
    {
        //GET: EventoController/GetEventos/
        [HttpPost]
        public IActionResult GetEventos(int? id, string fechaInicio, string fechaFin, string nombre, decimal? minPje, decimal? maxPje)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (!string.IsNullOrEmpty(rol) && rol == "Digitador")
            {
                string url = "http://localhost:5188/api/Eventos";

                List<string> parametros = new List<string>();

                if (id.HasValue)
                    parametros.Add($"Id={id.Value}");

                if (!string.IsNullOrEmpty(fechaInicio))
                    parametros.Add($"FechaInicio={fechaInicio}");

                if (!string.IsNullOrEmpty(fechaFin))
                    parametros.Add($"FechaFin={fechaFin}");

                if (!string.IsNullOrEmpty(nombre))
                    parametros.Add($"Nombre={nombre}");

                if (minPje.HasValue)
                    parametros.Add($"MinPje={minPje.Value}");

                if (maxPje.HasValue)
                    parametros.Add($"MaxPje={maxPje.Value}");

                if (parametros.Count > 0)
                    url += "?" + string.Join("&", parametros);

                try
                {
                    string token = HttpContext.Session.GetString("jwt");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> tarea1 = client.GetAsync(url);
                    tarea1.Wait();

                    HttpResponseMessage respuesta = tarea1.Result;
                    HttpContent contenido = tarea1.Result.Content;

                    Task<string> tarea2 = contenido.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string json = tarea2.Result;
                        List<ListadoEventosDTO> eventos = JsonConvert.DeserializeObject<List<ListadoEventosDTO>>(json);
                        return View("Index", eventos); // Reutilizamos la vista Index para mostrar los eventos
                    }
                    else
                    {
                        ViewBag.Error = tarea2.Result;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Ocurrió un error inesperado";
                }
                return View("Index");
            }
               
            return RedirectToAction("NoAutorizado", "Usuario");
        }



        //GET: EventoController/Eventos/
        [HttpGet]
        public IActionResult Index()
        {
            string rol = HttpContext.Session.GetString("rol");
            if (!string.IsNullOrEmpty(rol) && rol == "Digitador")
            {
                string url = "http://localhost:5188/api/Eventos";
                try
                {
                    string token = HttpContext.Session.GetString("jwt");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    Task<HttpResponseMessage> tarea1 = client.GetAsync(url);
                    tarea1.Wait();

                    HttpResponseMessage respuesta = tarea1.Result;
                    HttpContent contenido = tarea1.Result.Content;

                    Task<string> tarea2 = contenido.ReadAsStringAsync();

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string json = tarea2.Result;
                        List<ListadoEventosDTO> eventos = JsonConvert.DeserializeObject<List<ListadoEventosDTO>>(json);
                        return View(eventos);

                    }
                    else
                    {
                        ViewBag.Error = tarea2.Result;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Ocurrió un error inesperado";
                }
                return View();
            }

            return RedirectToAction("NoAutorizado", "Usuario");
        }


        //// GET: EventoController
        //public ActionResult Index(EventoFchDTO dto)
        //{
        //    IEnumerable<ListadoEventosDTO> DTO = CUListadoEventos.ObtenerListado(dto);
        //    return View("Index", vm);

        //}

        // GET: EventoController/Details/5
        //public ActionResult ListadoEventos(EventoFchDTO dto)
        //{
        //    return View();
        //}

        // GET: EventoController/Create
        //public ActionResult Create()
        //{
        //    string rol = HttpContext.Session.GetString("Rol");
        //    if (!string.IsNullOrEmpty(rol))
        //    {
        //        AltaEventoViewModel vm = new AltaEventoViewModel();
        //        vm.EventoDTO = new AltaEventoDTO();
        //        vm.atletas = CUListadoAtletas.ObtenerListado();
        //        vm.ListadoDisciplinasDTO = CUListadoDisciplinas.ObtenerListado();
        //        return View(vm);
        //    }
        //    else
        //    {
        //        return RedirectToAction("NoAutorizado","Usuario");
        //    }

        //}

        // POST: EventoController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(AltaEventoViewModel vm)
        //{
        //string rol = HttpContext.Session.GetString("Rol");
        //    if (!string.IsNullOrEmpty(rol))
        //    {
        //        try
        //        {
        //            ViewBag.Mensaje = null;
        //            CUAltaEvento.Alta(vm.EventoDTO, vm.EventoDTO.IdAtletas);
        //            ViewBag.Mensaje = "Evento creado correctamente";
        //            AltaEventoViewModel nuevo = new AltaEventoViewModel();
        //            nuevo.EventoDTO = new AltaEventoDTO();
        //            nuevo.EventoDTO.NombrePrueba = "";
        //            nuevo.atletas = CUListadoAtletas.ObtenerListado();
        //            nuevo.ListadoDisciplinasDTO = CUListadoDisciplinas.ObtenerListado();
        //            return View(nuevo);
        //        }
        //        catch (EventoInvalidoException ex)
        //        {
        //            ViewBag.Mensaje = $"Error al crear el evento: {ex.Message}";
        //            AltaEventoViewModel nuevo = new AltaEventoViewModel();
        //            nuevo.EventoDTO = new AltaEventoDTO();
        //            nuevo.atletas = CUListadoAtletas.ObtenerListado();
        //            nuevo.ListadoDisciplinasDTO = CUListadoDisciplinas.ObtenerListado();
        //            return View(nuevo);
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Mensaje = $"Error al crear el evento: {ex.Message}";
        //            AltaEventoViewModel nuevo = new AltaEventoViewModel();
        //            nuevo.EventoDTO = new AltaEventoDTO();
        //            nuevo.atletas = CUListadoAtletas.ObtenerListado();
        //            nuevo.ListadoDisciplinasDTO = CUListadoDisciplinas.ObtenerListado();
        //            return View(nuevo);
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("NoAutorizado", "Usuario");
        //    }
               
        //}



        // GET: EventoController/ListadoEventos
        [HttpGet]
        public ActionResult ListadoEventos()
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (!string.IsNullOrEmpty(rol))
            {
                var model = new ListadoEventosViewModel
                {
                    EventoFch = new EventoFchDTO(),
                    Eventos = new List<ListadoEventosDTO>()
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Usuario");
            }

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ListadoEventos(ListadoEventosViewModel vm)
        //{
        //    string rol = HttpContext.Session.GetString("Rol");
        //    if (!string.IsNullOrEmpty(rol))
        //    {
        //        try
        //        {
        //            // Lógica para obtener los eventos según la fecha ingresada
        //            var eventos = CUListadoEventos.ObtenerListado(vm.EventoFch);

        //            // Actualiza el modelo con los eventos encontrados
        //            vm.Eventos = eventos;
        //            vm.cargado = true;

        //            return View(vm);  // Devuelve la vista con los resultados
        //        }
        //        catch (Exception ex)
        //        {
        //            // Maneja errores y regresa a la vista
        //            ModelState.AddModelError("", ex.Message);
        //            return View(vm);
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("NoAutorizado", "Usuario");
        //    }

                
        //}

        // GET: EventoController/VerAtletas?nombrePrueba=Posta%20100metros
        //public ActionResult VerAtletas(string nombrePrueba)
        //{
        //    string rol = HttpContext.Session.GetString("Rol");
        //    if (!string.IsNullOrEmpty(rol))
        //    {
        //        ListadoEventosDTO e = CUBuscarEventoPorNombre.Buscar(nombrePrueba);
        //        IEnumerable<ListadoAtletasDTO> atletas = CUListadoAtletas.ObtenerAtletasPorId(e.IdAtletas);
        //        ViewBag.NombrePrueba = nombrePrueba;
        //        return View(atletas);
        //    }
        //    else
        //    {
        //        return RedirectToAction("NoAutorizado", "Usuario");
        //    }
                
        //}

        // GET: EventoController/AgregarParticipacion?atletaId=1&nombrePrueba=Posta%20400%20metros
        public ActionResult AgregarParticipacion(int idAtleta, string nombrePrueba)
        {
            string rol = HttpContext.Session.GetString("Rol");
            if (!string.IsNullOrEmpty(rol))
            {
                ParticipacionDTO dto = new ParticipacionDTO();
                dto.idAtleta = idAtleta;
                dto.nombrePrueba = nombrePrueba;
                return View(dto);
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Usuario");
            }

        }
        //[HttpPost]
        //public ActionResult AgregarParticipacion(ParticipacionDTO dto)
        //{
        //    string rol = HttpContext.Session.GetString("Rol");
        //    if (!string.IsNullOrEmpty(rol))
        //    {
        //        try
        //        {
        //            ViewBag.Mensaje = null;
        //            CUAltaParticipacion.Alta(dto);
        //            ViewBag.Mensaje = "Puntuación añadida con éxito";
        //            return View(dto);
        //        }
        //        catch (ParticipacionInvalidaException ex)
        //        {
        //            ViewBag.Mensaje = ex.Message;
        //            return View(dto);
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Mensaje = ex.Message;
        //            return View(dto);
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("NoAutorizado", "Usuario");
        //    }
               
        //}

        // GET: EventoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
