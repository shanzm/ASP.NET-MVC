using MyIOC.AttributeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyIOC.IOC
{
    /// <summary>
    /// IOC 工厂 (容器) 
    /// 以Dictionary类作为对象的容器
    /// </summary>
    class IOCFactory  
    {

        // IOC容器（创建的对象的容器）
        // string key:对象类型名
        // object value:对象实例
        private Dictionary<string, object> iocDictionaries = new Dictionary<string, object>();


        // IOC中对象类型的容器
        // string key：类型名
        // Type value：类型
        private Dictionary<string, Type> iocTypeDictionaries = new Dictionary<string, Type>();


        //加载程序集,将含有我们自定义的特性标签的类的类型存储到类型容器中
        public void LoadAssmaly(string asmName)
        {
            Assembly assembly = Assembly.Load(asmName);

            Type[] types = assembly.GetTypes();//注意这里获取的是程序集中的所有定义的类型

            // 筛选出含有IOcServiceAttribute特性标签的类，存储其type类型
            foreach (Type type in types)
            {
                IOCServiceAttribute iOCService = type.GetCustomAttribute(typeof(IOCServiceAttribute)) as IOCServiceAttribute;//获取类上的自定义的特性标签
                if (iOCService != null)//如果是IOCServiceAttribute标注类，则把其类型存入类型容器中
                {
                    iocTypeDictionaries.Add(type.Name, type);//最终其中的数据：{[Student, MyIOC.ClassLib.Student],[Teacher, MyIOC.ClassLib.Teacher]}
                }
            }

        }

        /// <summary>
        /// ioc容器对象创建
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public object GetObject(string typeName)
        {
            //根据参数取出指定的type
            Type type = iocTypeDictionaries[typeName];

            //创建type类型的对象
            object objectValue = Activator.CreateInstance(type);

            //获取type类型对象的所有属性
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //获取类中属性上的自定义IOCInjectAttribute特性标签
                IOCInjectAttribute iOCInject = (IOCInjectAttribute)propertyInfo.GetCustomAttribute(typeof(IOCInjectAttribute));
                //如果该属性是含有IOCInjectAttribute类型的特性，则为其也创建一个指定的实例（即注入依赖对象）
                if (iOCInject != null)
                {
                    //为objectValue的propertyInfo属性赋值
                    //这里使用了递归的方式创建一个指定类型的实例
                    propertyInfo.SetValue(objectValue, GetObject(propertyInfo.PropertyType.Name));
                }
            }

            //将创建的对象存储到容器中
            iocDictionaries.Add(typeName, objectValue);

            return objectValue;
        }


    }
}
