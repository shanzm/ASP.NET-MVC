using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        [MaxLength(6, ErrorMessage = "用户名不超过6位")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [MaxLength(6, ErrorMessage = "密码不超过8位")]
        public string Password { get; set; }
    }
}