using RABCDome.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RABCDome.Controllers
{
    public class UserController : Controller
    {
        RbacDB db = new RbacDB();
        // GET: User
        public ActionResult Index()
        {
            return View(db.Users);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var users = db.Users.FirstOrDefault(m => m.id == id);
            if (users == null) return Content("未找到要编辑的用户");
            return View(users);
        }

        public ActionResult Delete(int id)
        {
            User user = new User();
            user.id = id;
            db.Users.Attach(user);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Save(User user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            db.Users.AddOrUpdate(user);
            db.SaveChanges();
            return Json(new { code = 200 });
        }
    }
}