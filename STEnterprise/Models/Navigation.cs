using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STEnterprise.Models
{
    public class Navigation : CommonModel
    {
        public byte MenuId { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string MenuName { get; set; }
        public byte MenuLevel { get; set; }
        public byte? ParentId { get; set; }
        public bool HasSubMenu { get; set; }
        public bool IsDefault { get; set; }
    }
}