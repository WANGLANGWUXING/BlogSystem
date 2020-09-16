using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BlogSystem.Models
{
    public class BlogSystemContext:DbContext
    {
        public BlogSystemContext()
            :base("BlogDbCon")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 移除一对多
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            // 移除多对多
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<AdminsPermission> AdminsPermission { get; set; }
        public virtual DbSet<SystemMenu> SystemMenu { get; set; }
        public virtual DbSet<WebMenu> WebMenu { get; set; }
        public virtual DbSet<Seo> Seo { get; set; }
        public virtual DbSet<FriendLink> FriendLink { get; set; }
        public virtual DbSet<Copyright> Copyright { get; set; }

    }
}