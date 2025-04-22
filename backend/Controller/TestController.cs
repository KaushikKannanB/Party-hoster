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
        Console.WriteLine("✅ DB can connect: " + canConnect);
        return Ok(new
        {
            message = "✅ Database connection check completed.",
            canConnect
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ DB Connection Exception: " + ex.Message);
        if (ex.InnerException != null)
            Console.WriteLine("❌ Inner: " + ex.InnerException.Message);

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
