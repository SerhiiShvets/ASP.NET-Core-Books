using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };
            _context.Books.Add(_book);
            _context.SaveChanges();
        }

        public List<Book> GetBooks() => _context.Books.ToList();

        public Book GetBook(int bookId) => _context.Books.FirstOrDefault(b => b.Id == bookId);

        public Book UpdateBook(int bookId, BookVM book)
        {
            var bookToUpdate = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if (bookToUpdate != null)
            {
                bookToUpdate.Title = book.Title;
                bookToUpdate.Description = book.Description;
                bookToUpdate.IsRead = book.IsRead;
                bookToUpdate.DateRead = book.IsRead ? book.DateRead.Value : null;
                bookToUpdate.Rate = book.IsRead ? book.Rate.Value : null;
                bookToUpdate.Genre = book.Genre;
                bookToUpdate.CoverUrl = book.CoverUrl;
                bookToUpdate.DateAdded = DateTime.Now;

                _context.SaveChanges();
            }

            return bookToUpdate;
        }

        public void DeleteBook(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
