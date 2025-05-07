using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;


namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los usuarios del sistema.
    /// </summary>
    public class UserBusiness
    {
        private readonly UserData _userData;
        private readonly ILogger<UserBusiness> _logger;

        public UserBusiness(UserData userData, ILogger<UserBusiness> logger)
        {
            _userData = userData;
            _logger = logger;
        }

        // Método para obtener todos los usuarios como DTOs
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userData.GetAllAsync();

                return MapToDTOList(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos lo usuarios");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de usuarios", ex);
            }
        }


        // Método para obtener un usuario por ID como DTO
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {

                _logger.LogWarning("Se intento obtener un usuario con ID invalido: {UserId}", id);
                throw new ValidationException("id", "El ID del usuario debe ser mayor que cero");
            }
            try
            {
                var user = await _userData.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogInformation("No se encontró ningún usuario con ID: {UserId}", id);
                    throw new EntityNotFoundException("Usuario", id);
                }

                return MapToDTO(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario con ID: {UserId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el usuario con ID {id}", ex);
            }
        }

        // Método para crear un usuario desde un DTO
        public async Task<UserDto> CreateUserAsync(UserDto UserDto)
        {
            try
            {
                ValidateUser(UserDto);
                var user = MapToEntity(UserDto);

                var UserCreado = await _userData.CreateAsync(user);

                return MapToDTO(UserCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo usuario: {UserName}", UserDto?.Name ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el usuario", ex);
            }
        }

   
        // Actualiza un formulario existente.

        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            try
            {
                ValidateUser(userDto);

                var existingUser = await _userData.GetByIdAsync(userDto.Id);
                if (existingUser == null)
                {
                    throw new EntityNotFoundException("User", userDto.Id);
                }

                existingUser.IsDeleted = userDto.IsDeleted;
                existingUser.Name = userDto.Name;
                existingUser.Email = userDto.Email;
                existingUser.Password = userDto.Password;
                existingUser.Phone = userDto.Phone;
                existingUser.Identification = userDto.Identification;
                existingUser.Address = userDto.Address;

                return await _userData.UpdateAsync(existingUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el usuario con ID: {UserId}", userDto.Id);
                throw new ExternalServiceException("Base de datos", "Error al actualizar el usuario.", ex);
            }
        }

        // Método para borrar lógicamente un rol
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var existingUser = await _userData.GetByIdAsync(id);
                if (existingUser == null)
                {
                    throw new EntityNotFoundException("User", id);
                }

                return await _userData.DeleteLogicAsync(id); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar lógicamente el user con ID {id}");
                throw new ExternalServiceException("Base de datos", $"Error al eliminar lógicamente el user con ID {id}", ex);
            }
        }


        // Elimina un usuario por ID.

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var existingUser = await _userData.GetByIdAsync(id);
                if (existingUser == null)
                {
                    throw new EntityNotFoundException("User", id);
                }

                return await _userData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el usuario con ID: {UserId}", id);
                throw new ExternalServiceException("Base de datos", "Error al eliminar el usuario.", ex);
            }
        }

        // Método para validar el DTO
        private void ValidateUser(UserDto UserDto)
        {
            if (UserDto == null)
            {
                throw new ValidationException("El objeto usuario no puede ser nulo");
            }
            if (string.IsNullOrWhiteSpace(UserDto.Name))
            {
                _logger.LogWarning("Se intento crear/actualucar un usuario con Name vacio");
                throw new ValidationException("Name", "El name del usuario es obligatorio");
            }
        }

        // Método para mapear de User a UserDto
        private UserDto MapToDTO(Users user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                Identification = user.Identification,
                Address = user.Address,
                IsDeleted = user.IsDeleted
            };
        }

        // Método para mapear de RolDto a Rol
        private Users MapToEntity(UserDto UserDto)
        {
            return new Users
            {
                Id = UserDto.Id,
                Name = UserDto.Name,
                LastName = UserDto.LastName,
                Email = UserDto.Email,
                Password = UserDto.Password,
                Phone = UserDto.Phone,
                Identification = UserDto.Identification,
                Address = UserDto.Address,
                IsDeleted = UserDto.IsDeleted
            };
        }

        // Método para mapear una lista de User a una lista de UserDto
        private IEnumerable<UserDto> MapToDTOList(IEnumerable<Users> users)
        {
            var usersDto = new List<UserDto>();
            foreach (var user in users)
            {
                usersDto.Add(MapToDTO(user));
            }
            return usersDto;
        }
    }
}
