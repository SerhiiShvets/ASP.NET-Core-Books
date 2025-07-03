using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;
        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpPost("publisher")]
        public IActionResult AddAuthor([FromBody] PublisherVM publisher)
        {
            _publishersService.AddPublisher(publisher);
            return Ok();
        }

        [HttpGet("publisher-data/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpDelete("publisher/{id}")]
        public IActionResult DeletePublisher(int id)
        {
            _publishersService.DeletePublisher(id);
            return Ok();
        }
    }
}
