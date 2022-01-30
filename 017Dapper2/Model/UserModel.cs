using Dapper.Contrib.Extensions;
using System;

#region 说明
//Table：指定实体对应地数据库表名，可忽略，但是忽略后实体对应地数据库表名会在末尾加个s，Demo对应Demos

//Key：指定此列为主键（自动增长主键），可忽略，忽略后默认查找

//ExplicitKey：指定此列为主键（不自动增长类型例如guid，ExplicitKey与Key地区别下面会详细讲）

//Computed：计算属性，打上此标签，对象地insert，update等操作会忽略此列

//Write：需穿一个bool值，false时insert，update等操作会忽略此列

//Key和ExplicitKey这两项都是指定列为主键的。
//    区别是打上Key特性的列在插入时是不能指定值的，只能是数据库自动增长列，而ExplicitKey特性可以允许在插入时指定值，比如用guid为主键类型，则主键在插入时必须已经生成好。
#endregion


namespace _017Dapper2
{
    [Table("[User]")]//注意这里映射到的表User表的表名是关键字，需要使用方括号转义
    public class UserModel
    {
        [Key]//不是自动增长主键时使用ExplicitKey
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        [Computed]
        public string GenderCH => Gender ? "男" : "女";
        [Write(false)]
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
