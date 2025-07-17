using my_books.Data.Models;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };
            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach(var id in book.AuthorIds)
            {
                var book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.Books_Authors.Add(book_author);
                _context.SaveChanges();
            }
        }

        public List<Book> GetBooks() => _context.Books.ToList();
        public Book GetBook(int id)
        {
            var _book = _context.Books.FirstOrDefault(b => b.Id == id);
            if(_book != null)
            {
                return _book;
            }
            else
            {
                throw new Exception($"The book with id {id} does not exist.");
            }
        }
        public BookWithAuthorsVM GetBookWithAuthors(int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(b => b.Id == bookId).Select(b => new BookWithAuthorsVM()
            {
                Title = b.Title,
                Description = b.Description,
                IsRead = b.IsRead,
                DateRead = b.IsRead ? b.DateRead.Value : null,
                Rate = b.IsRead ? b.Rate.Value : null,
                Genre = b.Genre,
                CoverUrl = b.CoverUrl,
                PublisherName = b.Publisher.Name,
                AuthorNames = b.Book_Authors.Select(ba => ba.Author.FullName).ToList()
            }).FirstOrDefault();

            return _bookWithAuthors;
        }

        public Book UpdateBook(int bookId, BookVM book)
        {
            var bookToUpdate = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if(bookToUpdate != null)
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

        public void DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if(book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The bookth id {id} does not exist.");
            }
        }
    }
}
