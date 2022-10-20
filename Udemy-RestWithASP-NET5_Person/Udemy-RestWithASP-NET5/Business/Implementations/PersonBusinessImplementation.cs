using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Repository;

namespace Udemy_RestWithASP_NET5.Business.Implementations {
    public class PersonBusinessImplementation : IPersonBusiness {
        
        private readonly IPersonRepository _repository;

        public PersonBusinessImplementation(IPersonRepository repository) {
            _repository = repository;
        }

        public List<Person> FindAll() {
            List<Person> persons = new List<Person>();

            return _repository.FindAll();
        }

        public Person FindByID(long id) {
            return _repository.FindByID(id);
        }

        public Person Create(Person person) {
            return _repository.Create(person);
        }
        public Person Update(Person person) {
            return _repository.Update(person);
        }

        public void Delete(long id) {
            _repository.Delete(id);
        } 
    }
}
