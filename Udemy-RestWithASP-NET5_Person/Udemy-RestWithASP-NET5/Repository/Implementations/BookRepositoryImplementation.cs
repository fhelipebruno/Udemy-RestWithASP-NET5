﻿using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Model.Context;

namespace Udemy_RestWithASP_NET5.Repository.Implementations {
    public class BookRepositoryImplementation : IBookRepository {

        private MySQLContext _context;

        public BookRepositoryImplementation(MySQLContext context) {
            _context = context;
        }

        public List<Book> FindAll() {
            List<Book> books = new List<Book>();

            return _context.Books.ToList();
        }

        public Book FindByID(long id) {
            return _context.Books.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Book Create(Book books) {
            try {
                _context.Add(books);
                _context.SaveChanges();
            }
            catch (Exception) {

                throw;
            }
            return books;
        }
        public Book Update(Book book) {

            if (!Exists(book.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(book.Id));

            if (result != null) {
                try {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception) {

                    throw;
                }
            }

            return book;
        }

        public void Delete(long id) {

            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null) {
                try {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception) {

                    throw;
                }
            }
        }

        public bool Exists(long id) {
            return _context.Books.Any(p => p.Id.Equals(id));
        }
    }
}