using Udemy_RestWithASP_NET5.Model;

namespace Udemy_RestWithASP_NET5.Business {
    public interface IBookBusiness {
        Book Create(Book book);
        Book FindByID(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
    }
}
