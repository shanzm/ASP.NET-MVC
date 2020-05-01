using DemoDal;
using DemoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBll
{
    public class UserBll
    {

        UserDal userDal = new UserDal();

        //验证
        public bool Login(string name, string password)
        {
            return userDal.GetByNameAndPassworde(name, password);
        }

        //根据Id查询
        public UserDTO GetById(long id)
        {
            return userDal.GetById(id);
        }

        //删除数据
        public bool Delete(long id)
        {
            return userDal.MarkDeleted(id);
        }

        //查询所有
        public IEnumerable<UserDTO> GetAllUser()
        {
            return userDal.GetAllUser();
        }

        //添加
        public bool AddUser(UserDTO userDTO)
        {
            return userDal.AddUser(userDTO);
        }

        //修改
        public bool EditUser(UserDTO userDTO)
        {
            return userDal.EditUser(userDTO);
        }

    }
}
