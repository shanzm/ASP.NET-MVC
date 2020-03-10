using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestIBLL;

namespace TestBLLImpl
{
    public class DogBll : IDogBll
    {
        public void Bark()
        {
            Console.WriteLine("汪汪汪！");
        }
    }
}