using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _005Html辅助类.Models
{
    public class Person
    {
        [DisplayName("用户名")]
        [Required]
        [StringLength(8)]
        public string UserName { get; set; }

        [DisplayName("密码")]
        [Required]
        [StringLength(8)]
        public string Password { get; set; }
    }
}