using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.WebApp.Areas.Manager.Data.Roles
{
    public class AddRolesViewModel
    {
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "权限名称")]
        [Remote("CheckRolesTitle","RolesManager",ErrorMessage ="该权限名称以存在")]
        public string Title { get; set; }
    }
}