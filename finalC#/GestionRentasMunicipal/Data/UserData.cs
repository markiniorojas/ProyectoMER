using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Data
{
    /// <summary>
    /// Repositorio encargado de la getion de la entidad User en la base de datos
    /// </summary>
    public class UserData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserData> _logger;

        /// <summary>
        /// Constructor que recibe recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Intancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos.</param>

        public UserData(ApplicationDbContext context, ILogger<UserData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los usuarios almacenados en la base de datos.
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        /// 

        //Metodo SQL
        public async Task<IEnumerable<Users>> GetAllAsyncSql()
        {
            string query = @"SELECT * FROM Users";
            return (IEnumerable<Users>)await _context.QueryAsync<IEnumerable<Users>>(query);

            //return await _context.Set<User>().ToListAsync();
        }

        //Metodo LinQ
        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _context.Set<Users>().ToListAsync();
        }

        /// <summary>
        /// Obtiene un usuario especifico por su identificador con Sql
        /// </summary>
        /// 

        // Obtiene un usuario especifico por su identificador con linQ

        public async Task<Users?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Users>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuario con ID {UserId}", id);
                throw;//Re-lanza la excepcion para sea manejada en capas superiores
            }
        }

        // Obtiene un usuario especifico por su identificador con Sql

        public async Task<Users?> GetByIdAsyncSql(int id)
        {
            try
            {
                string query = @"SELECT * FROM Users WHERE Id = @Id";
                return await _context.QueryFirstOrDefaultAsync<Users>(query, new { Id = id });

                //return await _context.Set<User>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario con ID {UserId}", id);
                throw;
            }

        }



        /// <summary>
        /// Crea un nuevo usuario en la base de datos
        /// </summary>
        /// <param name="user"></param>
        /// <returns>el usuario creado.</returns>
        /// 

        // Metodo para crear nuevos usuarios con linQ
        public async Task<Users> CreateAsync(Users user)
        {
            try
            {
                await _context.Set<Users>().AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el usuario: {ex.Message}");
                throw;
            }
        }

        // Metodo para crear nuevos usuarios con Sql:
        public async Task<Users> CreateAsyncSql(Users user)
        {
            try
            {
                string query = @"
                    INSERT INTO Users (Name, LastName, Email, Password, Identification, Phone, Address ) 
                    VALUES (@Name, @LastName, @Email, @Password, @Identification, @Phone, @Address);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    user.Name,
                    user.LastName,
                    user.Email,
                    user.Password,
                    user.Identification,
                    user.Phone,
                    user.Address

                });

                user.Id = newId;
                return user;

                //await _context.Set<User>().AddAsync(user);
                //await _context.SaveChangesAsync();
                //return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el usuario: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza un usuario existente en la base de datos
        /// </summary>
        /// <param name="user">Objeto con la informacion actualizada</param>
        /// <returns>True si la operacion fue exitosa, False en caso contrario</returns>

        // Metodo para actualizar usuarios con linQ
        public async Task<bool> UpdateAsync(Users user)
        {
            try
            {
                _context.Set<Users>().Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el usuario: {ex.Message}");
                return false;
            }
        }

        // Metodo para actualizar usuarios con Sql
        
        public async Task<bool> UpdateAsyncSQL(Users user)
        {
            try
            {
                string query = @"
                    UPDATE Users 
                    SET Name = @Name, LastName = @LastName,  Email = @Email, Password = @Password, Identification = @Identification,
                    Phone = @Phone, Address = @Address
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    user.Id,
                    user.Name,
                    user.LastName,
                    user.Email,
                    user.Password,
                    user.Identification,
                    user.Phone,
                    user.Address

                });

                return rowsAffected > 0;

                //_context.Set<User>().Update(user);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el usuario : {ex.Message}");
                return false;
            }
        }


        /// <summary>
        /// Realiza una eliminación lógica del usuario con el ID especificado.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar lógicamente.</param>
        /// <returns>True si se marcó como eliminado, False si no se encontró.</returns>
        /// 

        // Metodo de eliminador logico en linQ
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var user = await _context.Set<Users>().FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                    return false;

                user.IsDeleted = true; // Marcamos como eliminado
                _context.Set<Users>().Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar eliminación lógica del user con ID {UserId}", id);
                return false;
            }
        }


        // Metodo de eliminador logico en sentencia SQL:
        public async Task<bool> DeleteLogicAsyncSql(int id)
        {
            try
            {
                string query = @"
            UPDATE Users SET IsDeleted = 1 WHERE Id = @Id;
            SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente el usuario: {ex.Message}");
                return false;
            }
        }


        /// <summary>
        /// Realiza una eliminación definitiva del usuario con el ID especificado.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar lógicamente.</param>
        /// <returns>True si se marcó como eliminado, False si no se encontró.</returns>
        /// 

        /// Metodo para eliminar usuarios con linQ
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var user = await _context.Set<Users>().FindAsync(id);
                if (user == null)
                    return false;
                _context.Set<Users>().Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el usuario: {ex.Message}");
                return false;
            }
        }

        /// Metodo para eliminar usuarios con Sql
        public async Task<bool> DeleteAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM Users WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;

                //var user = await _context.Set<User>().FindAsync(id);
                //if (user == null)
                //    return false;
                //_context.Set<User>().Remove(user);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el usuario {ex.Message}");
                return false;
            }
        }

    }
}
