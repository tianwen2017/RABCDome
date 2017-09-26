using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RABCDome.ViewModels
{
    public class RoleModuleViewModel
    {
        public int ModuleId { get; set; }
        public int RoleId { get;set; }

        public int UpdateModuleid { get; set; }
        public string RoleName { get; set; }
        public string ModuleName { get; set; }
    }
}