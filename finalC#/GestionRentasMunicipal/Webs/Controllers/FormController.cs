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
    public class FormController : ControllerBase
    {
        private readonly FormBusiness _formBusiness;
        private readonly ILogger<FormController> _logger;

        /// <summary>
        /// Constructor del controlador de permisos.
        /// </summary>
        /// <param name="FormBusiness">Capa de negocio de permisos.</param>
        /// <param name="logger">Logger para registro de eventos.</param>
        public FormController(FormBusiness FormBusiness, ILogger<FormController> logger)
        {
            _formBusiness = FormBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los permisos del sistema
        /// </summary>
        /// <returns> Lista de permisos </returns>
        /// <response code="200">Retorna la lista de permisos</response>
        /// <response code="500">Error interno del servidor </response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FormDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllForm()
        {
            try
            {
                var Forms = await _formBusiness.GetAllFormsAsync();
                return Ok(Forms);
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener formulario");
                return StatusCode(500, new { message = ex.Message });
            }
        }
        /// <summary>
        /// Obtiene un permiso especifico por su ID
        /// </summary>
        /// <param name="id">ID del permiso</param>
        /// <returns>Permiso solicitado</returns>
        /// <response code="200">Retorna el permiso solicitado</response>
        /// <response code="400">ID proporcionado no valido</response>
        /// <response code="404">Permisos no encontrados</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetFormById(int id, FormDto FormDto)
        {
           
            try
            {
                if (FormDto.IsDeleted == false)
                {

                }
                var Form = await _formBusiness.GetFormByIdAsync(id);
                return Ok(Form);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida para el formulario con ID: {FormId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Permiso no encontrado con ID: {FormId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener formulario con ID: {FormId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        ///<summary>
        ///Crea un nuevo permiso en el sistema
        /// </summary>
        /// <param name="FormDto">Datos del permiso a crear</param>
        /// <returns>Permiso Creado</returns>
        /// <response code="201">Retorna el permiso creado</response>
        /// <response code="400">Datos del permiso no validos</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost]
        [ProducesResponseType(typeof(FormDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateForm([FromBody] FormDto formDto)
        {
            try
            {
                var createdForm = await _formBusiness.CreateFormAsync(formDto);
                return CreatedAtAction(nameof(GetFormById), new { id = createdForm.FormId }, createdForm);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validacion fallida al crear el formulario");
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al crear el formulario");
                return StatusCode(500, new { messege = ex.Message });
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

        [HttpPut]
        [ProducesResponseType(typeof(FormDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateFormAsync([FromBody] FormDto FormDto)
        {
            try
            {
                if (FormDto == null || FormDto.FormId <= 0)
                {
                    return BadRequest(new { message = "El ID de la ruta no coincide con el ID del objeto." });
                }

                var updatedForm = await _formBusiness.UpdateFormAsync(FormDto);

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
        /// Guarda un formulario si deseamos quitarlo pero no eliminarlo por su ID
        /// </summary>
        /// <param name="id">ID del formulario</param>
        /// <returns>formulario solicitado</returns>
        /// <response code="200">Retorna el formulario solicitado</response>
        /// <response code="400">ID proporcionado no valido</response>
        /// <response code="404">Permisos no encontrados</response>
        /// <response code="500">Error interno del servidor</response>
        ///

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(FormDto), 200)]
        [ProducesResponseType(typeof(FormDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeletedLogicAsync(int id)
        {
            try
            {
                var deleteLogicalForm = await _formBusiness.DeleteLogicAsync(id);

                return Ok(deleteLogicalForm);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al eliminar el formulario con ID: {FormId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "No se encontró el Formulario con ID: {FormId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar el Formulario con ID: {FormId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina el rol del sistema
        /// </summary>
        /// 
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteForm(int id)
        {
            try
            {
                var deleted = await _formBusiness.DeleteFormAsync(id);

                if (!deleted)
                {
                    return NotFound(new { message = "rol no encontrado o ya esta eliminado" });
                }

                return Ok(new { message = "formulario eliminado con exito" });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar el formulario con ID: {FormId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
