using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RABCDome.Filters;
using RABCDome.Model;

namespace RABCDome.Controllers
{
    [CustomAuthroization(AuthorizationType=AuthorizationType.Identity)]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 头部的分部视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Header()
        {
            //var user = Session["user"] as User;
            //var roles = Session["roles"] as List<Role>;
            //var role = Session["role"] as Role;
            //List<SelectListItem> rolelist = new List<SelectListItem>();
            //foreach (var item in roles)
            //{
            //    rolelist.Add(new SelectListItem{Text=item.name,Value=item.id.ToString(),Selected=item.id==role.id});
            //}
            //ViewBag.rolelist = rolelist;

            //return PartialView(user);
            var user = Session["user"] as User;
            var role = Session["role"] as Role;

            ViewBag.UserName = user.UserName;
            ViewBag.RoleName = role.name;
            return PartialView();
        }
        /// <summary>
        /// 导航栏的分部视图
        /// 负责查询你所选择的角色的所有模块
        /// 并且把所有模块渲染出来
        /// </summary>
        /// <returns></returns>
        public ActionResult Nav(int roleId=0)
        {
            //获取Session中的用户模块表
            var roleModules=Session["roleModules"] as Dictionary<int, ICollection<Module>>;
            //获取Session里的默认的角色
            var role = Session["role"] as Role;
            var roles = Session["roles"] as List<Role>;

            //根据参数里的roleId，获取某一个角色的模块表
            List<Module> modules;
            if (roleModules.ContainsKey(roleId))
            {
                modules = roleModules[roleId].ToList();
                //切换当前的角色
                Session["role"] = roles.FirstOrDefault(r => r.id == roleId);
            }
            else
            {
                modules = roleModules[role.id].ToList();
            }      

            return PartialView(modules);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

    }
}