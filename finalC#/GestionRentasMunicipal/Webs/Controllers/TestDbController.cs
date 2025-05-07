//using Entity.Contexts;
//using Microsoft.AspNetCore.Mvc;

//namespace Webs.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TestDbController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public TestDBController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<IActionResult> TestConnection()
//        {
//            try
//            {
//                // Verifica si la conexión está abierta
//                bool canConnect = await _context.Database.CanConnectAsync();

//                if (canConnect)
//                {
//                    return Ok("Conexión exitosa a la base de datos");
//                }
//                else
//                {
//                    return BadRequest("No se pudo conectar a la base de datos");
//                }
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Error de conexión: {ex.Message}");
//            }
//        }
//    }
//}
