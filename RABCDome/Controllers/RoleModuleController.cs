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
    public class RoleModuleController : Controller
    {
        // GET: RoleModule
        RbacDB db = new RbacDB(); 
        public ActionResult Index()
        {
            var result = db.Roles.Include(r => r.Modules);
            return View(result);
        }

        public ActionResult Create()
        {
            //所有模块下拉框
            ViewBag.ModuleOptions = from m in db.Modules
                select new SelectListItem {Text = m.name, Value = m.id.ToString()};
            //所有角色下拉框
            ViewBag.RoleOptions = from r in db.Roles
                select new SelectListItem {Text = r.name, Value = r.id.ToString()};
            return View();
        }

        public ActionResult Edit(RoleModuleViewModel roleModule)
        {
            roleModule.RoleName = db.Roles.FirstOrDefault(r => r.id == roleModule.RoleId).name;
            roleModule.ModuleName = db.Modules.FirstOrDefault(m => m.id == roleModule.ModuleId).name;

            ViewBag.ModuleOptions = from r in db.Modules
                select new SelectListItem { Text = r.name, Value = r.id.ToString() };
            return View(roleModule);
        }

        public ActionResult Delete(RoleModuleViewModel roleModule)
        {
            var role = db.Roles.FirstOrDefault(r => r.id == roleModule.RoleId);
            var module = new Module { id = roleModule.ModuleId };
            db.Modules.Attach(module);
            role.Modules.Remove(module);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new {code = 200});
        }

        public ActionResult Insert(RoleModuleViewModel roleModule)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            //先把要添加权限的角色找出来
            var role = db.Roles.FirstOrDefault(r => r.id == roleModule.RoleId);
            //var moduleA = db.Modules.FirstOrDefault(m => m.id == roleModule.ModuleId);
            //构造一个要添加的权限模块
            var module = new Module {id = roleModule.ModuleId};
            //伪装成从数据库读取出来的一样
            db.Modules.Attach(module);
            //要添加到权限模块，Add到角色的模块集合中
            role.Modules.Add(module);
            if (db.SaveChanges()==0)
            {
                return Json(new {code = 400});
            }
            
            return Json(new { code = 200 });
        }

        public ActionResult Update(RoleModuleViewModel roleModule)
        {
            if (roleModule.ModuleId==roleModule.UpdateModuleid)
            {
                return Json(new { code = 400 });
            }

            var role = db.Roles.FirstOrDefault(r => r.id == roleModule.RoleId);
            var module = new Module { id = roleModule.ModuleId };
            db.Modules.Attach(module);
            var updateModule = new Module {id = roleModule.UpdateModuleid};
            db.Modules.Attach(updateModule);

            role.Modules.Remove(module);
            role.Modules.Add(updateModule);
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }

    }
}