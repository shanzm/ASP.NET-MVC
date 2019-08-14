using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _02DropDownList.Common
{
    //注意只要我们定义的属性类的名字是以Attribute结尾的那么我们在使用这个属性的时候是可以省略“Attribute”的
    //比如说，我们在使用下面我们定义的这个特性的实话，可以直接标注[QQNumber]

    //注意我们自定义的特性可以继承与指定类型的特性，这里我们设定的特性是为了匹配字符串是否复合QQ的格式
    //所以我们继承了RegularExpressAttribute，
    //Attribute的命名空间是：namespace System.ComponentModel.DataAnnotations
    public class QQNumberAttribute : RegularExpressionAttribute
    {
        public QQNumberAttribute() : base(@"^\d{5,11}$")
        {
            //注意这里我们给ErrorMessage的属性设置默认值，但是在我们使用这个特性的时候，可以重新赋值，覆盖掉默认值
            //如[QQNumber(ErrorMessage="qq号码格式不正确")]
            this.ErrorMessage = "字段{0}不是合法的QQ，正确QQ为5到10位数字";
        }
    }
}