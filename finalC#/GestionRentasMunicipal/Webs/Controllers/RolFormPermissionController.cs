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
    public class RolFormPermissionController : ControllerBase
    {
        private readonly RolFormPermissionBusiness _rolFormPermissionBusiness;
        private readonly ILogger<RolFormPermissionController> _logger;

        public RolFormPermissionController(RolFormPermissionBusiness rolFormPermissionBusiness, ILogger<RolFormPermissionController> logger)
        {
            _rolFormPermissionBusiness = rolFormPermissionBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtener todos los rol user del sistema
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RolFormPermissionDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllRolFormPermission()
        {
            try
            {
                var RolFormPermissions = await _rolFormPermissionBusiness.GetAllRolFormPermissionsAsync();
                return Ok(RolFormPermissions);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error al obtener los rol form permission");
                return StatusCode(500, new { message = ex.Message });
            }
        }


        ///<summary>
        /// Obtener un module especificio por su ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RolFormPermissionDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRolFormPermissionById(int id)
        {
            try
            {
                var rolFormPermission = await _rolFormPermissionBusiness.GetRolFormPermissionByIdAsync(id);
                return Ok(rolFormPermission);
            }
            catch (ValidationException ex)
            {
                _logger.LogInformation(ex, "Validacion fallida para rol form permission con ID: {RolFormPermissionId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {

                _logger.LogInformation(ex, "Rol Form Permission no encontrado con ID: {RolFormPermissionId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener el Rol form Permission con ID: {RolFormPermissionId}", id);
                throw;
            }
        }


        /// <summary>
        /// Crea un nuevo module en el sistema
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RolFormPermissionDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRolFormPermission([FromBody] RolFormPermissionDto rolFormPermissionDto)
        {
            try
            {
                var createdRolFormPermission = await _rolFormPermissionBusiness.CreateRolFormPermissionAsync(rolFormPermissionDto);
                return CreatedAtAction(nameof(GetRolFormPermissionById), new { id = createdRolFormPermission.RolFormPermissionId }, createdRolFormPermission);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validacion fallida al creal el Rol form permission");
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al crear el rol form permission");
                return StatusCode(500, new { message = ex.Message });
            }
        }


        /// <summary>
        /// Actualiza un form existente en el sistema
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(RolFormPermissionDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateRolFormPermission([FromBody] RolFormPermissionDto rolFormPermissionDto)
        {
            try
            {

                if (rolFormPermissionDto == null || rolFormPermissionDto.RolFormPermissionId <= 0)
                {
                    return BadRequest(new { message = "El ID de la ruta no coincide con el ID del objeto." });
                }

                var updatedRolFormPermission = await _rolFormPermissionBusiness.UpdateRolFormPermissionAsync(rolFormPermissionDto);

                return Ok(updatedRolFormPermission);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar el rol form permission con ID: {RolFormPermissionId}");
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "No se encontró el rol form permission con ID: {RolFormPermissionId}");
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar el rol form permission con ID: {RolFormPermissionId}");
                return StatusCode(500, new { message = ex.Message });
            }
        }


        /// <summary>
        /// Elimina un form del sistema
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteRolFormPermission(int id)
        {
            try
            {
                var deleted = await _rolFormPermissionBusiness.DeleteRolFormPermissionAsync(id);

                if (!deleted)
                {
                    return NotFound(new { message = "rol form permission no encontrado o ya eliminado" });
                }

                return Ok(new { message = "rol form permission eliminado exitosamente" });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar el rol form permission con ID: {RolFormPermissionId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
