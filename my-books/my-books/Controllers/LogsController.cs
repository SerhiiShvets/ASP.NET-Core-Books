using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private LogsService _logsService;
        public LogsController(LogsService logsService)
        {
            _logsService = logsService;
        }

        [HttpGet("logs")]
        public IActionResult GetLogs()
        {
            try
            {
                var logs = _logsService.GetLogs();
                return Ok(logs);
            }
            catch (Exception)
            {

                return BadRequest("Could not retrive logs from DB");
            }
        }
    }
}
