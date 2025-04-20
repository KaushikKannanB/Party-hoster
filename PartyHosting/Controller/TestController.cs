using Microsoft.AspNetCore.Mvc;
using PartyHosting.Data;

namespace PartyHosting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly Party_db_context _context;

        public TestController(Party_db_context context)
        {
            _context = context;
        }

        [HttpGet("check-db")]
        public IActionResult CheckDatabaseConnection()
        {
            try
            {
                // Try to connect
                _context.Database.CanConnect();
                return Ok("✅ Database connection successful!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Database connection failed: {ex.Message}");
            }
        }
    }
}
