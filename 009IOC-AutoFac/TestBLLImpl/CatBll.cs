using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestIBLL;

namespace TestBLLImpl
{
    public class CatBll : IAnimalBll
    {
        public void Cry()
        {
            Console.WriteLine("喵喵喵！");
        }
    }
}