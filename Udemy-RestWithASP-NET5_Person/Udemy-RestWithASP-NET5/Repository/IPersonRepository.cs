using Udemy_RestWithASP_NET5.Data.Converter.VO;
using Udemy_RestWithASP_NET5.Model;

namespace Udemy_RestWithASP_NET5.Repository {
    public interface IPersonRepository : IRepository<Person>{
        Person Disable(long id);
        List<Person> FindByName(string firstName, string secondName);
        int GetCount(object countQuery);
    }
}
