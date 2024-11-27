using DTO;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;

namespace PresentacionMVC.Controllers
{
    public class DisciplinaController : Controller
    {
        //GET: DisciplinaController/Disciplinas
        [HttpGet]
        public IActionResult Index()
        {
            string rol = HttpContext.Session.GetString("rol");
            if (!string.IsNullOrEmpty(rol) && rol == "Digitador")
            {
                try
                {
                    string token = HttpContext.Session.GetString("jwt");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    string url = "http://localhost:5188/api/Disciplinas";

                    Task<HttpResponseMessage> tarea1 = client.GetAsync(url);
                    tarea1.Wait();

                    HttpResponseMessage respuesta = tarea1.Result;

                    HttpContent body = respuesta.Content;
                    Task<string> tarea2 = body.ReadAsStringAsync();
                    tarea2.Wait();

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string json = tarea2.Result;
                        List<ListadoDisciplinasDTO> disciplinas = JsonConvert.DeserializeObject<List<ListadoDisciplinasDTO>>(json);
                        return View(disciplinas);

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
            }
               
            return RedirectToAction("NoAutorizado", "Usuario");
        }


        // GET: DisciplinaController/SeleccionarDisciplina
        [HttpGet]
        public IActionResult SeleccionarDisciplina()
        {
            string url = "http://localhost:5188/api/Disciplinas";
            try
            {
                HttpClient client = new HttpClient();
                Task<HttpResponseMessage> tarea1 = client.GetAsync(url);
                tarea1.Wait();

                HttpResponseMessage respuesta = tarea1.Result;
                HttpContent contenido = tarea1.Result.Content;

                Task<string> tarea2 = contenido.ReadAsStringAsync();

                if (respuesta.IsSuccessStatusCode)
                {
                    string json = tarea2.Result;
                    List<ListadoDisciplinasDTO> disciplinas = JsonConvert.DeserializeObject<List<ListadoDisciplinasDTO>>(json);
                    return View(disciplinas);

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



        // GET: DisciplinaController/5/atletas
        [HttpGet]
        public ActionResult AtletasPorDisciplina(int id)
        {
                string url = "http://localhost:5188/api/Disciplinas/" + id + "/atletas";
                try
                {
                    if (id <= 0) throw new Exception("No es válido el id");
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
                        List<ListadoAtletasDTO> atletas = JsonConvert.DeserializeObject<List<ListadoAtletasDTO>>(json);
                        return View(atletas);

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


        // GET: DisciplinaController/Details/5
        public ActionResult Details(int id)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (!string.IsNullOrEmpty(rol) && rol == "Digitador")
            {
                string url = "http://localhost:5188/api/Disciplinas/" + id;
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
                        ListadoDisciplinasDTO disciplina = JsonConvert.DeserializeObject<ListadoDisciplinasDTO>(json);
                        return View(disciplina);

                    }
                    else
                    {
                        TempData["Error"] = tarea2.Result;
                        return RedirectToAction("Index");
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

        // GET: DisciplinaController/Create
        public ActionResult Create()
        {
            string rol = HttpContext.Session.GetString("rol");
            if (!string.IsNullOrEmpty(rol) && rol == "Digitador")
            {
                AltaDisciplinaDTO dto = new AltaDisciplinaDTO();
                return View(dto);
            }

            return RedirectToAction("NoAutorizado", "Usuario");

        }

        // POST: DisciplinaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AltaDisciplinaDTO dto)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (!string.IsNullOrEmpty(rol) && rol == "Digitador")
            {
                string url = "http://localhost:5188/api/Disciplinas/";
                try
                {
                    string token = HttpContext.Session.GetString("jwt");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var tarea1 = client.PostAsJsonAsync(url, dto);
                    tarea1.Wait();

                    if (tarea1.Result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var tarea2 = tarea1.Result.Content.ReadAsStringAsync();
                        tarea2.Wait();
                        ViewBag.Error = tarea2.Result;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Ocurrió un error inesperado";
                }

                return View(dto);
            }

            return RedirectToAction("NoAutorizado", "Usuario");
        }

        // GET: RolesController/Edit/5
        public ActionResult Edit(int id)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (!string.IsNullOrEmpty(rol) && rol == "Digitador")
            {
                try
                {
                    AltaDisciplinaDTO dto = ObtenerDisciplinaPorId(id);
                    if (dto != null)
                    {
                        return View(dto);
                    }
                    else
                    {
                        TempData["Error"] = "No se encontró disciplina con ese id";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Ocurrió un error inesperado!";
                }

                return View();
            }
                
            return RedirectToAction("NoAutorizado", "Usuario");

        }

        // POST: DisciplinaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AltaDisciplinaDTO dto)
        {
            AltaDisciplinaDTO disciplinaActualizada = null;
            string rol = HttpContext.Session.GetString("rol");
            if (!string.IsNullOrEmpty(rol) && rol == "Digitador")
            {
                try
                {
                    string url = "http://localhost:5188/api/Disciplinas/" + dto.Id;

                    string token = HttpContext.Session.GetString("jwt");
                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var tarea1 = cliente.PutAsJsonAsync(url, dto);
                    tarea1.Wait();

                    if (tarea1.Result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var tarea2 = tarea1.Result.Content.ReadAsStringAsync();
                        tarea2.Wait();

                        ViewBag.Error = $"{tarea2.Result}";
                        disciplinaActualizada = ObtenerDisciplinaPorId(dto.Id);
                        return View(disciplinaActualizada);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Ocurrió un error y no se realizó la modificación.";
                    RedirectToAction("Edit", new { id = dto.Id });
                }

                disciplinaActualizada = ObtenerDisciplinaPorId(dto.Id);
                return View(disciplinaActualizada);
            }

            return RedirectToAction("NoAutorizado", "Usuario");

        }

        private AltaDisciplinaDTO ObtenerDisciplinaPorId(int id)
        {
            string url = "http://localhost:5188/api/Disciplinas/" + id;

            string token = HttpContext.Session.GetString("jwt");
            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var tarea1 = cliente.GetAsync(url);
            tarea1.Wait();

            if (tarea1.Result.IsSuccessStatusCode)
            {
                var tarea2 = tarea1.Result.Content.ReadAsStringAsync();
                tarea2.Wait();

                string json = tarea2.Result;
                return JsonConvert.DeserializeObject<AltaDisciplinaDTO>(json);
            }

            return null;
        }


        // GET: DisciplinaController/Delete/5
        public ActionResult Delete(int id)
        {
            string rol = HttpContext.Session.GetString("rol");
            if (!string.IsNullOrEmpty(rol) && rol == "Digitador")
            {
                try
                {
                    AltaDisciplinaDTO dto = ObtenerDisciplinaPorId(id);
                    if (dto != null)
                    {
                        return View(dto);
                    }
                    else
                    {
                        TempData["Error"] = "No se encontró disciplina con ese id";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Ocurrió un error inesperado!";
                }

                return View();
            }
               
            return RedirectToAction("NoAutorizado", "Usuario");
        }

        // POST: DisciplinaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AltaDisciplinaDTO dto)
        {
            AltaDisciplinaDTO disciplinaActualizada = null;
            string rol = HttpContext.Session.GetString("rol");
            if (!string.IsNullOrEmpty(rol) && rol == "Digitador")
            {
                try
                {
                    string url = "http://localhost:5188/api/Disciplinas/" + dto.Id;

                    string token = HttpContext.Session.GetString("jwt");
                    HttpClient cliente = new HttpClient();
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var tarea1 = cliente.DeleteAsync(url);
                    tarea1.Wait();

                    if (tarea1.Result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var tarea2 = tarea1.Result.Content.ReadAsStringAsync();
                        tarea2.Wait();

                        ViewBag.Error = $"{tarea2.Result}";
                        disciplinaActualizada = ObtenerDisciplinaPorId(dto.Id);
                        return View(disciplinaActualizada);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Ocurrió un error y no se realizó el borrado";
                }

                return View(dto);
            }
                
            return RedirectToAction("NoAutorizado", "Usuario");
        }

        // GET: DisciplinaController/BuscarDisciplina/BuscarDisciplina?id=1005
        [HttpGet]
        public IActionResult BuscarDisciplina()
        {
            string rol = HttpContext.Session.GetString("rol");
            if (!string.IsNullOrEmpty(rol) && rol == "Digitador")
            {
                string url = "http://localhost:5188/api/Disciplinas";
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
                        List<ListadoDisciplinasDTO> disciplinas = JsonConvert.DeserializeObject<List<ListadoDisciplinasDTO>>(json);
                        return View(disciplinas);

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


    }
}
