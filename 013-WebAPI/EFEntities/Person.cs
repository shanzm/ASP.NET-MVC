using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFEntities
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}