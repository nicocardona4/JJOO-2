using DTO;
using ExcepcionesPropias;
using LogicaAplicacion.CU;
using LogicaAplicacion.InterfacesCU;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        public IBuscarEventos CUBuscarEventos { get; set; }
        public EventosController(IBuscarEventos cuBuscarEventos) 
        { 
            CUBuscarEventos = cuBuscarEventos;
        }

        //// GET: api/<EventosController>/
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BuscarPorFiltro(int? Id, string? fechaInicio, string? fechaFin, string? nombre, decimal? minPje, decimal? maxPje)
        {
            if(Id < 0) return BadRequest("El id de disciplina debe ser un entero positivo");
            if (minPje < 0) return BadRequest("El puntaje mínimo debe ser un entero positivo");
            if (maxPje < 0) return BadRequest("El puntaje máximo debe ser un entero positivo");

            try
            {
                int idAux = Id ?? 0; // Si nullId es null, se asigna 0
                decimal minPjeAux = minPje ?? 0;
                decimal maxPjeAux = maxPje ?? 0;
                if (string.IsNullOrEmpty(nombre)) nombre = "";
                if (string.IsNullOrEmpty(fechaInicio)) fechaInicio = "";
                if (string.IsNullOrEmpty(fechaFin)) fechaFin = "";


                IEnumerable<ListadoEventosDTO> eventos = CUBuscarEventos.Buscar(idAux, fechaInicio,fechaFin,nombre,minPjeAux,maxPjeAux);

                if (eventos != null)
                {
                    return Ok(eventos);
                }
                else
                {
                    return NotFound("No se encontraron eventos");

                }
            }
            catch (EventoInvalidoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
