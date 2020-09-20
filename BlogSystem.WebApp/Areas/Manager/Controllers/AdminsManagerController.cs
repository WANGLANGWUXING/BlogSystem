using BlogSystem.IBLL;
using BlogSystem.WebApp.Areas.Manager.Common;
using BlogSystem.WebApp.Areas.Manager.Data.Admins;
using CodeCarvings.Piczard;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.WebApp.Areas.Manager.Controllers
{
    public class AdminsManagerController : Controller
    {
        private IAdminsBll _adminsBll;
        private IRolesBll _rolesBll;
        public AdminsManagerController(IAdminsBll adminsBll, IRolesBll rolesBll)
        {
            _adminsBll = adminsBll;
            _rolesBll = rolesBll;
        }


        public async Task<ActionResult> List(string search = "", int page = 1)
        {
            var data = await _adminsBll.GetAdminsByNickName(search);

            List<AdminsListViewModel> list = new List<AdminsListViewModel>();

            foreach (var item in data)
            {
                var role = await _rolesBll.GetRolesAsync(item.RolesId);

                AdminsListViewModel alvm = new AdminsListViewModel()
                {
                    Id = item.Id,
                    Email = item.Email,
                    Photo = item.Photo,
                    NickName = item.NickName,
                    UpdateTime = item.UpdateTime,
                    RolesTitle = role.Title
                };
                list.Add(alvm);
            }
            ViewBag.Search = search;
            ViewBag.PageIndex = page;
            IPagedList<AdminsListViewModel> pages = list.ToPagedList(page, PageConfig.GetPageSize());

            return View(pages);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            await BindRoles(Guid.Empty);

            return View(new AddAdminsViewModel());
        }
        /// <summary>
        /// 绑定权限下拉列表
        /// </summary>
        /// <param name="id">当前选中的值</param>
        /// <returns></returns>
        private async Task BindRoles(Guid id)
        {
            var roles = await _rolesBll.GetRolesList("", true);

            if (id == Guid.Empty)
            {
                SelectList rolesList = new SelectList(roles, "Id", "Title");

                ViewBag.RolesList = rolesList;

            }
            else
            {
                SelectList rolesList = new SelectList(roles, "Id", "Title", id);

                ViewBag.RolesList = rolesList;

            }

        }

        [HttpPost]
        public async Task<ActionResult> Add(AddAdminsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["MyPhoto"];

                var names = UploadFiles(file, @"../../Upload/Admins/");
                var rs = await _adminsBll.AddAdminsAsync(
                    model.Email, model.Password, model.NickName, names[0],
                    names[1], model.RolesId);

                if (rs > 0)
                {
                    return Content("<script>alert('新增成功');location.href='../../Manager/AdminsManager/List'</script>");
                }
            }
            return View(model);

        }

        public string[] UploadFiles(HttpPostedFileBase file,string url)
        {
            if (!file.FileName.Equals(""))
            {
                Random r = new Random();
                var newName = DateTime.Now.ToString("yyyyMMddHHmmss")
                    + r.Next(1000, 10000)
                    + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                var path = Server.MapPath(url + newName);
                file.SaveAs(path);// 保存的正常大小的图片
                ImageProcessingJob job = new ImageProcessingJob();// 实例化第三方缩略图插件
                job.Filters.Add(new FixedResizeConstraint(24, 24));
                var smName = newName.Substring(0, newName.LastIndexOf('.'))
                             + "_sm"
                             + newName.Substring(newName.LastIndexOf('.'));
                var smPath = Server.MapPath(url + smName);
                job.SaveProcessedImageToFileSystem(path, smPath);
                return new string[] { newName, smName };

            }
            return new string[] { "default.jpeg", "default.png" };
        }

        public async Task<JsonResult> CheckEmailAsync(string Email)
        {
            var rs = await _adminsBll.IsExistsAsync(Email);
            return Json(!rs, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var data = await _adminsBll.GetAdminsById(id);

            await BindRoles(data.RolesId);

            return View(new EditAdminsViewModel()
            {
                Id = data.Id,
                Email = data.Email,
                Password = data.Password,
                NickName = data.NickName,
                Photo = data.Photo,
                RolesId = data.RolesId,
                Images = data.Images

            });

        }
        [HttpPost]
        public async Task<ActionResult> Edit(EditAdminsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["MyPhoto"];
                var rs = -1;
                if (file.FileName != "" && file.FileName != null)
                {
                    var names = UploadFiles(file, @"../../../Upload/Admins/");
                    rs = await _adminsBll.EditAdminsAsync(
                       model.Id, model.Email, model.Password, model.NickName,
                       names[0], names[1], model.RolesId);

                }
                else
                {
                    rs = await _adminsBll.EditAdminsAsync(
                       model.Id, model.Email, model.Password, model.NickName,
                       model.Photo, model.Images, model.RolesId); ;
                }

                if (rs > 0)
                {
                    return Content("<script>alert('修改成功');location.href='../../../Manager/AdminsManager/List'</script>");

                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var rs = await _adminsBll.DeleteAdminsAsync(id);
            if (rs > 0)
            {
                return Content("<script>alert('删除成功');location.href='../../../Manager/AdminsManager/List'</script>");

            }
            else if (rs == -2)
            {
                return Content("<script>alert('数据传输丢失，请稍后再试');location.href='../../../Manager/AdminsManager/List'</script>");

            }
            else
            {
                return Content("<script>alert('删除失败');location.href='../../../Manager/AdminsManager/List'</script>");

            }
        }
    }
}