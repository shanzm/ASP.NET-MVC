using MyIOC.AttributeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIOC.ClassLib
{
    [IOCService]
    class Teacher
    {
        //[IOCInject]
        //public Student _Student { set; get; }

        public void Teach()
        {
            Console.WriteLine($"老师：教学中……");
        }
    }
}
