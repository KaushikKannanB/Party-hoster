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
        var canConnect = _context.Database.CanConnect();
        return Ok(new
        {
            message = "✅ Database connection check completed.",
            canConnect
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new
        {
            message = "❌ Database connection failed.",
            error = ex.Message,
            inner = ex.InnerException?.Message
        });
    }
}

    }
}
