using BlogSystem.IBLL;
using BlogSystem.WebApp.Areas.Manager.Common;
using BlogSystem.WebApp.Areas.Manager.Data.Roles;
using log4net;
using PagedList;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogSystem.WebApp.Areas.Manager.Controllers
{
    public class RolesManagerController : Controller
    {
        private IRolesBll _rolesBll;
        public RolesManagerController(IRolesBll rolesBll)
        {
            _rolesBll = rolesBll;
        }
        //1. 每页展示多少条
        //2. 一共能分多少页
        public async Task<ActionResult> List(string search = "", int page = 1)
        {
            // 注册日志
            //ILog log = LogManager.GetLogger(typeof(RolesManagerController));
            // （1）得到数据总条数
            var count = await _rolesBll.GetRolesCountAsync(search);
            //  (2)设置总页数
            //var total = PageConfig.GetTotalPage(count);
            //  (3)设置每页要展示条数
            //var pageSize = PageConfig.GetPageSize();

            var data = await _rolesBll.GetRolesList(search, false);

            List<RolesListViewModel> list = new List<RolesListViewModel>();

            foreach (var item in data)
            {
                RolesListViewModel rlvm = new RolesListViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    UpdateTime = item.UpdateTime
                };
                list.Add(rlvm);
            }

            ViewBag.Search = search;
            ViewBag.PageIndex = page;
            IPagedList<RolesListViewModel> pages = list.ToPagedList(page, PageConfig.GetPageSize());
            return View(pages);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new AddRolesViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddRolesViewModel roles)
        {
            if (ModelState.IsValid)
            {
                // 验证合法，
                int rs = await _rolesBll.AddRolesAsync(roles.Title);
                if (rs > 0)
                {
                    Response.Write("<script>alert('新增成功');location.href='../../Manager/RolesManager/List'</script>");
                }
            }
            return View(roles);
        }

        /// <summary>
        /// 唯一验证
        /// </summary>
        /// <param name="Title"></param>
        /// <returns></returns>
        public async Task<JsonResult> CheckRolesTitle(string Title)
        {
            var res = await _rolesBll.IsExistsAsync(Title);
            return Json(!res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var data = await _rolesBll.GetRolesAsync(id);

            return View(new EditRolesViewModel()
            {
                Id = data.Id,
                Title = data.Title
            });

        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditRolesViewModel roles)
        {
            if (ModelState.IsValid)
            {
                var res = await _rolesBll.EditRolesAsync(roles.Id, roles.Title);
                if (res > 0)
                {
                    Response.Write("<script>alert('编辑成功');location.href='../../../Manager/RolesManager/List'</script>");
                }

            }

            return View(roles);

        }
        [HttpGet]
        public async Task Delete(Guid id)
        {
            var res = await _rolesBll.DeleteRolesAsync(id);
            if (res > 0)
            {
                Response.Write("<script>alert('删除成功');location.href='../../../Manager/RolesManager/List'</script>");
            }
            else
            {
                Response.Write("<script>alert('删除失败');location.href='../../../Manager/RolesManager/List'</script>");
            }
        }
    }
}