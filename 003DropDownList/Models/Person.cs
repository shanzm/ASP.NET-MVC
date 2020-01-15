using _003DropDownList.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _003DropDownList.Models
{
    public class Person
    {
        [Required]
        public int Id { get; set; }//请求中使用Person类型Model，必须给Id赋值

        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string Name { get; set; }//必须赋值且长度最大是10,最小是5（注意一个属性可以有多个特性）

        [Range(1, 100, ErrorMessage = "年龄不对")]
        public int Age { get; set; }//年龄是1-100 

        [EmailAddress]
        public string Email { get; set; }//必须是邮件地址类型

        //注意所有的特性我们都是可以自定义错误信息的，自定义的ErrorMessage可以使用默认的占位符“｛0｝”，表示错误的属性名
        [EmailAddress(ErrorMessage = "{0}邮箱地址错误,请输入正确的邮箱地址")]
        public string Email1 { get; set; }

        //注意我们可以自定义使字符串复合指定的正则表达式
        [RegularExpression(@"^\w+@\w+\.\w+$")]
        public string Email2 { get; set; }

        //使用我们自定义的QQNumber特性
        [QQNumber]
        public string QQ { get; set; }

        //使用我们自定义的CHPhoneNumber特性
        //其实.net中自带[Phone]属性，但是只匹配美国的电话号码
        [CHPhoneNumber]
        public string Phone { get; set; }

    }
}