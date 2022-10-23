﻿using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Repository;

namespace Udemy_RestWithASP_NET5.Business.Implementations {
    public class BookBusinessImplementation : IBookBusiness {
        
        private readonly IRepository<Book> _repository;

        public BookBusinessImplementation(IRepository<Book> repository) {
            _repository = repository;
        }

        public List<Book> FindAll() {
            List<Book> books = new List<Book>();

            return _repository.FindAll();
        }

        public Book FindByID(long id) {
            return _repository.FindByID(id);
        }

        public Book Create(Book book) {
            return _repository.Create(book);
        }
        public Book Update(Book book) {
            return _repository.Update(book);
        }

        public void Delete(long id) {
            _repository.Delete(id);
        }
    }
}
