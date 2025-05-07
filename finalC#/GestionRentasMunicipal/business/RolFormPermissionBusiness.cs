using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los rol form permission del sistema.
    /// </summary>
    public class RolFormPermissionBusiness
    {
        private readonly RolFormPermissionData _rolFormPermissionData;
        private readonly ILogger<RolFormPermissionBusiness> _logger;

        public RolFormPermissionBusiness(RolFormPermissionData rolFormPermissionData, ILogger<RolFormPermissionBusiness> logger)
        {
            _rolFormPermissionData = rolFormPermissionData;
            _logger = logger;
        }

        // Método para obtener todos los rol form permission como DTOs
        public async Task<IEnumerable<RolFormPermissionDto>> GetAllRolFormPermissionsAsync()
        {
            try
            {
                var rolFormPermissions = await _rolFormPermissionData.GetAllAsync();
                return MapToDTOList(rolFormPermissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los permisos de rolformpermission");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de rolFormPermission", ex);
            }
        }


        // Método para obtener un rol form permission por ID como DTO
        public async Task<RolFormPermissionDto> GetRolFormPermissionByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un RolFormPermission con ID inválido: {id}", id);
                throw new ValidationException("id", "El ID del RolFormPermission debe ser mayor que cero");
            }
            try
            {
                var rolFormPermission = await _rolFormPermissionData.GetByIdAsync(id);
                if (rolFormPermission == null)
                {
                    _logger.LogInformation("No se encontró ningún RolFormPermission con ID: {RolFormPermissionId}", id);
                    throw new EntityNotFoundException("RolFormPermission", id);
                }

                return MapToDTO(rolFormPermission);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el RolFormPermission con ID: {RolFormPermissionId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el RolFormPermission con ID {id}", ex);
            }
        }

        // Método para crear un rol form permission desde un DTO
        public async Task<RolFormPermissionDto> CreateRolFormPermissionAsync(RolFormPermissionDto RolFormPermissionDto)
        {
            try
            {
                ValidateRolFormPermission(RolFormPermissionDto);

                var rolFormPermission = MapToEntity(RolFormPermissionDto);

                var RolFormPermissionCreado = await _rolFormPermissionData.CreateAsync(rolFormPermission);

                return MapToDTO(RolFormPermissionCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo permiso: {RolFormPermissionId}", RolFormPermissionDto?.RolFormPermissionId ?? 0);
                throw new ExternalServiceException("Base de datos", "Error al crear el rolFormPermission", ex);
            }
        }

        /// <summary>
        /// Actualiza un modulo existente.
        /// </summary>
        public async Task<bool> UpdateRolFormPermissionAsync(RolFormPermissionDto rolFormPermissionDto)
        {
            try
            {
                ValidateRolFormPermission(rolFormPermissionDto);

                var existingRolFormPermission = await _rolFormPermissionData.GetByIdAsync(rolFormPermissionDto.RolFormPermissionId);
                if (existingRolFormPermission == null)
                {
                    throw new EntityNotFoundException("RolFormPermission", rolFormPermissionDto.RolFormPermissionId);
                }

                // Actualizar propiedades
                //existingForm.Active = formDTO.Status;
                existingRolFormPermission.RolId = rolFormPermissionDto.RolId;
                existingRolFormPermission.FormId = rolFormPermissionDto.FormId;
                existingRolFormPermission.PermissionId = rolFormPermissionDto.PermissionId;


                return await _rolFormPermissionData.UpdateAsync(existingRolFormPermission);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el RolFormPermission User con ID: {RolFormPermissionId}", rolFormPermissionDto.RolFormPermissionId);
                throw new ExternalServiceException("Base de datos", "Error al actualizar el RolFormPermission.", ex);
            }
        }


        /// <summary>
        /// Elimina un formulario por ID.
        /// </summary>
        public async Task<bool> DeleteRolFormPermissionAsync(int id)
        {
            try
            {
                var existingRolFormPermission = await _rolFormPermissionData.GetByIdAsync(id);
                if (existingRolFormPermission == null)
                {
                    throw new EntityNotFoundException("RolFormPermission", id);
                }

                return await _rolFormPermissionData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el rolformpermission con ID: {RolFormPermissionId}", id);
                throw new ExternalServiceException("Base de datos", "Error al eliminar el rol form permission.", ex);
            }
        }

        // Método para validar el DTO

        private void ValidateRolFormPermission(RolFormPermissionDto RolFormPermissionDto)
        {
            if (RolFormPermissionDto == null)
            {
                throw new ValidationException("El objeto RolFormPermission no puede ser nulo");
            }
            if (RolFormPermissionDto.RolId <= 0)
            {
                _logger.LogWarning("Se intentó crear/actualizar un RolFormPermission con RolId inválido");
                throw new ValidationException("RolId", "El RolId es obligatorio y debe ser mayor que cero");
            }
        }

        // Método para mapear de Rolformpermission a RolformpermissionDTO
        private RolFormPermissionDto MapToDTO(RolFormPermissions rolFormPermission)
        {
            return new RolFormPermissionDto
            {
                RolFormPermissionId = rolFormPermission.Id,
                RolId = rolFormPermission.RolId,
                FormId = rolFormPermission.FormId,
                PermissionId = rolFormPermission.PermissionId
            };
        }

        // Método para mapear de RolformpermissionDTO a Rolformpermission
        private RolFormPermissions MapToEntity(RolFormPermissionDto RolFormPermissionDto)
        {
            return new RolFormPermissions
            {
                Id = RolFormPermissionDto.RolFormPermissionId,
                RolId = RolFormPermissionDto.RolId,
                FormId = RolFormPermissionDto.FormId,
                PermissionId = RolFormPermissionDto.PermissionId,
            };
        }

        // Método para mapear una lista de Rolformpermission a una lista de RolformpermissionDTO
        private IEnumerable<RolFormPermissionDto> MapToDTOList(IEnumerable<RolFormPermissions> rolFormPermissions)
        {
            var rolFormPermissionsDto = new List<RolFormPermissionDto>();
            foreach (var rolFormPermission in rolFormPermissions)
            {
                rolFormPermissionsDto.Add(MapToDTO(rolFormPermission));
            }
            return rolFormPermissionsDto;
        }
    }
}
