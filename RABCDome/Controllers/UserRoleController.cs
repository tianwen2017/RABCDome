using RABCDome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using RABCDome.ViewModels;

namespace RABCDome.Controllers
{
    public class UserRoleController : Controller
    {
        // GET: UserRole
        RbacDB db = new RbacDB();
        public ActionResult Index()
        {
            var result = db.Users.Include(u => u.Roles);
            return View(result);
        }

        public ActionResult Create()
        {
            //所有用户下拉框
            ViewBag.UserOptions = from u in db.Users
                select new SelectListItem { Text = u.UserName, Value = u.id.ToString() };
            //所有角色下拉框
            ViewBag.RoleOptions = from r in db.Roles
                select new SelectListItem { Text = r.name, Value = r.id.ToString() };
            return View();
        }

        public ActionResult Edit(UserRoleViewModel userrole)
        {
            userrole.UserName = db.Users.FirstOrDefault(r => r.id == userrole.UserId).UserName;
            userrole.RoleName = db.Roles.FirstOrDefault(m => m.id == userrole.RoleId).name;

            ViewBag.RoleOptions = from r in db.Roles
                select new SelectListItem { Text = r.name, Value = r.id.ToString() };
            return View(userrole);
        }

        public ActionResult Delete(UserRoleViewModel userrole)
        {
            var user = db.Users.FirstOrDefault(r => r.id == userrole.UserId);
            var role = new Role { id = userrole.RoleId };
            db.Roles.Attach(role);
            user.Roles.Remove(role);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }
        public ActionResult Insert(UserRoleViewModel userrole)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            //先把要添加角色的用户找出来
            var user = db.Users.FirstOrDefault(r => r.id == userrole.UserId);
            //构造一个要添加的角色模块
            var role = new Role { id = userrole.RoleId };
            //伪装成从数据库读取出来的一样
            db.Roles.Attach(role);
            //要添加到角色模块，Add到用户的模块集合中
            user.Roles.Add(role);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }

            return Json(new { code = 200 });
        }

        public ActionResult Update(UserRoleViewModel userrole)
        {
            if (userrole.RoleId == userrole.UpdateRoleId)
            {
                return Json(new { code = 400 });
            }

            var user = db.Users.FirstOrDefault(r => r.id == userrole.UserId);
            var role = new Role { id = userrole.RoleId };
            db.Roles.Attach(role);
            var updateRole = new Role { id = userrole.UpdateRoleId };
            db.Roles.Attach(updateRole);

            user.Roles.Remove(role);
            user.Roles.Add(updateRole);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }


    }
}