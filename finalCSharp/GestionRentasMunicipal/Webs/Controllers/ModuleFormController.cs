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
    public class ModuleFormController : ControllerBase
    {
        private readonly ModuleFormBusiness _moduleFormBusiness;
        private readonly ILogger<ModuleFormController> _logger;

        /// <summary>
        /// Constructor del controlador de module form.
        /// </summary>
        /// <param name="ModuleFormBusiness">Capa de negocio de permisos.</param>
        /// <param name="logger">Logger para registro de eventos.</param>
        public ModuleFormController(ModuleFormBusiness ModuleFormBusiness, ILogger<ModuleFormController> logger)
        {
            _moduleFormBusiness = ModuleFormBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los module form del sistema
        /// </summary>
        /// <returns> Lista de module form </returns>
        /// <response code="200">Retorna la lista de module form</response>
        /// <response code="500">Error interno del servidor </response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ModuleFormDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllModuleForm()
        {
            try
            {
                var ModuleForm = await _moduleFormBusiness.GetAllModuleFormAsync();
                return Ok(ModuleForm);
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener module form");
                return StatusCode(500, new { message = ex.Message });
            }
        }
        /// <summary>
        /// Obtiene un permiso especifico por su ID
        /// </summary>
        /// <param name="id">ID del module form</param>
        /// <returns>Permiso solicitado</returns>
        /// <response code="200">Retorna el permiso solicitado</response>
        /// <response code="400">ID proporcionado no valido</response>
        /// <response code="404">Permisos no encontrados</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModuleFormDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetModuleFormById(int id)
        {
            try
            {
                var ModuleForm = await _moduleFormBusiness.GetModuleFormByIdAsync(id);
                return Ok(ModuleForm);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida para el module form con ID: {ModuleFormId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Permiso no encontrado con ID: {ModuleFormId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener module form con ID: {ModuleFormId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        ///<summary>
        ///Crea un nuevo permiso en el sistema
        /// </summary>
        /// <param name="ModuleFormDto">Datos del permiso a crear</param>
        /// <returns>Permiso Creado</returns>
        /// <response code="201">Retorna el permiso creado</response>
        /// <response code="400">Datos del permiso no validos</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost]
        [ProducesResponseType(typeof(ModuleFormDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateForm([FromBody] ModuleFormDto moduleFormDto)
        {
            try
            {
                var createdModuleForm = await _moduleFormBusiness.CreateModuleFormAsync(moduleFormDto);
                return CreatedAtAction(nameof(GetModuleFormById), new { id = createdModuleForm.ModuleFormId }, createdModuleForm);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validacion fallida al crear el module form");
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al crear el module form");
                return StatusCode(500, new { messege = ex.Message });
            }
        }

        /// <summary>
        /// actualiza un module form especifico por su ID
        /// </summary>
        /// <param name="id">ID del module form</param>
        /// <returns>Permiso solicitado</returns>
        /// <response code="200">Retorna el permiso solicitado</response>
        /// <response code="400">ID proporcionado no valido</response>
        /// <response code="404">Permisos no encontrados</response>
        /// <response code="500">Error interno del servidor</response>
        ///

        [HttpPut]
        [ProducesResponseType(typeof(ModuleFormDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateModuleFormAsync([FromBody] ModuleFormDto ModuleFormDto)
        {
            try
            {
                if (ModuleFormDto == null || ModuleFormDto.ModuleFormId <= 0)
                {
                    return BadRequest(new { message = "El ID de la ruta no coincide con el ID del objeto." });
                }

                var updatedForm = await _moduleFormBusiness.UpdateModuleFormAsync(ModuleFormDto);

                return Ok(updatedForm);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar el rol con ID: {RolId}");
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "No se encontró el rol con ID: {RolId}");
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar el rol con ID: {RolId}");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// actualiza un permiso especifico por su ID
        /// </summary>
        /// <param name="id">ID del permiso</param>
        /// <returns>Permiso solicitado</returns>
        /// <response code="200">Retorna el permiso solicitado</response>
        /// <response code="400">ID proporcionado no valido</response>
        /// <response code="404">Permisos no encontrados</response>
        /// <response code="500">Error interno del servidor</response>
        ///

        //[HttpPath("{id}")]
        //[ProducesResponseType(typeof(RolDto), 200)]
        //[ProducesResponseType(typeof(RolDto), 200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(500)]
        //public async Task<IActionResult> UpdateRolAsync(int id, [FromBody] RolDto RolDto)
        //{
        //    try
        //    {
        //        if (id != RolDto.RolId)
        //        {
        //            return BadRequest(new { message = "El ID de la ruta no coincide con el ID del objeto." });
        //        }

        //        var updatedRol = await _RolBusiness.UpdateRolAsync(RolDto);

        //        return Ok(updatedRol);
        //    }
        //    catch (ValidationException ex)
        //    {
        //        _logger.LogWarning(ex, "Validación fallida al actualizar el rol con ID: {RolId}", id);
        //        return BadRequest(new { message = ex.Message });
        //    }
        //    catch (EntityNotFoundException ex)
        //    {
        //        _logger.LogInformation(ex, "No se encontró el rol con ID: {RolId}", id);
        //        return NotFound(new { message = ex.Message });
        //    }
        //    catch (ExternalServiceException ex)
        //    {
        //        _logger.LogError(ex, "Error al actualizar el rol con ID: {RolId}", id);
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

        /// <summary>
        /// Elimina el rol del sistema
        /// </summary>
        /// 
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteModuleForm(int id)
        {
            try
            {
                var deleted = await _moduleFormBusiness.DeleteModuleFormAsync(id);

                if (!deleted)
                {
                    return NotFound(new { message = "module form no encontrado o ya esta eliminado" });
                }

                return Ok(new { message = "module formulario eliminado con exito" });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar el module form con ID: {ModuleFormId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
