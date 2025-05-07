using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{

    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los permisos del sistema.
    /// </summary>
    public class PermissionBusiness
    {
        private readonly PermissionData _permissionData;
        private readonly ILogger<PermissionBusiness> _logger;

        public PermissionBusiness(PermissionData permissionData, ILogger<PermissionBusiness> logger)
        {
            _permissionData = permissionData;
            _logger = logger;
        }

        // Método para obtener todos los permisos como DTOs
        public async Task<IEnumerable<PermissionDto>> GetAllPermissionAsync()
        {
            try
            {
                var permissions = await _permissionData.GetAllAsync();

                return MapToDTOList(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los permisos");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de permisos", ex);
            }
        }

        // Método para obtener un permiso por ID como DTO
        public async Task<PermissionDto> GetPermissionByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un permiso con ID inválido: {PermissionId}", id);
                throw new Utilities.Exceptions.ValidationException("id", "El ID del permiso debe ser mayor que cero");
            }

            try
            {
                var permission = await _permissionData.GetByIdAsync(id);
                if (permission == null)
                {
                    _logger.LogInformation("No se encontró ningún permiso con ID: {PermissionId}", id);
                    throw new EntityNotFoundException("Permiso", id);
                }

                return MapToDTO(permission);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el permiso con ID: {PermissionId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el permiso con ID {id}", ex);
            }
        }

        // Método para crear un permiso desde un DTO
        public async Task<PermissionDto> CreatePermissionAsync(PermissionDto permissionDto)
        {
            try
            {
                ValidatePermission(permissionDto);

                var permission = MapToEntity(permissionDto);

                var permissionCreado = await _permissionData.CreateAsync(permission);

                return MapToDTO(permissionCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo permiso: {PermissionName}", permissionDto?.PermissionName ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el permiso", ex);
            }
        }

        // Actualiza un formulario existente.
        public async Task<bool> UpdatePermissionAsync(PermissionDto permissionDto)
        {
            try
            {
                ValidatePermission(permissionDto);

                var existingPermission = await _permissionData.GetByIdAsync(permissionDto.PermissionId);
                if (existingPermission == null)
                {
                    throw new EntityNotFoundException("Permission", permissionDto.PermissionId);
                }

                // Actualizar propiedades
                existingPermission.IsDeleted = permissionDto.IsDeleted;
                existingPermission.Name = permissionDto.PermissionName;
                existingPermission.Description = permissionDto.PermissionDescription;

                return await _permissionData.UpdateAsync(existingPermission);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el permiso con ID: {PermissionId}", permissionDto.PermissionId);
                throw new ExternalServiceException("Base de datos", "Error al actualizar el permiso.", ex);
            }
        }

        // Método para borrar lógicamente un permiso
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var existingPermission = await _permissionData.GetByIdAsync(id);
                if (existingPermission == null)
                {
                    throw new EntityNotFoundException("Permiso", id);
                }

                return await _permissionData.DeleteLogicAsync(id); // Asegúrate de tener este en tu capa Data
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar lógicamente el permiso con ID {id}");
                throw new ExternalServiceException("Base de datos", $"Error al eliminar lógicamente el permiso con ID {id}", ex);
            }
        }

        // Elimina un permiso por ID.

        public async Task<bool> DeletePermissionAsync(int id)
        {
            try
            {
                var existingForm = await _permissionData.GetByIdAsync(id);
                if (existingForm == null)
                {
                    throw new EntityNotFoundException("Permission", id);
                }

                return await _permissionData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el permiso con ID: {PermissionId}", id);
                throw new ExternalServiceException("Base de datos", "Error al eliminar el permiso.", ex);
            }
        }

        // Método para validar el DTO
        private void ValidatePermission(PermissionDto PermissionDto)
        {
            if (PermissionDto == null)
            {
                throw new Utilities.Exceptions.ValidationException("El objeto permiso no puede ser nulo");
            }

            if (string.IsNullOrWhiteSpace(PermissionDto.PermissionName))
            {
                _logger.LogWarning("Se intentó crear/actualizar un permiso con Name vacío");
                throw new Utilities.Exceptions.ValidationException("Name", "El Name del permiso es obligatorio");
            }
        }

        // Método para mapear de User a UserDto
        private PermissionDto MapToDTO(Permissions permission)
        {
            return new PermissionDto
            {
                PermissionId = permission.Id,
                PermissionName = permission.Name,
                PermissionDescription = permission.Description,
                IsDeleted = permission.IsDeleted
            };
        }

        // Método para mapear de RolDto a Rol
        private Permissions MapToEntity(PermissionDto PermissionDto)
        {
            return new Permissions
            {
                Name = PermissionDto.PermissionName,
                Description = PermissionDto.PermissionDescription,
                IsDeleted = PermissionDto.IsDeleted
            };
        }

        // Método para mapear una lista de Rol a una lista de RolDto
        private IEnumerable<PermissionDto> MapToDTOList(IEnumerable<Permissions> permissions)
        {
            var permissionDto = new List<PermissionDto>();
            foreach (var permission in permissions)
            {
                permissionDto.Add(MapToDTO(permission));
            }
            return permissionDto;
        }
    }
}
