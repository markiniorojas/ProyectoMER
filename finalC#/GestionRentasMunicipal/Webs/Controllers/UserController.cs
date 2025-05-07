using Business;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Utilities.Exceptions;

namespace Web.Controllers
{
    /// <summary>
    /// Controlador para la gestión de usuarios en el sistema
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly UserBusiness _userBusiness;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Constructor del controlador de usuarios.
        /// </summary>
        /// <param name="UserBusiness">Capa de negocio de usuarios.</param>
        /// <param name="logger">Logger para registro de eventos.</param>
        public UserController(UserBusiness UserBusiness, ILogger<UserController> logger)
        {
            _userBusiness = UserBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los usuarios del sistema
        /// </summary>
        /// <returns> Lista de permisos </returns>
        /// <response code="200">Retorna la lista de usuario</response>
        /// <response code="500">Error interno del servidor </response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var Users = await _userBusiness.GetAllUsersAsync();
                return Ok(Users);
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener los usuarios");
                return StatusCode(500, new { message = ex.Message });
            }
        }
        /// <summary>
        /// Obtiene un usuario especifico por su ID
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <returns>Permiso solicitado</returns>
        /// <response code="200">Retorna el permiso solicitado</response>
        /// <response code="400">ID proporcionado no valido</response>
        /// <response code="404">Permisos no encontrados</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var Rol = await _userBusiness.GetUserByIdAsync(id);
                return Ok(Rol);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida para el usuario con ID: {UserId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Permiso no encontrado con ID: {UserId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener permiso con ID: {UserId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        ///<summary>
        ///Crea un nuevo permiso en el sistema
        /// </summary>
        /// <param name="UserDto">Datos del permiso a crear</param>
        /// <returns>Permiso Creado</returns>
        /// <response code="201">Retorna el permiso creado</response>
        /// <response code="400">Datos del permiso no validos</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRol([FromBody] UserDto userDto)
        {
            try
            {
                var createdUser = await _userBusiness.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validacion fallida al crear usuario");
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al crear usuario");
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
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserDto UserDto)
        {
            try
            {
                if (UserDto == null || UserDto.Id <= 0)
                {
                    return BadRequest(new { message = "El ID de la ruta no coincide con el ID del objeto." });
                }

                var updatedUser = await _userBusiness.UpdateUserAsync(UserDto);

                return Ok(updatedUser);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar el user con ID: {UserId}");
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "No se encontró el user con ID: {UserId}");
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar el user con ID: {UserId}");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Guarda un rol si deseamos quitarlo mas no eliminarlo especifico por su ID
        /// </summary>
        /// <param name="id">ID del permiso</param>
        /// <returns>Permiso solicitado</returns>
        /// <response code="200">Retorna el permiso solicitado</response>
        /// <response code="400">ID proporcionado no valido</response>
        /// <response code="404">Permisos no encontrados</response>
        /// <response code="500">Error interno del servidor</response>
        ///

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeletedLogicAsync(int id)
        {
            try
            {
                var deleteLogicalRol = await _userBusiness.DeleteLogicAsync(id);

                return Ok(deleteLogicalRol);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al eliminar el user con ID: {UserId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "No se encontró el user con ID: {UserId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar el user con ID: {UserId}", id);
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
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var deleted = await _userBusiness.DeleteUserAsync(id);

                if (!deleted)
                {
                    return NotFound(new { message = "usuario no encontrado o ya esta eliminado" });
                }

                return Ok(new { message = "Usuario eliminado con exito" });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar el usuario con ID: {UserId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}