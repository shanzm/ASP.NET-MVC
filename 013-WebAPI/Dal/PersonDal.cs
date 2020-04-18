using Dto;
using EFEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//DAL
//intall-package entityframework
//添加对EFEntities项目的引用
//添加对DTO项目的引用

namespace Dal
{
    public class PersonDal
    {
        public PersonDto GetPersonById(long id)
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                Person p = cxt.persons.SingleOrDefault(t => t.Id == id);
                return new PersonDto() { Id = p.Id, Name = p.Name, Age = p.Age, CreateDateTime = p.CreateDateTime };
            }
        }

        public IEnumerable<PersonDto> GetPersonAll()
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                List<Person> personList = cxt.persons.ToList();
                foreach (Person p in personList)
                {
                    yield return new PersonDto() { Id = p.Id, Name = p.Name, Age = p.Age, CreateDateTime = p.CreateDateTime };
                }
            }
        }

        public int AddPerson(Person p)
        {
            using (MyDbContext cxt = new MyDbContext())
            {
                cxt.persons.Add(p);
                return cxt.SaveChanges();
            }
        }
    }
}