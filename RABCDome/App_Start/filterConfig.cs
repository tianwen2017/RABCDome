using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RABCDome.Filters;

namespace RABCDome
{
    public class filterConfig
    {
        public static void RegisterGlobalFilter(GlobalFilterCollection filters)
        {
            filters.Add(new CustomAuthroizationAttribute());
        }
    }
}