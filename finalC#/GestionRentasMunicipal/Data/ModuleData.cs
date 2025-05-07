using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    /// <summary>
    /// Repositorio encargado de la getion de la entidad module en la base de datos
    /// </summary>
    public class ModuleData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ModuleData> _logger;

        /// <summary>
        /// Constructor que recibe recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Intancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos.</param>
        /// 

        public ModuleData(ApplicationDbContext context, ILogger<ModuleData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los modulos almacenados en la base de datos 
        /// </summary>
        /// 

        // Obtiene todos los Modulos con sus modulo almacenados en la base de datos CON linQ  .

        public async Task<IEnumerable<Modules>> GetAllAsync()
        {
            return await _context.Set<Modules>().ToListAsync();
        }

        // Obtiene todos los Modulos con sus modulo almacenados en la base de datos CON SQL

        public async Task<IEnumerable<Modules>> GetAllAsyncSql()
        {
            string query = @"SELECT * FROM Modules";
            return (IEnumerable<Modules>)await _context.QueryAsync<IEnumerable<Modules>>(query);

            //return await _context.Set<Module>().ToListAsync();
        }



        /// <summary>
        /// Obtiene un Modulos con sus module especificos por su identificador con LINQ
        /// </summary>
        public async Task<Modules?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Modules>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Modulo con ID {ModuleId}", id);
                throw; //Re-lanza la excepcion para sea manejada en capas superiores
            }
        }

        /// <summary>
        /// Obtiene un modulo especifico por su identificacion SQL
        /// </summary
        public async Task<Modules?> GetByIdAsyncSql(int id)
        {
            try
            {
                string query = @"SELECT * FROM Modules WHERE Id = @Id";
                return await _context.QueryFirstOrDefaultAsync<Modules>(query, new { Id = id });

                //return await _context.Set<Module>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el formulario con ID {FormId}", id);
                throw;
            }

        }

        

        /// <summary>
        /// Crea un nuevo Modulos con sus modulos en la base de datos con LINQ
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns>el Modulo fue creado.</returns>
        /// 

        //Metodo con LinQ para crear los modulos con linQ
        public async Task<Modules> CreateAsync(Modules module)
        {
            try
            {
                await _context.Set<Modules>().AddAsync(module);
                await _context.SaveChangesAsync();
                return module;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el Modulo: {ex.Message}");
                throw;
            }
        }

        // Crear un nuevo moduleData en la base de datos SQL

        public async Task<Modules> CreateAsyncSql(Modules module)
        {
            try
            {
                string query = @"
                    INSERT INTO Modules (Name, Description, Code) 
                    VALUES (@Name, @Description, @Code);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    module.Name,
                    module.Description,
                    module.Code
                });

                module.Id = newId;
                return module;

                //await _context.Set<module>().AddAsync(module);
                //await _context.SaveChangesAsync();
                //return module;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el modulo: {ex.Message}");
                throw;
            }
        }



        /// <summary>
        /// Actualiza un Modulos con sus formularios existente en la base de datos con linQ
        /// </summary>
        /// <param name="moduleForm">Objeto con la informacion actualizada</param>
        /// <returns>True si la operacion fue exitosa, False en caso contrario</returns>
        /// 

        // Actualiza un MOdulo existente en la base de datos con linQ

        public async Task<bool> UpdateAsync(Modules module)
        {
            try
            {
                _context.Set<Modules>().Update(module);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el modulo con sus permisos: {ex.Message}");
                return false;
            }
        }

        // Actualiza un MOdulo existente en la base de datos SQL

        public async Task<bool> UpdateAsyncSQL(Modules module)
        {
            try
            {
                string query = @"
                    UPDATE Modules 
                    SET Name = @Name, Description = @Description, Code = @Code
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    module.Id,
                    module.Name,
                    module.Description,
                    module.Code
                });

                return rowsAffected > 0;

                //_context.Set<Module>().Update(module);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el formulario : {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Realiza una eliminación lógica del modulo con el ID especificado.
        /// </summary>
        /// <param name="id">ID del modulo a eliminar lógicamente.</param>
        /// <returns>True si se marcó como eliminado, False si no se encontró.</returns>
        /// 

        // Metodo de eliminador logico en linQ
        public async Task<bool> DeleteLogicAsync(int id)
        {
            try
            {
                var module = await _context.Set<Modules>().FirstOrDefaultAsync(r => r.Id == id);
                if (module == null)
                    return false;

                module.IsDeleted = true; // Marcamos como eliminado
                _context.Set<Modules>().Update(module);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar eliminación lógica del modulo con ID {ModuleId}", id);
                return false;
            }
        }


        // Metodo de eliminador logico en sentencia SQL:
        public async Task<bool> DeleteLogicAsyncSql(int id)
        {
            try
            {
                string query = @"
            UPDATE MOdules SET IsDeleted = 1 WHERE Id = @Id;
            SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente el module: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Realiza una eliminación  del Modulo con el ID especificado.
        /// </summary>
        /// <param name="id">ID del Modulo a eliminar .</param>
        /// <returns>True si se marcó como eliminado, False si no se encontró.</returns>
        /// 

        //Metodo con LinQ para eliminar los modulos
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var module = await _context.Set<Modules>().FindAsync(id);
                if (module == null)
                    return false;

                _context.Set<Modules>().Remove(module);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el modulo{ex.Message}");
                return false;
            }
        }

  
        // Elimina un ModuleData de la base de datos SQL
 
        public async Task<bool> DeleteAsyncSql(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM Modules WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;

                //var module = await _context.Set<Module>().FindAsync(id);
                //if (module == null)
                //    return false;
                //_context.Set<Module>().Remove(module);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el modulo {ex.Message}");
                return false;
            }
        }
    }
}
