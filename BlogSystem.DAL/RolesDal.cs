using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.DAL
{
    public class RolesDal:BaseDal<Roles>,IRolesDal
    {
        public RolesDal() : base(new BlogSystemContext())
        {
        }
    }
}