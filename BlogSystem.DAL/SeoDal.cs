using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.DAL
{
    public class SeoDal : BaseDal<Seo>,ISeoDal
    {
        public SeoDal() : base(new BlogSystemContext())
        {
        }
    }
}