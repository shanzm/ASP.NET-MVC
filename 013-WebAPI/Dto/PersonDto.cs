using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    [Serializable]
    public class PersonDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
