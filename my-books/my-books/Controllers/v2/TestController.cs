using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers.v2
{
    [ApiVersion("2.0")]
    //[Route("api/[controller]")]
    [Route("api/v{version:ApiVersion}[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("test-data2")]
        public IActionResult Get()
        {
            return Ok("This is a test controller V2");
        }
    }
}
