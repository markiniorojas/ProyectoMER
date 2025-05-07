using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Data
{
    /// <summary>
    /// Repositorio encargado de la getion de la entidad RolFormPermission en la base de datos
    /// </summary>
    public class RolFormPermissionData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolFormPermissionData> _logger;

        /// <summary>
        /// Constructor que recibe recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Intancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos.</param>


        public RolFormPermissionData(ApplicationDbContext context, ILogger<RolFormPermissionData> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Obtiene todos los RolFormPermission con sus roles y permisos almacenados en la base de datos CON Sql.
        /// </summary>
        /// <returns>Lista de los formularios con sus roles y permisos</returns>
        /// 

        public async Task<IEnumerable<RolFormPermissions>> GetAllAsyncSql()
        {
            string query = @"SELECT * FROM RolFormPermissions";
            return (IEnumerable<RolFormPermissions>)await _context.QueryAsync<IEnumerable<RolFormPermissions>>(query);

            //return await _context.Set<Module>().ToListAsync();
        }

        /// <summary>
        /// Obtiene todos los usuarios almacenados en la base de datos.
        /// </summary>
        /// <returns>Lista de Rol con los usuarios</returns>
        /// 

        public async Task<IEnumerable<RolFormPermissions>> GetAllAsync()
        {
            return await _context.Set<RolFormPermissions>().ToListAsync();
        }

        /// <summary>
        /// Obtiene un formulario con sus roles y permisos especifico por su identificador con Linq
        /// </summary>

        public async Task<RolFormPermissions?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<RolFormPermissions>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el permiso del rol con ID {RolFormPermissionId}", id);
                throw; //Re-lanza la excepcion para sea manejada en capas superiores
            }
        }

        /// <summary>
        /// Obtiene un FormData especifico por su identificacion SQL
        /// </summary
        public async Task<RolFormPermissions?> GetByIdAsyncSql(int id)
        {
            try
            {
                string query = @"SELECT * FROM RolFormPermissions WHERE Id = @Id";
                return await _context.QueryFirstOrDefaultAsync<RolFormPermissions>(query, new { Id = id });

                //return await _context.Set<RolFormPermissions>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rolformpermission con ID {RolFormPermissionId}", id);
                throw;
            }

        }

        /// <summary>
        /// Crea un nuevo formulario con sus roles y permisos en la base de datos
        /// </summary>
        /// <param name="rolFormPermission"></param>
        /// <returns>el formulario con su rol y permiso fue creado.</returns>
        /// 

        //Metodo para crear un Rol Form Permission con linQ
        public async Task<RolFormPermissions> CreateAsync(RolFormPermissions rolFormPermission)
        {
            try
            {
                await _context.Set<RolFormPermissions>().AddAsync(rolFormPermission);
                await _context.SaveChangesAsync();
                return rolFormPermission;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el rolformPermision: {ex.Message}");
                throw;
            }
        }



        //Metodo para crear un nuevo rol user con sentencia SQl

        public async Task<RolFormPermissions> CreateAsyncSql(RolFormPermissions rolFormPermissions)
        {
            try
            {
                string query = @"
                    INSERT INTO RolFormPermissions (RolId, FormId, PermissionId) 
                    OUTPUT INSERTED.Id
                    VALUES (@RolId, @FormId, @PermissionId);
                    ";

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    rolFormPermissions.Id,
                    rolFormPermissions.RolId,
                    rolFormPermissions.FormId,
                    rolFormPermissions.PermissionId
                });
                return rolFormPermissions;

                //await _context.Set<module>().AddAsync(module);
                //await _context.SaveChangesAsync();
                //return module;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el rol form permission: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza un formulario con su rol y permiso existente en la base de datos
        /// </summary>
        /// <param name="rolFormPermission">Objeto con la informacion actualizada</param>
        /// <returns>True si la operacion fue exitosa, False en caso contrario</returns>

        //Metodo para actualizar un Rol Form Permission con linQ
        public async Task<bool> UpdateAsync(RolFormPermissions rolFormPermission)
        {
            try
            {
                _context.Set<RolFormPermissions>().Update(rolFormPermission);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el rol con formulario y permiso del usuario: {ex.Message}");
                return false;
            }
        }

        //Metodo para actualizar un rol user con sentencias SQL

        public async Task<bool> UpdateAsyncSQL(RolFormPermissions rolFormPermission)
        {
            try
            {
                string query = @"
                    UPDATE RolFormPermissions 
                    SET RolId = @RolId, UserId= @UserId, PermissionId = @PermissionId
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    rolFormPermission.Id,
                    rolFormPermission.RolId,
                    rolFormPermission.FormId,
                    rolFormPermission.PermissionId
                });

                return rowsAffected > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el rol form permission: {ex.Message}");
                return false;
            }
        }


        //Metodo para eliminar un Rol Form Permission con linQ
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rolFormPermission = await _context.Set<RolFormPermissions>().FindAsync(id);
                if (rolFormPermission == null)
                    return false;

                _context.Set<RolFormPermissions>().Remove(rolFormPermission);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el formulario con su formulario y permiso: {ex.Message}");
                return false;
            }
        }

        //Metodo para eliminar un Rol Form Permission con sentencia SQl

        public async Task<bool> DeleteAsyncSql(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM RolFormPermissions WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el modulo {ex.Message}");
                return false;
            }

        }
    }
}
