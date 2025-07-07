using my_books.Data.Models;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher GetPublisher(int id) => _context.Publishers.FirstOrDefault(p => p.Id == id);

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartsWithNumber(publisher.Name))
            {
                throw new PublisherNameException("Name starts with a number", publisher.Name);
            }
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int id)
        {
            var _publisherData = _context.Publishers.Where(p => p.Id == id)
                .Select(p => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = p.Name,
                    BookAuthors = p.Books.Select(b => new BookAuthorVM()
                    {
                        BookName = b.Title,
                        BookAuthors = b.Book_Authors.Select(ba => ba.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();
            if (_publisherData != null)
            {
                return _publisherData;
            }
            else
            {
                throw new Exception($"The publisher with id {id} does not exist.");
            }
        }

        public void DeletePublisher(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(p => p.Id == id);

            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
            }
            else
            {
                throw new Exception($"The publisher with id {id} does not exist.");
            }
            _context.SaveChanges();
        }

        private bool StringStartsWithNumber(string name) => Regex.IsMatch(name, @"^d");
    }
}
