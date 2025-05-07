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
    public class RolBusiness
    {
        private readonly RolData _rolData;
        private readonly ILogger<RolBusiness> _logger;

        public RolBusiness(RolData rolData, ILogger<RolBusiness> logger)
        {
            _rolData = rolData;
            _logger = logger;
        }

        // Método para obtener todos los roles como DTOs
        public async Task<IEnumerable<RolDto>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _rolData.GetAllAsync();
                return MapToDTOList(roles);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los roles");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de roles", ex);
            }
        }

        // Método para obtener un rol por ID como DTO
        public async Task<RolDto> GetRolByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un rol con ID inválido: {RolId}", id);
                throw new Utilities.Exceptions.ValidationException("id", "El ID del rol debe ser mayor que cero");
            }

            try
            {
                var rol = await _rolData.GetByIdAsync(id);
                if (rol == null)
                {
                    _logger.LogInformation("No se encontró ningún rol con ID: {RolId}", id);
                    throw new EntityNotFoundException("Rol", id);
                }

                return MapToDTO(rol);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rol con ID: {RolId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el rol con ID {id}", ex);
            }
        }

        // Método para crear un rol desde un DTO
        public async Task<RolDto> CreateRolAsync(RolDto rolDto)
        {
            try
            {
                ValidateRol(rolDto);

                var rol = MapToEntity(rolDto);

                var rolCreado = await _rolData.CreateAsync(rol);

                return MapToDTO(rolCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo rol: {RolNombre}", rolDto?.RolName ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el rol", ex);
            }
        }

        // Método para actualizar un rol
        public async Task<bool> UpdateRolAsync(RolDto rolDto)
        {
            try
            {
                ValidateRol(rolDto);

                var existingRol = await _rolData.GetByIdAsync(rolDto.RolId);
                if (existingRol == null)
                {
                    throw new EntityNotFoundException("Rol", rolDto.RolId);
                }

                // Actualizar propiedades
                existingRol.IsDeleted = rolDto.IsDeleted;
                existingRol.Name = rolDto.RolName;

                return await _rolData.UpdateAsync(existingRol);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el rol con ID {rolDto?.RolId}");
                throw new ExternalServiceException("Base de datos", $"Error al actualizar el rol con ID {rolDto?.RolId}", ex);
            }
        }

        // Método para borrar lógicamente un rol
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var existingRol = await _rolData.GetByIdAsync(id);
                if (existingRol == null)
                {
                    throw new EntityNotFoundException("Rol", id);
                }

                return await _rolData.DeleteLogicAsync(id); // Asegúrate de tener este en tu capa Data
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar lógicamente el rol con ID {id}");
                throw new ExternalServiceException("Base de datos", $"Error al eliminar lógicamente el rol con ID {id}", ex);
            }
        }


        // Método para eliminar un rol
        public async Task<bool> DeleteRolAsync(int id)
        {
            try
            {
                var existingRol = await _rolData.GetByIdAsync(id);
                if (existingRol == null)
                {
                    throw new EntityNotFoundException("Rol", id);
                }

                return await _rolData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar físicamente el rol con ID {id}");
                throw new ExternalServiceException("Base de datos", $"Error al eliminar físicamente el rol con ID {id}", ex);
            }
        }


        // Método para validar el DTO
        private void ValidateRol(RolDto rolDto)
        {
            if (rolDto == null)
            {
                throw new Utilities.Exceptions.ValidationException("El objeto rol no puede ser nulo");
            }

            if (string.IsNullOrWhiteSpace(rolDto.RolName))
            {
                _logger.LogWarning("Se intentó crear/actualizar un rol con Name vacío");
                throw new Utilities.Exceptions.ValidationException("Name", "El Name del rol es obligatorio");
            }
        }

        // Método para mapear de Rol a RolDto
        private RolDto MapToDTO(Rols rol)
        {
            return new RolDto
            {
                RolId = rol.Id,
                RolName = rol.Name,
                IsDeleted = rol.IsDeleted 
            };
        }

        // Método para mapear de RolDto a Rol
        private Rols MapToEntity(RolDto rolDto)
        {
            return new Rols
            {
                Id = rolDto.RolId,
                Name = rolDto.RolName,
                IsDeleted = rolDto.IsDeleted // Añadir esta línea
            };
        }

        // Método para mapear una lista de Rol a una lista de RolDto
        private IEnumerable<RolDto> MapToDTOList(IEnumerable<Rols> rols)
        {
            var rolsDto = new List<RolDto>();
            foreach (var rol in rols)
            {
                rolsDto.Add(MapToDTO(rol));
            }
            return rolsDto;
        }
    }
}