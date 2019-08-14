using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _02DropDownList.Common
{
    //继承于ValidationAttribute，实现
    public class CHPhoneNumber : ValidationAttribute
    {
        //通过构造函数给ErrorMessage属性赋值，设置默认的错误消息
        public CHPhoneNumber()
        {
            this.ErrorMessage = "手机号必须是以13,17,18开头，固号以3到4为位的区号开头加“-”";
        }

        public override bool IsValid(object value)//重写继承类中的IsValid()
        {
            if (value is string)
            {
                string s = (string)value;
                if (s.Length == 11)//手机号
                {
                    if (s.StartsWith("13") || s.StartsWith("17") || s.StartsWith("18"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                else if (s.Contains("-"))//固号
                {
                    string[] strArray = s.Split('-');
                    if ((strArray[0].Length == 3) || (strArray[0].Length == 4))//区号只能是3位或4位
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                else
                {
                    return false;
                }


            }
            else
            {
                return false;
            }
        }
    }
}