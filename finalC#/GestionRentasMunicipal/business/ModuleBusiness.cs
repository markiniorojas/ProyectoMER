using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los modulos del sistema.
    /// </summary>
    public class ModuleBusiness
    {
        private readonly ModuleData _moduleData;
        private readonly ILogger<ModuleBusiness> _logger;

        public ModuleBusiness(ModuleData moduleData, ILogger<ModuleBusiness> logger)
        {
            _moduleData = moduleData;
            _logger = logger;
        }

        // Método para obtener todos los modulos como DTOs
        public async Task<IEnumerable<ModuleDto>> GetAllModulesAsync()
        {
            try
            {
                var modules = await _moduleData.GetAllAsync();

                return MapToDTOList(modules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los Modulos");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de Modulos", ex);
            }
        }
        // Método para obtener un modulos por ID como DTO
        public async Task<ModuleDto> GetModuleByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un modulo con ID inválido: {ModuleId}", id);
                throw new Utilities.Exceptions.ValidationException("id", "El ID del modulo debe ser mayor que cero");
            }
            try
            {
                var module = await _moduleData.GetByIdAsync(id);
                if (module == null)
                {
                    _logger.LogInformation("No se encontró ningún modulo con ID: {ModuleId}", id);
                    throw new EntityNotFoundException("Modulo", id);
                }

                return MapToDTO(module);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el modulo con ID: {ModuleId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el modulo con ID {id}", ex);
            }
        }

        // Método para crear un modulos desde un DTO
        public async Task<ModuleDto> CreateModuleAsync(ModuleDto ModuleDto)
        {
            try
            {
                ValidateModule(ModuleDto);

                var module = MapToEntity(ModuleDto);
                var moduleCreado = await _moduleData.CreateAsync(module);

                return MapToDTO(moduleCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo modulo: {ModuleNombre}", ModuleDto?.ModuleName ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el modulo", ex);
            }
        }

        /// <summary>
        /// Actualiza un modulo existente.
        /// </summary>
        public async Task<bool> UpdateModuleAsync(ModuleDto moduleDto)
        {
            try
            {
                ValidateModule(moduleDto);

                var existingModule = await _moduleData.GetByIdAsync(moduleDto.ModuleId);
                if (existingModule == null)
                {
                    throw new EntityNotFoundException("Form", moduleDto.ModuleId);
                }

                // Actualizar propiedades
                existingModule.IsDeleted = moduleDto.IsDeleted;
                existingModule.Name = moduleDto.ModuleName;
                existingModule.Description = moduleDto.ModuleDescription;
                existingModule.Code = moduleDto.ModuleCode;


                return await _moduleData.UpdateAsync(existingModule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el modulo con ID: {ModuleId}", moduleDto.ModuleId);
                throw new ExternalServiceException("Base de datos", "Error al actualizar el modulo.", ex);
            }
        }

        /// <summary>
        /// Metodo para ocultar con un modulo existente.
        /// </summary>
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var existingPermission = await _moduleData.GetByIdAsync(id);
                if (existingPermission == null)
                {
                    throw new EntityNotFoundException("Permiso", id);
                }

                return await _moduleData.DeleteLogicAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar lógicamente el modulo con ID {id}");
                throw new ExternalServiceException("Base de datos", $"Error al eliminar lógicamente el modulo  con ID {id}", ex);
            }
        }


        /// <summary>
        /// Elimina un formulario por ID.
        /// </summary>
        public async Task<bool> DeleteModuleAsync(int id)
        {
            try
            {
                var existingModule = await _moduleData.GetByIdAsync(id);
                if (existingModule == null)
                {
                    throw new EntityNotFoundException("Module", id);
                }

                return await _moduleData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el modulo con ID: {ModuleId}", id);
                throw new ExternalServiceException("Base de datos", "Error al eliminar el modulo.", ex);
            }
        }

        // Método para validar el DTO
        private void ValidateModule(ModuleDto moduleDto)
        {
            if (moduleDto == null)
            {
                throw new Utilities.Exceptions.ValidationException("Module", "El modulo no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(moduleDto.ModuleName))
            {
                throw new Utilities.Exceptions.ValidationException("ModuleName", "El nombre del modulo no puede estar vacío");
            }
        }

        // Método para mapear de modulos a modulosDto
        private ModuleDto MapToDTO(Modules module)
        {
            return new ModuleDto
            {
                ModuleId = module.Id,
                ModuleName = module.Name,
                ModuleDescription = module.Description,
                ModuleCode = module.Code,
                IsDeleted = module.IsDeleted
            };
        }

        // Método para mapear de modulosDto a modulo
        private Modules MapToEntity(ModuleDto moduleDto)
        {
            return new Modules
            {
                Name = moduleDto.ModuleName,
                Description = moduleDto.ModuleDescription,
                Code = moduleDto.ModuleCode,
                IsDeleted = moduleDto.IsDeleted
            };
        }

        // Método para mapear una lista de modulo a una lista de modulosDto
        private IEnumerable<ModuleDto> MapToDTOList(IEnumerable<Modules> modules)
        {
            var modulesDto = new List<ModuleDto>();
            foreach (var module in modules)
            {
                modulesDto.Add(MapToDTO(module));
            }
            return modulesDto;
        }
    }
}
