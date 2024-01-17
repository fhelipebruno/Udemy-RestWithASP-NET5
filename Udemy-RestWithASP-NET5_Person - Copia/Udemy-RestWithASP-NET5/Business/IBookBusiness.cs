using Udemy_RestWithASP_NET5.Data.VO;
using Udemy_RestWithASP_NET5.Hypermedia.Utils;

namespace Udemy_RestWithASP_NET5.Business {
    public interface IBookBusiness {
        BookVO Create(BookVO book);
        BookVO FindByID(long id);
        List<BookVO> FindAll();
        PagedSearchVO<BookVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
