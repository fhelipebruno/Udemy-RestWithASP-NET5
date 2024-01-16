using Microsoft.AspNetCore.Mvc;
using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Model.Context;
using Udemy_RestWithASP_NET5.Repository.Generic;

namespace Udemy_RestWithASP_NET5.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {

        public PersonRepository(PostgreSQLContext context) : base(context) { }
        public Person Disable(long id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id)))
                return null;

            var user = _context.Persons.SingleOrDefault(p => p.Id == id);

            if (user != null)
            {
                user.Enabled = false;

                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
                return user;
            }

            return user;
        }

        public List<Person> FindByName(string firstName, string secondName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(secondName))
            {
                return _context.Persons.Where(p =>  p.FirstName.Contains(firstName) && 
                                                    p.LastName.Contains(secondName)).ToList();

            }
            else if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(secondName))
            {
                return _context.Persons.Where(p => p.LastName.Contains(secondName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(secondName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToList();
            }
            return null;

        }
    }
}
