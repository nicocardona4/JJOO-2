using DTO;
using ExcepcionesPropias;
using LogicaAplicacion.InterfacesCU;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public ILogin CULogin { get; set; }

        public UsuariosController(ILogin culogin) 
        {
            CULogin = culogin;
        }

        //POST
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.Email)) return BadRequest("Las credenciales son obligatorias");
            try
            {
                ListadoUsuariosDTO usu = CULogin.Loguear(dto);
                if (usu == null) return Unauthorized("Las credenciales no son válidas");
                string token = ManejadorJWT.GenerarToken(usu);
                return Ok(new { Token = token, Rol = usu.NombreRol });
                return Ok();
            }
            catch (UsuarioInvalidoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado");
            }
        }

        
    }
}
