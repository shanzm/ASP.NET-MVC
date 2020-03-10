using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TestIBLL项目是一个类库项目
//该项目中定义每个实体类所需要的接口


namespace TestIBLL
{
    public interface IUserBll
    {
        //检查登录信息
        bool Login(string userName, string pwd);
        //添加新用户
        void AddNew(string userName, string pwd);
    }
}
