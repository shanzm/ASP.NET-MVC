using DemoBll;
using DemoDTO;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        UserBll userBll = new UserBll();

        public ActionResult Index()
        {

            return Content("Connect Success！");
        }
        public ActionResult Delete(long id)
        {
            if (userBll.Delete(id))
            {
                return Redirect ("List");
            }
            else
            {
                return Content("error");
            }
        }

        [HttpGet]
        public ActionResult List()
        {
            IEnumerable<UserDTO> userDTOs = userBll.GetAllUser();

            return View(userDTOs);
        }
        
        //undone:在添加新用户的时候需要验证：
        //1.是否复合UserModel的特性约束（用户名长度，密码长度等），UserModel的特性约束要和LoginModel的特性约束一致
        //2.使用Ajax实现异步查询数据库中是否已经存在
        [HttpPost]
        public ActionResult Add(UserModel user, string BtnSubmit)
        {
            UserDTO userDTO = new UserDTO() { Age = user.Age, Name = user.Name, Password = user.Password };
            if (BtnSubmit == "AddNew")
            {
                userBll.AddUser(userDTO);
            }
            else if (BtnSubmit == "Cancel")
            {

            }
            return Redirect("~/User/List");

        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Edit(long id)
        {
            UserDTO userDTO = userBll.GetById(id);
            return View(userDTO);
        }

        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            UserDTO userDTO = new UserDTO() { Id = user.Id, Name = user.Name, Age = user.Age, Password = user.Password };
            if (userBll.EditUser(userDTO))
            {
                return Redirect("~/User/List");
            }
            else
            {
                return Content("error");
            }
        }
    }
}