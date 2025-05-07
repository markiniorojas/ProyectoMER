using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class FormData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FormData> _logger;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Intancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos.</param>

        public FormData(ApplicationDbContext context, ILogger<FormData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los formularios almacenados en la base de datos con Sql
        /// </summary>
        public async Task<IEnumerable<Forms>> GetAllAsyncSQL()
        {
            string query = @"SELECT * FROM Forms";
            return (IEnumerable<Forms>)await _context.QueryAsync<IEnumerable<Forms>>(query);

        }

        /// <returns>Lista de formularios</returns>
        public async Task<IEnumerable<Forms>> GetAllAsync()
        {
            return await _context.Set<Forms>().ToListAsync();
        }

        /// <summary>
        /// Obtiene un formulario especifico por su identificador cin linQ
        /// </summary>
        public async Task<Forms?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Forms>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el formulario con ID {FormId}", id);
                throw; //Re-lanza la excepcion para sea manejada en capas superiores
            }
        }

        /// <summary>
        /// Obtiene un FormData especifico por su identificacion SQL
        /// </summary
        public async Task<Forms?> GetByIdAsyncSQL(int id)
        {
            try
            {
                string query = @"SELECT * FROM Forms WHERE Id = @Id";
                return await _context.QueryFirstOrDefaultAsync<Forms>(query, new { Id = id });

                //return await _context.Set<Form>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el formulario con ID {FormId}", id);
                throw;
            }

        }
        /// <summary>
        /// Crea un nuevo formulario en la base de datos
        /// </summary>
        /// <param name="form"></param>
        /// <returns>el formulario creado.</returns>
        /// 


        //Metodo con LinQ para crear los formularios con linQ
        public async Task<Forms> CreateAsync(Forms form)
        {
            try
            {
                await _context.Set<Forms>().AddAsync(form);
                await _context.SaveChangesAsync();
                return form;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el formulario: {ex.Message}");
                throw;
            }
        }

        // Crear un nuevo FormData en la base de datos SQL

        public async Task<Forms> CreateAsyncSQL(Forms form)
        {
            try
            {
                string query = @"
                    INSERT INTO Forms (Name, Description,) 
                    VALUES (@Name, @Description);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                int newId = await _context.QuerySingleAsync<int>(query, new
                {
                    form.Name,
                    form.Description
                });
                return form;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el formulario: {ex.Message}");
                throw;
            }
        }



        /// <summary>
        /// Actualizar un FormData existente en la base de datos 
        /// </summary>
        /// 

        //Metodo para actualizar formularios con LinQ 
        public async Task<bool> UpdateAsync(Forms form)
        {
            try
            {
                _context.Set<Forms>().Update(form);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el formulario: {ex.Message}");
                return false;
            }
        }

        //Metodo para actualizar formularios con SQL
        public async Task<bool> UpdateAsyncSQL(Forms form)
        {
            try
            {
                string query = @"
                    UPDATE Forms 
                    SET Name = @Name, Description = @Description
                    WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new
                {
                    form.Id,
                    form.Name,
                    form.Description
                });

                return rowsAffected > 0;

                //_context.Set<Form>().Update(form);
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
                var form = await _context.Set<Forms>().FirstOrDefaultAsync(f => f.Id == id);
                if (form == null)
                    return false;

                form.IsDeleted = true; // Marcamos como eliminado
                _context.Set<Forms>().Update(form);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar eliminación lógica del formulario con ID {FormId}", id);
                return false;
            }
        }


        // Metodo de eliminador logico en sentencia SQL:
        public async Task<bool> DeleteLogicAsyncSql(int id)
        {
            try
            {
                string query = @"
            UPDATE Forms SET IsDeleted = 1 WHERE Id = @Id;
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
        /// Elimina un FormData de la base de datos 
        /// </summary>
        /// 


        //Metodo para eliminar formularios con linQ
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var form = await _context.Set<Forms>().FindAsync(id);
                if (form == null)
                    return false;
                _context.Set<Forms>().Remove(form);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar formulario: {ex.Message}");
                return false;
            }
        }


        //Metodo para eliminar formularios con SQL
        public async Task<bool> DeleteAsyncSQL(int id)
        {
            try
            {
                string query = @"
                    DELETE FROM Forms WHERE Id = @Id;
                    SELECT CAST(@@ROWCOUNT AS int);";

                int rowsAffected = await _context.QuerySingleAsync<int>(query, new { Id = id });

                return rowsAffected > 0;

                //var form = await _context.Set<Form>().FindAsync(id);
                //if (form == null)
                //    return false;
                //_context.Set<Form>().Remove(form);
                //await _context.SaveChangesAsync();
                //return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el formulario {ex.Message}");
                return false;
            }
        }
    }
}
