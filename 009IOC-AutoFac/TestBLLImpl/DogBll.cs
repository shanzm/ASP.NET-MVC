using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestIBLL;

namespace TestBLLImpl
{
    public class DogBll : IAnimalBll
    {
        public void Cry()
        {
            Console.WriteLine("汪汪汪！");
        }
    }
}