using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    /// <summary>
    /// Repositorio encargado de la getion de la entidad RolUser en la base de datos
    /// </summary>
    public class RolUserData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolUserData> _logger;

        /// <summary>
        /// Constructor que recibe recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Intancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos.</param>

        public RolUserData(ApplicationDbContext context, ILogger<RolUserData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los modulos almacenados en la base de datos SQL
        /// </summary>
        public async Task<IEnumerable<RolUsers>> GetAllAsyncSql()
        {
            string query = @"SELECT * FROM RolUsers";
            return (IEnumerable<RolUsers>)await _context.QueryAsync<IEnumerable<RolUsers>>(query);

            //return await _context.Set<Module>().ToListAsync();
        }


        /// <summary>
        /// Obtiene todos los RolUser almacenados en la base de datos.
        /// </summary>
        /// <returns>Lista de RolUser con los usuarios</returns>
        /// 
        public async Task<IEnumerable<RolUsers>> GetAllAsync()
        {
            return await _context.Set<RolUsers>().ToListAsync();
        }
        /// <summary>
        /// Obtiene un usuario con un RolUser especificos por su identificador CON linQ
        /// </summary>
        public async Task<RolUsers?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<RolUsers>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuario con su rol con ID {RolUserId}", id);
                throw;//Re-lanza la excepcion para sea manejada en capas superiores
            }
        }

        /// <summary>
        /// Obtiene un RolUserData especifico por su identificacion SQL
        /// </summary
        public async Task<RolUsers?> GetByIdAsyncSql(int id)
        {
            try
            {
                string query = @"SELECT * FROM RolUsers WHERE Id = @Id";
                return await _context.QueryFirstOrDefaultAsync<RolUsers>(query, new { Id = id });

                //return await _context.Set<RolUsers>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rol user con ID {RolUserId}", id);
                throw;
            }

        }
        /// <summary>
        /// Crea un nuevo usuario con un rol definido en la base de datos
        /// </summary>
        /// <param name="user"></param>
        /// <returns>el usuario con su rol fue creado creado.</returns>
        /// 

        //Metodo para crear un nuevo rol user con linQ
        public async Task<RolUsers> CreateAsync(RolUsers rolUser)
        {
            try
            {
                await _context.Set<RolUsers>().AddAsync(rolUser);
                await _context.SaveChangesAsync();
                return rolUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el usuario con su rol user: {ex.Message}");
                throw;
            }
        }

        //Metodo para crear un nuevo rol user con sentencia SQl

        public async Task<RolUsers> CreateAsyncSql(RolUsers rolUsers)
        {
            try
            {
                string query = @"
                    INSERT INTO RolUsers (RolId, UserId) 
                    OUTPUT INSERTED.Id
                    VALUES (@RolId, @UserId);
                    ";

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    rolUsers.Id,
                    rolUsers.RolId,
                    rolUsers.UserId
                });
                return rolUsers;

                //await _context.Set<RolUser>().AddAsync(RolUser);
                //await _context.SaveChangesAsync();
                //return RolUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el modulo: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza un usuario con su rol existente en la base de datos
        /// </summary>
        /// <param name="user">Objeto con la informacion actualizada</param>
        /// <returns>True si la operacion fue exitosa, False en caso contrario</returns>
        /// 

        //Metodo para actualizar un rol user con linQ

        public async Task<bool> UpdateAsync(RolUsers rolUser)
        {
            try
            {
                _context.Set<RolUsers>().Update(rolUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el usuario con su rol: {ex.Message}");
                return false;
            }
        }

        //Metodo para actualizar un rol user con sentencias SQL

        public async Task<bool> UpdateAsyncSQL(RolUsers rolUser)
        {
            try
            {
                string query = @"
                    UPDATE RolUsers 
                    SET RolId = @RolId, UserId= @UserId
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    rolUser.Id,
                    rolUser.RolId,
                    rolUser.UserId,
                });

                return rowsAffected > 0;

                //_context.Set<RolUser>().Update(RolUser);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el rol user : {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Realiza una eliminación lógica del rolUser con el ID especificado.
        /// </summary>
        /// <param name="id">ID del rolUser a eliminar lógicamente.</param>
        /// <returns>True si se marcó como eliminado, False si no se encontró.</returns>
        /// 

        // Metodo de eliminador logico en linQ
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var rolUser = await _context.Set<RolUsers>().FirstOrDefaultAsync(r => r.Id == id);
                if (rolUser  == null)
                    return false;

                //rolUser.IsDeleted = true; // Marcamos como eliminado
                _context.Set<RolUsers>().Update(rolUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar eliminación lógica del rol con ID {RolUserId}", id);
                return false;
            }
        }


        // Metodo de eliminador logico en sentencia SQL:
        public async Task<bool> DeleteLogicAsyncSql(int id)
        {
            try
            {
                string query = @"
            UPDATE Rols SET IsDeleted = 1 WHERE Id = @Id;
            SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente el rolUser : {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un RolUserData de la base de datos SQL
        /// </summary>

        //Metodo para eliminar un rol user con linQ
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rolUser = await _context.Set<RolUsers>().FindAsync(id);
                if (rolUser == null)
                    return false;
                _context.Set<RolUsers>().Remove(rolUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el usuario con su rol: {ex.Message}");
                return false;
            }
        }

        //Metodo para eliminar un rol user con SQL

        public async Task<bool> DeleteAsyncSql(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM RolUsers WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;

                //var RolUser = await _context.Set<RolUser>().FindAsync(id);
                //if (RolUser == null)
                //    return false;
                //_context.Set<RolUser>().Remove(RolUser);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el RolUser {ex.Message}");
                return false;
            }
        }
    }
}
