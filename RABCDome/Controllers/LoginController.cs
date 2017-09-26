using RABCDome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RABCDome.Filters;
namespace RABCDome.Controllers
{
    [CustomAuthroization(AuthorizationType = AuthorizationType.None)]
    public class LoginController : Controller
    {
        RbacDB ur=new RbacDB();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User loginUser)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            //查找用户
            var user = ur.Users.FirstOrDefault(u =>u.UserName == loginUser.UserName && u.PassWord == loginUser.PassWord);

            if (user == null) return Json(new { code=404});
            Session["user"] = user;

            //var roleModules = user.Roles.ToDictionary(r => r.id, r => r.Modules);
            //Session["roleModules"] = roleModules;
            ////获取用户角色表
            //var roles = user.Roles.ToList();
            ////存入到Session中以便复用 
            //Session["roles"] = roles;
            ////设置当前角色为用户角色表里的第一个
            //Session["role"] = roles[0];


            //Func<Role, bool> func1 = delegate (Role role1)
            //{
            //    if (role1.id == 1) return true;
            //    return false;
            //};

            Func<Role, bool> funcl = rolel => true;
            var role = user.Roles.FirstOrDefault(funcl);
            Session["role"] = role;


            return Json(new { code = 200 });
        }
        private bool Funcl(Role rolel)
        {
            if (rolel.id == 3) return true;
            return false;
        }
    }
}