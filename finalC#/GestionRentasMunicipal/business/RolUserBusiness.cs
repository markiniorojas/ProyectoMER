using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los roles del sistema.
    /// </summary>
    public class RolUserBusiness
    {
        private readonly RolUserData _rolUserData;
        private readonly ILogger<RolUserBusiness> _logger;

        public RolUserBusiness(RolUserData rolUserData, ILogger<RolUserBusiness> logger)
        {
            _rolUserData = rolUserData;
            _logger = logger;
        }

        // Método para obtener todos los rol users como DTOs
        public async Task<IEnumerable<RolUserDto>> GetAllRolUsersAsync()
        {
            try
            {
                var rolusers = await _rolUserData.GetAllAsync();
                return MapToDTOList(rolusers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los roles con los usuarios");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de los roles con los usuarios", ex);
            }
        }

        // Método para obtener un rol users por ID como DTO
        public async Task<RolUserDto> GetRolUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un rol de usuario con ID inválido: {RolUserId}", id);
                throw new Utilities.Exceptions.ValidationException("id", "El ID del rol de usuario debe ser mayor que cero");
            }
            try
            {
                var roluser = await _rolUserData.GetByIdAsync(id);
                if (roluser == null)
                {
                    _logger.LogInformation("No se encontró ningún usuario con ID: {RolUserId}", id);
                    throw new EntityNotFoundException("Usuario", id);
                }

                return MapToDTO(roluser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario con ID: {RolUserId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el usuario con ID {id}", ex);
            }
        }

        // Método para crear un rol user desde un DTO

        public async Task<RolUserDto> CreateRolUsersAsync(RolUserDto RolUserDto)
        {
            try
            {
                ValidateRolUser(RolUserDto);

                var rolsuser = MapToEntity(RolUserDto);

                var RolUserCreado = await _rolUserData.CreateAsyncSql(rolsuser);

                return MapToDTO(RolUserCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo rol user: {RolUserId}", RolUserDto?.RolUserId ?? 0);
                throw new ExternalServiceException("Base de datos", "Error al crear el usuario", ex);
            }
        }

       
        // Actualiza un rolUser existente.

        public async Task<bool> UpdateRolUserAsync(RolUserDto rolUserDto)
        {
            try
            {
                ValidateRolUser(rolUserDto);

                var existingRolUser = await _rolUserData.GetByIdAsync(rolUserDto.RolUserId);
                if (existingRolUser == null)
                {
                    throw new EntityNotFoundException("RolUser", rolUserDto.RolUserId);
                }

                // Actualizar propiedades
                //existingRolUser.IsDeleted= rolUserDto.IsDeleted;
                existingRolUser.RolId = rolUserDto.RolId;
                existingRolUser.UserId = rolUserDto.UserId;


                return await _rolUserData.UpdateAsync(existingRolUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Rol User con ID: {RolUserId}", rolUserDto.RolUserId);
                throw new ExternalServiceException("Base de datos", "Error al actualizar el Rol User.", ex);
            }
        }

        // Método para borrar lógicamente un rol
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var existingRolUser = await _rolUserData.GetByIdAsync(id);
                if (existingRolUser == null)
                {
                    throw new EntityNotFoundException("Rol User", id);
                }

                return await _rolUserData.DeleteLogicAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar lógicamente el rol user con ID {id}");
                throw new ExternalServiceException("Base de datos", $"Error al eliminar lógicamente el rol user con ID {id}", ex);
            }
        }

        /// Elimina un rol user por ID.

        public async Task<bool> DeleteRolUserAsync(int id)
        {
            try
            {
                var existingModule = await _rolUserData.GetByIdAsync(id);
                if (existingModule == null)
                {
                    throw new EntityNotFoundException("Rol User", id);
                }

                return await _rolUserData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el rol user con ID: {RolUserId}", id);
                throw new ExternalServiceException("Base de datos", "Error al eliminar el rol user.", ex);
            }
        }

        // Método para validar el DTO
        private void ValidateRolUser(RolUserDto RolUserDto)
        {
            if (RolUserDto == null)
            {
                throw new Utilities.Exceptions.ValidationException("El objeto Usuario no puede ser nulo");
            }
            if (RolUserDto.UserId <= 0)
            {
                _logger.LogError("no se pudo crear");
                throw new Utilities.Exceptions.ValidationException("no se pudo");
            }
        }
        // Método para mapear de User a UserDto
        private RolUserDto MapToDTO(RolUsers rolUser)
        {
            return new RolUserDto
            {
                RolUserId = rolUser.Id,
                RolId = rolUser.RolId,
                UserId = rolUser.UserId,
            //    IsDeleted = rolUser.IsDeleted

            };
        }

        // Método para mapear de RolDto a Rol
        private RolUsers MapToEntity(RolUserDto RolUserDto)
        {
            return new RolUsers
            {
                Id = RolUserDto.RolUserId,
                RolId = RolUserDto.RolId,
                UserId = RolUserDto.UserId,
               // IsDeleted = RolUserDto.IsDeleted
            };
        }

        // Método para mapear una lista de Rol a una lista de RolDto
        private IEnumerable<RolUserDto> MapToDTOList(IEnumerable<RolUsers> rolUsers)
        {
            var rolUserDto = new List<RolUserDto>();
            foreach (var rolUser in rolUsers)
            {
                rolUserDto.Add(MapToDTO(rolUser));
            }
            return rolUserDto;
        }
    }
}
