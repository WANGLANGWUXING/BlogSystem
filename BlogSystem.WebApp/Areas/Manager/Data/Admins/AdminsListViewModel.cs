using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogSystem.WebApp.Areas.Manager.Data.Admins
{
    public class AdminsListViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "昵称")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "头像")]
        public string Photo { get; set; }
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "权限名称")]
        public string RolesTitle { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}