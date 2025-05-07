using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Data
{
    /// <summary>
    /// Repositorio encargado de la getion de la entidad Rol en la base de datos
    /// </summary>
    public class RolData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolData> _logger;

        /// <summary>
        /// Constructor que recibe  el contexto de base de datos.
        /// </summary>
        /// <param name="context">Intancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos.</param>

        public RolData(ApplicationDbContext context, ILogger<RolData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los RolData almacenados en la base de datos SQL
        /// </summary>
        public async Task<IEnumerable<Rols>> GetAllAsyncSql()
        {
            string query = @"SELECT * FROM Rols";
            return (IEnumerable<Rols>)await _context.QueryAsync<IEnumerable<Rols>>(query);

            //return await _context.Set<Form>().ToListAsync();
        }

        /// <summary>
        /// Obtiene todos los roles almacenados en la base de datos.
        /// </summary>
        /// <returns>Lista de roles</returns>
        public async Task<IEnumerable<Rols>> GetAllAsync()
        {
            return await _context.Set<Rols>().ToListAsync();
        }

        /// <summary>
        /// Obtiene un rol especifico por su identificador sql
        /// </summary>
        public async Task<Rols?> GetByIdAsyncSql(int id)
        {
            try
            {
                string query = @"SELECT * FROM Rols WHERE Id = @Id";
                return await _context.QueryFirstOrDefaultAsync<Rols>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rol con ID {RolId}", id);
                throw;
            }
        }


        /// <summary>
        /// Obtiene un Rol específico por su identificación LINQ
        /// </summary>
        public async Task<Rols?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Rols>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener un rol con ID {RolId}", id);
                throw;
            }
        }
        /// <summary>
        /// Crea un nuevo rol en la base de datos
        /// </summary>
        /// <param name="rol"></param>
        /// <returns>el rol creado.</returns>
        /// 

        // Metodo para crear un nuevo rol con sentencia linQ

        public async Task<Rols> CreateAsync(Rols rol)
        {
            try
            {
                await _context.Set<Rols>().AddAsync(rol);
                await _context.SaveChangesAsync();
                return rol;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el rol: {ex.Message}");
                throw;
            }
        }



        // Metodo para crear un nuevo rol con sentencia SQl
        // y recupera el Id del rol que se acaba de crear

        public async Task<Rols> CreateAsyncSQL(Rols rol)
        {
            try
            {
                string query = @"
                    INSERT INTO Rols (Name)  
                            VALUES(@Name);
                        SELECT CAST(SCOPE_IDENTITY() AS int); ";

                int newId = await _context.QuerySingleAsync<int>(query, new { rol.Name });

                rol.Id = newId;
                return rol;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el rol: {ex.Message}");
                throw;
            }
        }


        /// <summary>
        /// Actualiza un rol existente en la base de datos
        /// </summary>
        /// <param name="rol">Objeto con la informacion actualizada</param>
        /// <returns>True si la operacion fue exitosa, False en caso contrario</returns>
        /// 

        // Metodo para actualiza un rol con sentencia linQ
        public async Task<bool> UpdateAsync(Rols rol)
        {
            try
            {
                _context.Set<Rols>().Update(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el rol: {ex.Message}");
                return false;
            }
        }


        // Metodo para actualiza un rol con sentencia SQl
        public async Task<bool> UpdateAsyncSql(Rols rol)
        {
            try
            {
                string query = @"
                    UPDATE Rols 
                    SET Name = @Name
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    rol.Id,
                    rol.Name
                });

                return rowsAffected > 0;

                //_context.Set<Rol>().Update(rol);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el rol : {ex.Message}");
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
                var rol = await _context.Set<Rols>().FirstOrDefaultAsync(r => r.Id == id);
                if (rol == null)
                    return false;

                rol.IsDeleted = true; // Marcamos como eliminado
                _context.Set<Rols>().Update(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar eliminación lógica del rol con ID {RolId}", id);
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
                _logger.LogError($"Error al eliminar lógicamente el rol: {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Elimina un Rol de la base de datos SQL
        /// </summary>
        /// 

        // Metodo para elimina un rol con sentencia linQ
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rol = await _context.Set<Rols>().FindAsync(id);
                if (rol == null)
                    return false;
                _context.Set<Rols>().Remove(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar rol: {ex.Message}");
                return false;
            }
        }

        // Metodo para elimina un rol con sentencia Sentencia SQl 

        public async Task<bool> DeleteAsyncSql(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM Rols WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;

                //var rol = await _context.Set<Form>().FindAsync(id);
                //if (rol == null)
                //    return false;
                //_context.Set<Rol>().Remove(rol);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el rol {ex.Message}");
                return false;
            }
        }
    }
}
