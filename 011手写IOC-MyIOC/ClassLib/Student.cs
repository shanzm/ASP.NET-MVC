using MyIOC.AttributeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIOC.ClassLib
{
    [IOCService]
    class Student
    {

        [IOCInject]
        public Teacher Teacher { set; get; }


        public void Study()
        {
            
            Teacher.Teach();

            Console.WriteLine($"学生：学习中……");
        }
    }
}
