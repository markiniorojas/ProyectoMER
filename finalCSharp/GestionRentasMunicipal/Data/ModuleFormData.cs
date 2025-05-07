using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    /// <summary>
    /// Repositorio encargado de la getion de la entidad RolFormPermission en la base de datos
    /// </summary>
    public class ModuleFormData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ModuleFormData> _logger;

        /// <summary>
        /// Constructor que recibe recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Intancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos.</param>

        public ModuleFormData(ApplicationDbContext context, ILogger<ModuleFormData> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Obtiene todos los Modulos con sus formularios almacenados en la base de datos.
        /// </summary>
        /// 
        public async Task<IEnumerable<ModuleForms>> GetAllAsyncSql()
        {
            string query = @"SELECT * FROM ModuleForms";
            return (IEnumerable<ModuleForms>)await _context.QueryAsync<IEnumerable<ModuleForms>>(query);

            //return await _context.Set<Module>().ToListAsync();
        }


        /// <returns>Lista de los Modulos con sus formularios</returns>
        /// 

        public async Task<IEnumerable<ModuleForms>> GetAllAsync()
        {
            return await _context.Set<ModuleForms>().ToListAsync();
        }
        /// <summary>
        /// Obtiene un Modulos con sus formularios especificos por su identificador con linQ
        /// </summary>
        public async Task<ModuleForms?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<ModuleForms>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Modulo con sus fomularios con ID {ModuleFormId}", id);
                throw; //Re-lanza la excepcion para sea manejada en capas superiores
            }
        }

        /// <summary>
        /// Obtiene un module rol especifico por su identificacion SQL
        /// </summary
        public async Task<ModuleForms?> GetByIdAsyncSql(int id)
        {
            try
            {
                string query = @"SELECT * FROM ModuleForms WHERE Id = @Id";
                return await _context.QueryFirstOrDefaultAsync<ModuleForms>(query, new { Id = id });

                //return await _context.Set<ModuleForms>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el module form user con ID {RolUserId}", id);
                throw;
            }

        }

        /// <summary>
        /// Crea un nuevo Modulos con sus formularios en la base de datos
        /// </summary>
        /// <param name="moduleForm"></param>
        /// <returns>el Modulos con sus formularios fue creado.</returns>
        /// 

        //Metodo de crear modulos con sus formulas con linQ
        public async Task<ModuleForms> CreateAsync(ModuleForms moduleForm)
        {
            try
            {
                await _context.Set<ModuleForms>().AddAsync(moduleForm);
                await _context.SaveChangesAsync();
                return moduleForm;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el Modulo con sus formularios: {ex.Message}");
                throw;
            }
        }

        //Metodo para crear un nuevo rol user con sentencia SQl

        public async Task<ModuleForms> CreateAsyncSql(ModuleForms moduleForms)
        {
            try
            {
                string query = @"
                    INSERT INTO ModuleForms (ModuleId, FormId) 
                    OUTPUT INSERTED.Id
                    VALUES (@ModuleId, @FormId);
                    ";

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    moduleForms.Id,
                    moduleForms.ModuleId,
                    moduleForms.FormId
                });
                return moduleForms;

                //await _context.Set<moduleForms>().AddAsync(moduleForms);
                //await _context.SaveChangesAsync();
                //return module;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el module form: {ex.Message}");
                throw;
            }
        }


        //Metodo de actualizar modulos con sus formularios con linQ


        public async Task<bool> UpdateAsync(ModuleForms moduleForm)
        {
            try
            {
                _context.Set<ModuleForms>().Update(moduleForm);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el modulo con sus formularios: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Actualiza un Modulos con sus formularios existente en la base de datos
        /// </summary>
        /// <param name="moduleForm">Objeto con la informacion actualizada</param>
        /// <returns>True si la operacion fue exitosa, False en caso contrario</returns>
        /// 


        public async Task<bool> UpdateAsyncSQL(ModuleForms moduleForms)
        {
            try
            {
                string query = @"
                    UPDATE ModuleForms
                    SET RolId = @ModuelId, FormId= @FormId
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    moduleForms.Id,
                    moduleForms.ModuleId,
                    moduleForms.FormId,
                });

                return rowsAffected > 0;

                //_context.Set<moduleForms>().Update(moduleForms);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el module form user : {ex.Message}");
                return false;
            }
        }



        //Metodo para eliminar un modulo form con sus formularios en linQ
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var moduleForm = await _context.Set<ModuleForms>().FindAsync(id);
                if (moduleForm == null)
                    return false;

                _context.Set<ModuleForms>().Remove(moduleForm);
                await _context.SaveChangesAsync();
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el modulo con sus formularios {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un ModuleForm de la base de datos SQL
        /// </summary>
        public async Task<bool> DeleteAsyncSql(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM ModuleForms WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el modulo con sus formularios {ex.Message}");
                return false;
            }
        }
    }
}
