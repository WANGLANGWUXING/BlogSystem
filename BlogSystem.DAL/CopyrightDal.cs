using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.DAL
{
    public class CopyrightDal : BaseDal<Copyright>,ICopyrightDal
    {
        public CopyrightDal() : base(new BlogSystemContext())
        {
        }
    }
}