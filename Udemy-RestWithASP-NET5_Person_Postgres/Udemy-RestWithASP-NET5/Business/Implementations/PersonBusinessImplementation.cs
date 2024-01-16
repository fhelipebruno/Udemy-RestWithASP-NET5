using Udemy_RestWithASP_NET5.Data.Converter.Contract.Implementations;
using Udemy_RestWithASP_NET5.Data.VO;
using Udemy_RestWithASP_NET5.Hypermedia.Utils;
using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Repository;

namespace Udemy_RestWithASP_NET5.Business.Implementations {
    public class PersonBusinessImplementation : IPersonBusiness {
        
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository) {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll() {
            List<PersonVO> persons = new List<PersonVO>();

            return _converter.Parse(_repository.FindAll());
        }
        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var offset = page > 0 ? (page - 1) * pageSize : 0;
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
            var size = (pageSize < 1) ? 1 : pageSize;

            string query = @"select *
                             from person p
                             where 1 = 1 
                            ";

            if (!string.IsNullOrWhiteSpace(name))
                query += $"and p.first_name like '%{name}%'";

            query += $"order by p.first_name {sort} limit {size} offset {offset}";

            string countQuery = @"select count(1)
                                  from person p
                                  where 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(name))
                countQuery += $"and p.first_name like '%{name}%'";

            var persons = _repository.FindWithPagedSearch(query);
            
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = offset,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public PersonVO FindByID(long id) {
            return _converter.Parse(_repository.FindByID(id));
        }
        public List<PersonVO> FindByName(string firstName, string secondName)
        {
            return _converter.Parse(_repository.FindByName(firstName, secondName));
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

        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id) {
            _repository.Delete(id);
        }        
    }
}
