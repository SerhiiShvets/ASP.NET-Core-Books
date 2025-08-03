using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Route("api/[controller]")]
    [Route("api/v{version:ApiVersion}[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("test-data")]
        public IActionResult Get()
        {
            return Ok("This is a test controller V1");
        }
    }
}
