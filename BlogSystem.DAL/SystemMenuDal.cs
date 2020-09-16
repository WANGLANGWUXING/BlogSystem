using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.DAL
{
    public class SystemMenuDal : BaseDal<SystemMenu>,ISystemMenuDal
    {
        public SystemMenuDal() : base(new BlogSystemContext())
        {
        }
    }
}