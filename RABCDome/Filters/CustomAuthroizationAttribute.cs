using RABCDome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace RABCDome.Filters
{
    public enum AuthorizationType { None, Identity, Authorization }
    /// <summary>
    /// 自定义授权过滤器
    /// </summary>
    public class CustomAuthroizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public AuthorizationType AuthorizationType { get; set; } = AuthorizationType.Authorization;
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthorizationType == AuthorizationType.None) return;
            if (filterContext.HttpContext.Session["user"]==null)
            {
                RedirectToLogin(filterContext);
                return;
            }
            if (AuthorizationType == AuthorizationType.Identity) return;

            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var role = filterContext.HttpContext.Session["role"] as Role;
            if (role==null)
            {
                RedirectToLogin(filterContext);
                return;
            }
            var module = role.Modules.FirstOrDefault(m => m.Controller == controller);
            if (module==null)
            {
                RedirectToLogin(filterContext);
            }

        }

        public void RedirectToLogin(AuthorizationContext filterContext)
        {
            var url = new UrlHelper(filterContext.RequestContext);
            filterContext.Result = new RedirectResult(@url.Action("Index", "Login"));
        }

    }
}