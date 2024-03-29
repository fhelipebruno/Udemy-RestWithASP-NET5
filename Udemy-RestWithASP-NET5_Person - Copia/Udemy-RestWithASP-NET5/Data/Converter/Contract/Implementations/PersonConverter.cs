﻿using Udemy_RestWithASP_NET5.Data.VO;
using Udemy_RestWithASP_NET5.Model;

namespace Udemy_RestWithASP_NET5.Data.Converter.Contract.Implementations {
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO> {
        public PersonVO Parse(Person origin) {
            if (origin == null) return null;

            return new PersonVO {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public Person Parse(PersonVO origin) {
            if (origin == null) return null;

            return new Person {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<PersonVO> Parse(List<Person> origin) {
            if (origin == null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }

        public List<Person> Parse(List<PersonVO> origin) {
            if (origin == null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
