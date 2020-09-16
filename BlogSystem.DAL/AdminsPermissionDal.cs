using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.DAL
{
    public class AdminsPermissionDal : BaseDal<AdminsPermission>,IAdminsPermissionDal
    {
        public AdminsPermissionDal() : base(new BlogSystemContext())
        {
        }
    }
}