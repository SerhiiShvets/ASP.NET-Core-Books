using Microsoft.AspNetCore.Mvc;
using my_books.ActionResults;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System;

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
        public CustomActionResult GetPublisher(int id)
        {
            var _response = _publishersService.GetPublisher(id);
            var _responseObject = new CustonActionResultVM();
            if(_response != null)
            {
                _responseObject.Publisher = _response;
                // return Ok(_response);
            }
            else
            {
                _responseObject.Exception = new Exception("This is coming from controller");
                // return NotFound();
            }

            return new CustomActionResult(_responseObject);
        }

        [HttpGet("publishers")]
        public IActionResult GetPublishers(string sortBy, string search, int pageNumber)
        {
            try
            {
                var _response = _publishersService.GetPublishers(sortBy, search, pageNumber);
                return Ok(_response);
            }
            catch (Exception ex)
            {

                return BadRequest("Sorry, we couldn`t load the publishers.");
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
