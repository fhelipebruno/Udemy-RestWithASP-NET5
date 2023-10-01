using Udemy_RestWithASP_NET5.Data.VO;
using Udemy_RestWithASP_NET5.Hypermedia.Utils;
using Udemy_RestWithASP_NET5.Model;

namespace Udemy_RestWithASP_NET5.Business {
    public interface IPersonBusiness {
        PersonVO Create(PersonVO person);
        PersonVO FindByID(long id);
        List<PersonVO> FindByName(string firstName, string secondName);
        List<PersonVO> FindAll();
        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        PersonVO Update(PersonVO person);
        PersonVO Disable(long id);
        void Delete(long id);
    }
}
