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
    public class ModuleFormBusiness
    {
        private readonly ModuleFormData _moduleFormData;
        private readonly ILogger<ModuleFormBusiness> _logger;


        public ModuleFormBusiness(ModuleFormData moduleFormData, ILogger<ModuleFormBusiness> logger)
        {
            _moduleFormData = moduleFormData;
            _logger = logger;
        }

        // Método para obtener todos los modulos con sus formularios como DTOs
        public async Task<IEnumerable<ModuleFormDto>> GetAllModuleFormAsync()
        {
            try
            {
                var moduleForms = await _moduleFormData.GetAllAsync();
                return MapToDTOList(moduleForms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los modulos form");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de modules form", ex);
            }
        }

        // Método para obtener un module form por ID como DTO
        public async Task<ModuleFormDto> GetModuleFormByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un permiso con ID inválido: {ModuleFormId}", id);
                throw new Utilities.Exceptions.ValidationException("id", "El ID del modulo debe ser mayor que cero");
            }
            try
            {
                var moduleForms = await _moduleFormData.GetByIdAsync(id);
                if (moduleForms == null)
                {
                    _logger.LogInformation("No se encontró ningún modulo con su formulario con ID: {ModuleFormId}", id);
                    throw new EntityNotFoundException("Moduleform", id);
                }

                return MapToDTO(moduleForms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Moduleform con ID: {ModuleFormId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el modulo con ID {id}", ex);
            }
        }

        // Método para crear un Modulo con formularios desde un DTO
        public async Task<ModuleFormDto> CreateModuleFormAsync(ModuleFormDto ModuleFormDto)
        {
            try
            {
                ValidateModuleForm(ModuleFormDto);

                var moduleForm = MapToEntity(ModuleFormDto);

                var moduleFormCreado = await _moduleFormData.CreateAsync(moduleForm);

                return MapToDTO(moduleFormCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo moduloForm: {ModuleFormId}", ModuleFormDto?.ModuleFormId ?? 0);
                throw new ExternalServiceException("Base de datos", "Error al crear el modulo con formularios", ex);
            }
        }

        //Metodo para actualizar el moduleForm
        public async Task<bool> UpdateModuleFormAsync(ModuleFormDto moduleFormDto)
        {
            try
            {
                ValidateModuleForm(moduleFormDto);

                var existingModuleForm = await _moduleFormData.GetByIdAsync(moduleFormDto.ModuleFormId);
                if (existingModuleForm == null)
                {
                    throw new EntityNotFoundException("ModuleForm", moduleFormDto.ModuleFormId);
                }

                existingModuleForm.ModuleId = moduleFormDto.ModuleId;
                existingModuleForm.FormId = moduleFormDto.FormId;


                return await _moduleFormData.UpdateAsync(existingModuleForm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Rol User con ID: {RolUserId}", moduleFormDto.ModuleFormId);
                throw new ExternalServiceException("Base de datos", "Error al actualizar el Rol User.", ex);
            }
        }

        /// <summary>
        /// Elimina un modulo con formulario por ID.
        /// </summary>
        public async Task<bool> DeleteModuleFormAsync(int id)
        {
            try
            {
                var existingModuleForm = await _moduleFormData.GetByIdAsync(id);
                if (existingModuleForm == null)
                {
                    throw new EntityNotFoundException("ModuleForm", id);
                }

                return await _moduleFormData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el modulo con el formulario con ID: {ModuleFormId}", id);
                throw new ExternalServiceException("Base de datos", "Error al eliminar el modulo con el formulario.", ex);
            }
        }

        // Método para validar el DTO
        private void ValidateModuleForm(ModuleFormDto ModuleFormDto)
        {
            if (ModuleFormDto == null)
            {
                throw new Utilities.Exceptions.ValidationException("El objeto ModuleForm no puede ser nulo");

            }
            if (ModuleFormDto.ModuleId <= 0)
            {
                _logger.LogWarning("Se intentó crear/actualizar un modulo con ModuleId inválido");
                throw new Utilities.Exceptions.ValidationException("ModuleId", "El ModuleId es obligatorio y debe ser mayor que cero");
            }
        }

        // Método para mapear de User a UserDto
        private ModuleFormDto MapToDTO(ModuleForms moduleForm)
        {
            return new ModuleFormDto
            {
                ModuleFormId = moduleForm.Id,
                ModuleId = moduleForm.ModuleId,
                FormId = moduleForm.FormId
            };
        }


        // Método para mapear de RolDto a Rol
        private ModuleForms MapToEntity(ModuleFormDto ModuleFormDto)
        {
            return new ModuleForms
            {
                Id = ModuleFormDto.ModuleFormId,
                ModuleId = ModuleFormDto.ModuleId,
                FormId = ModuleFormDto.FormId
            };
        }

        // Método para mapear una lista de Rol a una lista de RolDto
        private IEnumerable<ModuleFormDto> MapToDTOList(IEnumerable<ModuleForms> moduleForms)
        {
            return moduleForms.Select(MapToDTO);
        }
    }
}
