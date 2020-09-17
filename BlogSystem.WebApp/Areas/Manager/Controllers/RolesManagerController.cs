using BlogSystem.IBLL;
using BlogSystem.WebApp.Areas.Manager.Common;
using BlogSystem.WebApp.Areas.Manager.Data.Roles;
using log4net;
using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

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
        public async Task<ActionResult> List(string search="",int page = 1)
        {
            // 注册日志
            ILog log = LogManager.GetLogger(typeof(RolesManagerController));
            // （1）得到数据总条数
            var count = await _rolesBll.GetRolesCountAsync(search);
            //  (2)设置总页数
            //var total = PageConfig.GetTotalPage(count);
            //  (3)设置每页要展示条数
            //var pageSize = PageConfig.GetPageSize();

            var data = await _rolesBll.GetRolesList(search,false);

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
    }
}