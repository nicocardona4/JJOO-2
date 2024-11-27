using DTO;
using ExcepcionesPropias;
using LogicaAplicacion.InterfacesCU;
using LogicaNegocio.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinasController : ControllerBase
    {
        public IBuscarAtletasPorDisciplina CUAtletasPorDisciplina { get; set; }

        public IListadoDisciplinas CUListadoDisciplinas { get; set; }

        public IBuscarDisciplinaPorId CUBuscarDisciplina { get; set; }

        public IBuscarDisciplinaPorNombre CUBuscarDisciplinaPorNombre { get; set; }

        public IAltaDisciplina CUAlta {  get; set; }

        public IModificarDisciplina CUModificar { get; set; }

        public IBajaDisciplina CUBaja { get; set; }

        public IAltaAuditoria CUAltaAuditoria { get; set; }

        public DisciplinasController(IBuscarAtletasPorDisciplina cuAtletasPorDisciplina, IListadoDisciplinas cUListadoDisciplinas, IBuscarDisciplinaPorId cuBuscarDisciplina, IAltaDisciplina cUAlta, IModificarDisciplina cUModificar, IBajaDisciplina cUBaja, IBuscarDisciplinaPorNombre cUBuscarDisciplinaPorNombre, IAltaAuditoria cuAltaAuditoria)
        {
            CUAtletasPorDisciplina = cuAtletasPorDisciplina;
            CUListadoDisciplinas = cUListadoDisciplinas;
            CUBuscarDisciplina = cuBuscarDisciplina;
            CUAlta = cUAlta;
            CUModificar = cUModificar;
            CUBaja = cUBaja;
            CUBuscarDisciplinaPorNombre = cUBuscarDisciplinaPorNombre;
            CUAltaAuditoria = cuAltaAuditoria;
        }

        // GET api/disciplinas/5/atletas
        [HttpGet("{id}/atletas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AtletasPorDisciplina(int id)
        {
            if (id <= 0) return BadRequest("El id debe ser un entero positivo");
            try
            {
                IEnumerable<ListadoAtletasDTO> atletas = CUAtletasPorDisciplina.BuscarAtletas(id);

                if (atletas.Count() > 0)
                {
                    return Ok(atletas);
                }
                else
                {
                    return NotFound("No hay atletas registrados para esa disciplina");

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado!");
            }
        }

        // GET: api/disciplinas
        [HttpGet(Name = "FindAll")]
        public IActionResult FindAll()
        {
            try
            {
                IEnumerable<ListadoDisciplinasDTO> disciplinas = CUListadoDisciplinas.ObtenerListado();
                return Ok(disciplinas);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "Ocurrió un error inesperado!");
            }

        }

        // GET api/<DisciplinasController>/5
        [HttpGet("{id}", Name = "FindById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {

            if (id <= 0) return BadRequest("El id debe ser un entero positivo");
            try
            {
                ListadoDisciplinasDTO disciplina = CUBuscarDisciplina.Buscar(id);

                if (disciplina != null)
                {
                    return Ok(disciplina);
                }
                else
                {
                    return NotFound("No se encontró disciplina con ese Id");

                }
            }
            catch (DisciplinaInvalidaException ex)
            {
                return NotFound("No se encontró disciplina con ese Id");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado!");
            }
        }

        // GET api/<DisciplinasController>/nombre/SaltoConGarrocha
        [HttpGet("nombre/{nombre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetNombre(string nombre)
        {

            if (string.IsNullOrEmpty(nombre)) return BadRequest("Debe ingresar un nombre de disciplina");
            if (nombre.Length < 10 || nombre.Length > 50) return BadRequest("Debe ingresar un nombre con 10-50 caracteres");
            try
            {
                ListadoDisciplinasDTO disciplina = CUBuscarDisciplinaPorNombre.Buscar(nombre);

                if (disciplina != null)
                {
                    return Ok(disciplina);
                }
                else
                {
                    return NotFound("No se encontró disciplina con ese nombre");

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado!");
            }
        }


        // POST api/<DisciplinasController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Post(AltaDisciplinaDTO dto)
        {
            Claim? claim_email = User.Claims.Where(c => c.Type == ClaimTypes.Email).SingleOrDefault();
                string email = claim_email.Value;
            if (dto == null) return BadRequest("No se proporcionaron datos para el alta de disciplina");
            if (dto.Anio < 1830 || dto.Anio > 2024) return BadRequest("El año ingresado no es válido");
            if (dto.Codigo <= 0) return BadRequest("El código ingresado no es válido");
            if (dto.Nombre.Length < 10 || dto.Nombre.Length > 50) return BadRequest("El nombre debe tener entre 10 y 50 caracteres");
            if (dto.Id != 0) return BadRequest("No se debe proporcionar un id para el alta");
            try
            {
                CUAlta.Alta(dto);
                AltaAuditoriaDTO a = new AltaAuditoriaDTO();
                a.Email = email;
                a.Id = 0;
                a.EntidadId = dto.Id;
                a.Entidad = "Disciplina";
                a.Fecha = DateTime.Now;
                a.Operacion = "Creado";
                CUAltaAuditoria.Alta(a);
                return CreatedAtRoute("FindById", new { id = dto.Id }, dto);
            }
            catch (OperacionConflictivaException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error y no se realizó el alta.");
            }
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Put(int? id, [FromBody] AltaDisciplinaDTO? disciplina)
        {
            Claim? claim_email = User.Claims.Where(c => c.Type == ClaimTypes.Email).SingleOrDefault();
            string email = claim_email.Value;
            if (id == null)
            {
                return BadRequest("No se proporciona un id");
            }

            if (disciplina == null)
            {
                return BadRequest("No se proporcionan datos para la modificación");
            }

            if (id <= 0 || disciplina.Id <= 0)
            {
                return BadRequest("Los id deben ser enteros positivos");
            }

            if (id.Value != disciplina.Id)
            {
                return BadRequest("No coinciden los ids proporcionados");
            }

            try
            {
                CUModificar.Modificar(disciplina);
                AltaAuditoriaDTO a = new AltaAuditoriaDTO();
                a.Email = email;
                a.Id = 0;
                a.EntidadId = disciplina.Id;
                a.Entidad = "Disciplina";
                a.Fecha = DateTime.Now;
                a.Operacion = "Editado";
                CUAltaAuditoria.Alta(a);
                return Ok(disciplina);
            }
            catch (DisciplinaInvalidaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (OperacionConflictivaException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "No se pudo realizar la modificación, debido a un error inesperado");
            }
        }

        //// PUT api/<DisciplinasController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<DisciplinasController>/5
        [HttpDelete("{id?}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Delete(int? id)
        {
            Claim? claim_email = User.Claims.Where(c => c.Type == ClaimTypes.Email).SingleOrDefault();
            string email = claim_email.Value;
            if (id == null)
            {
                return BadRequest("Debe proporcionar un id");
            }

            if (id <= 0)
            {
                return BadRequest("El id debe ser un entero mayor que cero");
            }

            try
            {
                CUBaja.Borrar(id.Value);
                AltaAuditoriaDTO a = new AltaAuditoriaDTO();
                a.Email = email;
                a.Id = 0;
                a.EntidadId = id.Value;
                a.Entidad = "Disciplina";
                a.Fecha = DateTime.Now;
                a.Operacion = "Eliminado";
                CUAltaAuditoria.Alta(a);
                return NoContent();
            }
            catch (DisciplinaInvalidaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (OperacionConflictivaException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error, no se pudo realizar la baja de la disciplina");
            }
        }
    }
}
