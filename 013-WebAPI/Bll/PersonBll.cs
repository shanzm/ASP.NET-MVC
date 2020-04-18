using Dal;
using Dto;
using EFEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//添加对Dal层的引用
//添加对Dto层的引用

namespace Bll
{
    public class PersonBll
    {
        PersonDal personDal = new PersonDal();
        public PersonDto GetPerson(long id)
        {
            return personDal.GetPersonById(id);
        }

        public IEnumerable<PersonDto> GetPersonAll()
        {
            return personDal.GetPersonAll();
        }

        public bool AddPerson(PersonDto pDto)
        {
            Person p = new Person() { Id = pDto.Id, Name = pDto.Name, Age = pDto.Age, CreateDateTime = DateTime.Now };
            if ( personDal.AddPerson(p)!=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
