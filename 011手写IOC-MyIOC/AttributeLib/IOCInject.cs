using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIOC.AttributeLib
{
    /// <summary>
    /// IOC依赖注入特性
    /// 标明IOCInjectAttribute特性的属性，被注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]//表示该自定义的属性只能用于类之上
    class IOCInjectAttribute : Attribute
    {
        public IOCInjectAttribute()
        {

        }
    }
}
