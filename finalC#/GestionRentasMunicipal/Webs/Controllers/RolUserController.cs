using Business;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Utilities.Exceptions;

namespace Webs.Controllers
{
    /// <summary>
    /// Controlador para la gestión de permisos en el sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RolUserController : ControllerBase
    {
        private readonly RolUserBusiness _rolUserBusiness;
        private readonly ILogger<RolUserController> _logger;

        public RolUserController(RolUserBusiness rolUserBusiness, ILogger<RolUserController> logger)
        {
            _rolUserBusiness = rolUserBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtener todos los rol user del sistema
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RolUserDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllRolUser()
        {
            try
            {
                var RolUsers = await _rolUserBusiness.GetAllRolUsersAsync();
                return Ok(RolUsers);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error al obtener los rol user");
                return StatusCode(500, new { message = ex.Message });
            }
        }


        ///<summary>
        /// Obtener un module especificio por su ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RolUserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRolUserById(int id)
        {
            try
            {
                var RolUser = await _rolUserBusiness.GetRolUserByIdAsync(id);
                return Ok(RolUser);
            }
            catch (ValidationException ex)
            {
                _logger.LogInformation(ex, "Validacion fallida para rol user con ID: {RolUserId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {

                _logger.LogInformation(ex, "Form no encontrado con ID: {RolUserId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener el Rol user con ID: {RolUserId}", id);
                throw;
            }
        }


        /// <summary>
        /// Crea un nuevo module en el sistema
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RolUserDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateForm([FromBody] RolUserDto rolUserDto)
        {
            try
            {
                var createdRolUser = await _rolUserBusiness.CreateRolUsersAsync(rolUserDto);
                return CreatedAtAction(nameof(GetRolUserById), new { id = createdRolUser.RolUserId }, createdRolUser);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validacion fallida al creal el Rol user");
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al crear el rol user");
                return StatusCode(500, new { message = ex.Message });
            }
        }


        /// <summary>
        /// Actualiza un form existente en el sistema
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(RolUserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateRolUser([FromBody] RolUserDto rolUserDto)
        {
            try
            {

                if (rolUserDto == null || rolUserDto.RolUserId <= 0)
                {
                    return BadRequest(new { message = "El ID de la ruta no coincide con el ID del objeto." });
                }

                var updatedRolUser = await _rolUserBusiness.UpdateRolUserAsync(rolUserDto);

                return Ok(updatedRolUser);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar el rol user con ID: {RolUserId}");
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "No se encontró el rol user con ID: {RolUserId}");
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar el rol user con ID: {RolUserId}");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Guarda un rol si deseamos quitarlo pero no eliminarlo por su ID
        /// </summary>
        /// <param name="id">ID del permiso</param>
        /// <returns>Permiso solicitado</returns>
        /// <response code="200">Retorna el permiso solicitado</response>
        /// <response code="400">ID proporcionado no valido</response>
        /// <response code="404">Permisos no encontrados</response>
        /// <response code="500">Error interno del servidor</response>
        ///

        //[HttpPatch("{id}")]
        //[ProducesResponseType(typeof(RolUserDto), 200)]
        //[ProducesResponseType(typeof(RolUserDto), 200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(500)]
        //public async Task<IActionResult> DeletedLogicAsync(int id)
        //{
        //    try
        //    {
        //        var deleteLogicalRol = await _rolUserBusiness.DeleteLogicAsync(id);

        //        return Ok(deleteLogicalRol);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        _logger.LogWarning(ex, "Validación fallida al eliminar el rolUser con ID: {RolUserId}", id);
        //        return BadRequest(new { message = ex.Message });
        //    }
        //    catch (EntityNotFoundException ex)
        //    {
        //        _logger.LogInformation(ex, "No se encontró el rolUser con ID: {RolUserId}", id);
        //        return NotFound(new { message = ex.Message });
        //    }
        //    catch (ExternalServiceException ex)
        //    {
        //        _logger.LogError(ex, "Error al actualizar el rolUser con ID: {RolUserId}", id);
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}


        /// <summary>
        /// Elimina un form del sistema
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteRolUser(int id)
        {
            try
            {
                var deleted = await _rolUserBusiness.DeleteRolUserAsync(id);

                if (!deleted)
                {
                    return NotFound(new { message = "rol user no encontrado o ya eliminado" });
                }

                return Ok(new { message = "rol user eliminado exitosamente" });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar el rol user con ID: {RolUserId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
