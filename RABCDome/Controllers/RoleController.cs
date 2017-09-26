using RABCDome.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RABCDome.Controllers
{
    public class RoleController : Controller
    {
        RbacDB db = new RbacDB();
        // GET: Role
        public ActionResult Index()
        {
            return View(db.Roles);
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var roles = db.Roles.FirstOrDefault(m => m.id == id);
            if (roles == null) return Content("未找到要编辑的角色");
            return View(roles);
        }

        public ActionResult Delete(int id)
        {
            Role role = new Role();
            role.id = id;
            db.Roles.Attach(role);
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Save(Role role)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            db.Roles.AddOrUpdate(role);
            db.SaveChanges();
            return Json(new { code = 200 });
        }

    }
}