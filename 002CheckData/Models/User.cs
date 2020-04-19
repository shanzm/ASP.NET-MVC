using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _002CheckData.Models
{
    public class User
    {
        [Required(ErrorMessage = "请输入用户名")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "用户名最长10最短5")]
        public string Name { get; set; }//必须赋值且长度最大是10,最小是5（注意一个属性可以有多个特性）

        [Required(ErrorMessage = "请输入密码")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "密码是5到10位")]
        public string Password { get; set; }
    }
}