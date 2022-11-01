using Udemy_RestWithASP_NET5.Data.VO;

namespace Udemy_RestWithASP_NET5.Business {
    public interface IPersonBusiness {
        PersonVO Create(PersonVO person);
        PersonVO FindByID(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        void Delete(long id);
    }
}
