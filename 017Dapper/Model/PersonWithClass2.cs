using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _017Dapper.Model
{
    public class PersonWithClass2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public SchoolClass schoolClass { get; set; }
    }
}
