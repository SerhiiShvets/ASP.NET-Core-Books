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
    public class BooksController : ControllerBase
    {
        private BooksService _booksService;
        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("books")]
        public IActionResult GetBooks()
        {
            var allBooks = _booksService.GetBooks();
            return Ok(allBooks);
        }

        [HttpPost("book")]
        public IActionResult AddBook([FromBody]BookVM book)
        {
            _booksService.AddBook(book);
            return Ok();
        }

        [HttpGet("book/{id}")]
        public IActionResult GetBook(int id)
        {
            try
            {
                var book = _booksService.GetBook(id);
                return Ok(book);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("book/{id}")]
        public IActionResult UpdateBook(int id, [FromBody]BookVM book)
        {
            var updatedBook = _booksService.UpdateBook(id, book);
            return Ok(updatedBook);
        }

        [HttpDelete("book/{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _booksService.DeleteBook(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
