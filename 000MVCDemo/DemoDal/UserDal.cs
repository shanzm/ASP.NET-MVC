using DemoDTO;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDal
{
    public class UserDal
    {
        MyDbContext cxt = new MyDbContext();

        //验证用户：
        public bool GetByNameAndPassworde(string name, string password)
        {

            User user = cxt.Set<User>().SingleOrDefault(e => e.Name == name && e.Password == password && e.IsDeleted != true);
            return user != null ? true : false;
        }


        /// <summary>
        /// 根据Id删除数据（软删除）
        /// </summary>
        /// <param name="id"></param>
        public bool MarkDeleted(long id)
        {

            //法1：
            //这里使用EntityState修改IsDeleted字段，必修同时把Name，Password值从前端传递来
            //更新数据时同时把Name，Password等在userConfig中要求非空的字段保存，否则SaveChanges()会报错“ EntityValidationErrors”,
            //所以我觉的还是直接通过Id查询处数据，remove()比较方便
            //User user = new User() { Id = id,Name =,Password = };
            //cxt.Entry(user).State = System.Data.Entity.EntityState.Unchanged;
            //user.IsDeleted = true;
            //cxt.Entry(user).State = System.Data.Entity.EntityState.Modified;

            //法2：
            User user = cxt.Set<User>().Where(u => u.Id == id).First();
            cxt.Users.Remove(user);

            return cxt.SaveChanges() == 1 ? true : false;
        }


        /// <summary>
        /// 根据Id查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDTO GetById(long id)
        {
            User user = cxt.Set<User>().SingleOrDefault(e => e.Id == id && e.IsDeleted != true);
            return new UserDTO() { Id = user.Id, Name = user.Name, Age = user.Age, Password = user.Password };
        }

        #region 
        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <param name="start"></param>
        ///// <param name="count"></param>
        ///// <returns></returns>
        //public IQueryable<User> GetAll(int start, int count)
        //{
        //    return cxt.Set<User>().OrderBy(e => e.CreateDateTime).Skip(start).Take(count).Where(e => e.IsDeleted == false);
        //}
        /////注意这里的返回值类型是IQueryable<T>类型，他继承与IEnumerable<T>类型，但是我们不要使用IEnumberable类型
        /////为什么呢，假如我们的查询的数据的类中有ICollection<T>类型的属性，我们需要使用Include（）函数避免延迟加载
        /////注意我们这里的查询结果是IQueryable<T>类型的对象，他的Include（）函数的命名空间是：System.Data.Entity

        #endregion


        /// <summary>
        /// 查询所有User
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDTO> GetAllUser()//因为我们的Id类型就是long，所以我们的一张表中的数据理论上最多有long类型取值范围的条数
        {
            List<User> userList = cxt.Set<User>().Where(u => u.IsDeleted == false).ToList();
            foreach (User user in userList)
            {
                yield return new UserDTO() { Id = user.Id, Name = user.Name, Age = user.Age, Password = user.Password };
            }

        }

        //添加用户
        public bool AddUser(UserDTO userDTO)
        {
            User user = new User() { Name = userDTO.Name, Age = userDTO.Age, Password = userDTO.Password };
            cxt.Entry(user).State = System.Data.Entity.EntityState.Added;
            return cxt.SaveChanges() == 1 ? true : false;
        }

        //修改
        public bool EditUser(UserDTO userDTO)
        {
            User user = new User() { Id = userDTO.Id };
            cxt.Entry(user).State = System.Data.Entity.EntityState.Unchanged;
            user.Name = userDTO.Name;
            user.Password = userDTO.Password;
            user.Age = userDTO.Age;
            cxt.Entry(user).State = System.Data.Entity.EntityState.Modified;
            return cxt.SaveChanges() == 1 ? true : false;
        }
    }
}