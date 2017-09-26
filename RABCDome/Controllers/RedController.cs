using RABCDome.Filters;
using RABCDome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RABCDome.Controllers
{
    [CustomAuthroization(AuthorizationType = AuthorizationType.None)]
    public class RedController : Controller
    {

        // GET: Red
        RbacDB db = new RbacDB();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Red(User regUser)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {code = 400});
            }
            try
            {
                var role = db.Roles.FirstOrDefault(r => r.id == 3);
                regUser.Roles.Add(role);
                db.Users.Add(regUser);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { code = 404 });
            }

            return Json(new {code = 200});
        }

    }
}