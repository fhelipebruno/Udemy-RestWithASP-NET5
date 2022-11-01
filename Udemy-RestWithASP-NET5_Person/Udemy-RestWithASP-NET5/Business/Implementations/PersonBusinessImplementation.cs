using Udemy_RestWithASP_NET5.Data.Converter.Contract.Implementations;
using Udemy_RestWithASP_NET5.Data.VO;
using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Repository;

namespace Udemy_RestWithASP_NET5.Business.Implementations {
    public class PersonBusinessImplementation : IPersonBusiness {
        
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> repository) {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll() {
            List<PersonVO> persons = new List<PersonVO>();

            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindByID(long id) {
            return _converter.Parse(_repository.FindByID(id));
        }

        public PersonVO Create(PersonVO person) {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }
        public PersonVO Update(PersonVO person) {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id) {
            _repository.Delete(id);
        } 
    }
}
