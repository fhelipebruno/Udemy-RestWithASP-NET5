using Udemy_RestWithASP_NET5.Data.Converter.Contract.Implementations;
using Udemy_RestWithASP_NET5.Data.VO;
using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Repository;

namespace Udemy_RestWithASP_NET5.Business.Implementations {
    public class BookBusinessImplementation : IBookBusiness {
        
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository) {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll() {
            List<BookVO> books = new List<BookVO>();

            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindByID(long id) {
            return _converter.Parse(_repository.FindByID(id));
        }

        public BookVO Create(BookVO book) {
            var BookEntity = _converter.Parse(book);
            BookEntity = _repository.Create(BookEntity);
            return _converter.Parse(BookEntity);
        }
        public BookVO Update(BookVO book) {
            var BookEntity = _converter.Parse(book);
            BookEntity = _repository.Update(BookEntity);
            return _converter.Parse(BookEntity);
        }

        public void Delete(long id) {
            _repository.Delete(id);
        }
    }
}
