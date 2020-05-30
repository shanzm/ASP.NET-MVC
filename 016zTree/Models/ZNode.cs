using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _016zTree.Models
{
    /// <summary>
    /// 树节点，这个类中的所有属性就是下列的key,当然还可以有许多，可以参考文档
    /// {
    ///   name: "浙江",
    ///   open: false, 
    ///   children:[],
    ///   url:"https://www.baidu.com"
    /// }
    /// 其实这里是有一点要注意的，一般的前端中所有的变量都是小写开头的，而在C#中变量、属性等都是大驼峰命名
    /// 而使用C#中的Json()函数进行序列化，则序列化后的Json字符串中的key依旧是保留C#中的变量格式，即驼峰命名，
    /// 我们自己在前端中反序列化可以注意，定义的变量大写，既可以读取Json，但是前端框架的中一般都是无法识别的
    /// 所以毫无疑问的，在实际项目中一定是使用NewtonSoft.Json
    /// </summary>
    public class ZNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pid { get; set; }
        public bool IsParent { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }//显示的节点图标
        public bool Open { get; set; }
    }
}