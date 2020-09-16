using BlogSystem.IDAL;
using BlogSystem.Models;

namespace BlogSystem.DAL
{
    public class FriendLinkDal : BaseDal<FriendLink>,IFriendLinkDal
    {
        public FriendLinkDal() : base(new BlogSystemContext())
        {
        }
    }
}