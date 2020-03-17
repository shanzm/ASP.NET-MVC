using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIOC.AttributeLib
{
    /// <summary>
    /// IOC容器类特性
    /// 标记了IOCServiceAttribute特性的类，被注册到容器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]//表示该自定义的属性只能用于类之上
    class IOCServiceAttribute : Attribute
    {
        public IOCServiceAttribute()
        {

        }
    }
}
