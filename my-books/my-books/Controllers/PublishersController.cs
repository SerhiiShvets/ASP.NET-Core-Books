using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;
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

        [HttpGet("publisher/{id}")]
        public IActionResult GetPublisher(int id)
        {
            var _response = _publishersService.GetPublisher(id);
            if (_response != null)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var _publisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), _publisher);
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("publisher-data/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            try
            {
                var _response = _publishersService.GetPublisherData(id);
                return Ok(_response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("publisher/{id}")]
        public IActionResult DeletePublisher(int id)
        {
            try
            {
                _publishersService.DeletePublisher(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
