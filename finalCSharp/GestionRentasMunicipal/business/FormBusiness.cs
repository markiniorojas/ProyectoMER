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
    public class FormBusiness
    {
        private readonly FormData _formData;
        private readonly ILogger<FormData> _logger;

        public FormBusiness(FormData formData, ILogger<FormData> logger)
        {
            _formData = formData;
            _logger = logger;
        }

        // Método para obtener todos los formularios como DTOs
        public async Task<IEnumerable<FormDto>> GetAllFormsAsync()
        {
            try
            {
                var forms = await _formData.GetAllAsync();

                var formsDTO = MapToDTOList(forms);



                return formsDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los formularios");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de formularios", ex);
            }
        }

        // Método para obtener un formulario por ID como DTO
        public async Task<FormDto> GetFormByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un formulario con ID inválido: {FormId}", id);
                throw new Utilities.Exceptions.ValidationException("id", "El ID del formulario debe ser mayor que cero");
            }

            try
            {
                var form = await _formData.GetByIdAsync(id);
                if (form == null)
                {
                    _logger.LogInformation("No se encontró ningún formulario con ID: {FormId}", id);
                    throw new EntityNotFoundException("Formulario", id);
                }

                return MapToDTO(form);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el formulario con ID: {FormId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el formulario con ID {id}", ex);
            }
        }

        // Método para crear un formulario desde un DTO
        public async Task<FormDto> CreateFormAsync(FormDto FormDto)
        {
            try
            {
                ValidateForm(FormDto);

                var form = MapToEntity(FormDto);

                var formCreado = await _formData.CreateAsync(form);

                return MapToDTO(formCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo formulario: {FormName}", FormDto?.FormName ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el formulario", ex);
            }
        }

        /// <summary>
        /// Actualiza un formulario existente.
        /// </summary>
        public async Task<bool> UpdateFormAsync(FormDto formDto)
        {
            try
            {
                ValidateForm(formDto);

                var existingForm = await _formData.GetByIdAsync(formDto.FormId);
                if (existingForm == null)
                {
                    throw new EntityNotFoundException("Form", formDto.FormId);
                }

                // Actualizar propiedades
                existingForm.IsDeleted = formDto.IsDeleted;
                existingForm.Name = formDto.FormName;
                existingForm.Description = formDto.FormDescription;

                return await _formData.UpdateAsync(existingForm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el formulario con ID: {FormId}", formDto.FormId);
                throw new ExternalServiceException("Base de datos", "Error al actualizar el formulario.", ex);
            }
        }

        /// Elimina logicamente un formulario por ID.
    
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var existingForm = await _formData.GetByIdAsync(id);
                if (existingForm == null)
                {
                    throw new EntityNotFoundException("form", id);
                }

                return await _formData.DeleteLogicAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar lógicamente el formulario con ID {id}");
                throw new ExternalServiceException("Base de datos", $"Error al eliminar lógicamente el formulario con ID {id}", ex);
            }
        }


        // Elimina un formulario por ID.
       
        public async Task<bool> DeleteFormAsync(int id)
        {
            try
            {
                var existingForm = await _formData.GetByIdAsync(id);
                if (existingForm == null)
                {
                    throw new EntityNotFoundException("Form", id);
                }

                return await _formData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el formulario con ID: {FormId}", id);
                throw new ExternalServiceException("Base de datos", "Error al eliminar el formulario.", ex);
            }
        }

        // Método para validar el DTO
        private void ValidateForm(FormDto formDto)
        {
            if (formDto == null)
            {
                throw new Utilities.Exceptions.ValidationException("El objeto formulario no puede ser nulo");
            }

            if (string.IsNullOrWhiteSpace(formDto.FormName))
            {
                _logger.LogWarning("Se intentó crear/actualizar un formulario con Name vacío");
                throw new Utilities.Exceptions.ValidationException("Name", "El Name del formulario es obligatorio");
            }
        }

        // Método para mapear de modulos a modulosDto
        private FormDto MapToDTO(Forms form)
        {
            return new FormDto
            {
                FormId = form.Id,
                FormName = form.Name,
                FormDescription = form.Description,
                IsDeleted  = form.IsDeleted
            };
        }

        // Método para mapear de modulosDto a modulo
        private Forms MapToEntity(FormDto FormDto)
        {
            return new Forms
            {
                Id = FormDto.FormId,
                Name = FormDto.FormName,
                Description = FormDto.FormDescription,
                IsDeleted = FormDto.IsDeleted
            };
        }

        // Método para mapear una lista de modulo a una lista de modulosDto
        private IEnumerable<FormDto> MapToDTOList(IEnumerable<Forms> forms)
        {
            var FormDto = new List<FormDto>();
            foreach (var form in forms)
            {
                FormDto.Add(MapToDTO(form));
            }
            return FormDto;
        }
    }
}
