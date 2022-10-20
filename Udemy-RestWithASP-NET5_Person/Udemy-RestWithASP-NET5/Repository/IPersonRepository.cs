using Udemy_RestWithASP_NET5.Model;

namespace Udemy_RestWithASP_NET5.Repository {
    public interface IPersonRepository {
        Person Create(Person person);
        Person FindByID(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
        bool Exists(long id);
    }
}
