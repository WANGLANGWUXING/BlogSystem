using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.DAL
{
    public class AdminsDal:BaseDal<Admins>,IAdminsDal
    {
        public AdminsDal() : base(new BlogSystemContext())
        {
        }
    }
}