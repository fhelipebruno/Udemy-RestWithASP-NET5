using Udemy_RestWithASP_NET5.Data.VO;

namespace Udemy_RestWithASP_NET5.Business {
    public interface IBookBusiness {
        BookVO Create(BookVO book);
        BookVO FindByID(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
