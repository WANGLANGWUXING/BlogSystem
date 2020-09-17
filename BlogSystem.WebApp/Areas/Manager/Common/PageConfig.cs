using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BlogSystem.WebApp.Areas.Manager.Common
{
    public class PageConfig
    {
        /// <summary>
        /// 获取每页显示多少条
        /// </summary>
        /// <returns>显示条数</returns>        
        public static int GetPageSize()
        {
            int.TryParse(ConfigurationManager.AppSettings["PageSize"], out int res);
            return res;
        }
        /// <summary>
        /// 获取分页数
        /// </summary>
        /// <param name="count">数据总条数</param>
        /// <returns>分页数</returns>
        public static int GetTotalPage(int count)
        {
            return count % GetPageSize() == 0 ? count / GetPageSize() : count / GetPageSize() + 1;
        }
    }
}