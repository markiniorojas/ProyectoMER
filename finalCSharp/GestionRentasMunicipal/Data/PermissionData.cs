using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Data
{
    /// <summary>
    /// Repositorio encargado de la getion de la entidad PermissionData en la base de datos
    /// </summary>
    public class PermissionData
    {
        /// <summary>
        /// Constructor que recibe recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Intancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos.</param>

        private readonly ApplicationDbContext _context;
        private readonly ILogger<PermissionData> _logger;

        public PermissionData(ApplicationDbContext context, ILogger<PermissionData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los FormData almacenados en la base de datos SQL
        /// </summary>
        public async Task<IEnumerable<Permissions>> GetAllAsyncSQL()
        {
            string query = @"SELECT * FROM Permissions";
            return (IEnumerable<Permissions>)await _context.QueryAsync<IEnumerable<Permissions>>(query);

            //return await _context.Set<Permission>().ToListAsync();
        }

        /// <summary>
        /// Obtiene todos los permisos almacenados en la base de datos.
        /// </summary>
        /// <returns>Lista de los permisos</returns>
        /// 
        public async Task<IEnumerable<Permissions>> GetAllAsync()
        {
            return await _context.Set<Permissions>().ToListAsync();
        }

        /// <summary>
        /// Obtiene un PermissionData especifico por su identificacion SQL
        /// </summary
        public async Task<Permissions?> GetByIdAsyncSQL(int id)
        {
            try
            {
                string query = @"SELECT * FROM Permissions WHERE Id = @Id";
                return await _context.QueryFirstOrDefaultAsync<Permissions>(query, new { Id = id });

                //return await _context.Set<Form>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el permiso con ID {FormId}", id);
                throw;
            }

        }

        /// <summary>
        /// Obtiene un permisos especifico por su identificador
        /// </summary>
        public async Task<Permissions?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Permissions>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener permiso con ID {PermissioniD}", id);
                throw; //Re-lanza la excepcion para sea manejada en capas superiores
            }
        }

        /// <summary>
        /// Crea un nuevo permiso en la base de datos
        /// </summary>
        /// <param name="permission"></param>
        /// <returns>el permiso fue creado.</returns>
        /// 

        // Metodo para crear un nuevo permiso con linQ
        public async Task<Permissions> CreateAsync(Permissions permission)
        {
            try
            {
                await _context.Set<Permissions>().AddAsync(permission);
                await _context.SaveChangesAsync();
                return permission;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el permiso: {ex.Message}");
                throw;
            }
        }

        // Metodo para crear un nuevo permiso con sentencia Sql

        public async Task<Permissions> CreateAsyncSql(Permissions permission)
        {
            try
            {
                string query = @"
                    INSERT INTO Permissions (Name, Description) 
                    VALUES (@Name, @Description);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    permission.Name,
                    permission.Description
                });

                permission.Id = newId;
                return permission;

                //await _context.Set<permission>().AddAsync(permission);
                //await _context.SaveChangesAsync();
                //return form;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el formulario: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza un permiso existente en la base de datos
        /// </summary>
        /// <param name="permission">Objeto con la informacion actualizada</param>
        /// <returns>True si la operacion fue exitosa, False en caso contrario</returns>
        /// 

        //Metodo para actualizar un permiso en la base de datos con linQ
        public async Task<bool> UpdateAsync(Permissions permission)
        {
            try
            {
                _context.Set<Permissions>().Update(permission);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erroe al actualizar el permiso: {ex.Message}");
                return false;
            }
        }

      
        // Actualiza un PermissionData existente en la base de datos SQL
 
        public async Task<bool> UpdateAsyncSql(Permissions permission)
        {
            try
            {
                string query = @"
                    UPDATE Permissions 
                    SET Name = @Name, Description = @Description
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    permission.Id,
                    permission.Name,
                    permission.Description
                });

                return rowsAffected > 0;

                //_context.Set<Permission>().Update(permission);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el permiso : {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Realiza una eliminación lógica del rol con el ID especificado.
        /// </summary>
        /// <param name="id">ID del rol a eliminar lógicamente.</param>
        /// <returns>True si se marcó como eliminado, False si no se encontró.</returns>
        /// 

        // Metodo de eliminador logico en linQ
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var permission = await _context.Set<Permissions>().FirstOrDefaultAsync(r => r.Id == id);
                if (permission == null)
                    return false;

                permission.IsDeleted = true; // Marcamos como eliminado
                _context.Set<Permissions>().Update(permission);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar eliminación lógica del permiso con ID {PermissionId}", id);
                return false;
            }
        }


        // Metodo de eliminador logico en sentencia SQL:
        public async Task<bool> DeleteLogicAsyncSql(int id)
        {
            try
            {
                string query = @"
            UPDATE Permissions SET IsDeleted = 1 WHERE Id = @Id;
            SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente el permiso: {ex.Message}");
                return false;
            }
        }


        /// <summary>
        /// Elimina un permiso
        /// </summary>
        /// <param name="id">Elimina con el ID del permiso .</param>
        /// <returns>True si se marcó como eliminado,</returns>
        /// 

        //Metodo para eliminar un permiso en la base de datos con linQ
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var permission = await _context.Set<Permissions>().FindAsync(id);
                if (permission == null)
                    return false;

                _context.Set<Permissions>().Remove(permission);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el permiso: {ex.Message}");
                return false;
            }
        }

        //Metodo para eliminar un permiso en la base de datos con sentencia SQl
        public async Task<bool> DeleteAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM Permissions WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;

                //var permission = await _context.Set<Permission>().FindAsync(id);
                //if (permission == null)
                //    return false;
                //_context.Set<Permission>().Remove(permission);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el permiso {ex.Message}");
                return false;
            }
        }
    }
}
