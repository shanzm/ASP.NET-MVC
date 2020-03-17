using MyIOC.IOC;
using MyIOC.AttributeLib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyIOC.ClassLib;

namespace MyIOC
{
    class Program
    {
        static void Main(string[] args)
        {
            IOCFactory iOCFactory = new IOCFactory();

            iOCFactory.LoadAssmaly("MyIOC");

            // 1、创建老师对象
            // Teacher teacher = (Teacher)iOCFactory.GetObject("Teacher");

            // 2、创建学生对象
            Student student = (Student)iOCFactory.GetObject("Student");
            //student.Teacher = teacher;//IOCFactory实现了属性的注入


            //Undone：没有实现的功能
            //1.实例生命周期的问题，实例作用域的问题
            //2.循环依赖的问他：Student类中有Teacher属性，若是Teacher类中有Student类属性，则会因为递归实现属性的注入而造成内存移除

            student.Study();


            Console.ReadKey();
        }
    }
}
