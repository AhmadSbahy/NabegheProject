using Microsoft.EntityFrameworkCore;
using Nabeghe.Domain.Models.Blog;
using Nabeghe.Domain.Models.ContactUs;
using Nabeghe.Domain.Models.Course;
using Nabeghe.Domain.Models.CourseCategory;
using Nabeghe.Domain.Models.Order;
using Nabeghe.Domain.Models.User;

namespace Nabeghe.Infra.Data.Context
{
    public class NabegheContext : DbContext
	{
		#region Constructor

		public NabegheContext(DbContextOptions<NabegheContext> options)
			: base(options)
		{
      
        }

        #endregion

        #region Dbsets

        #region User

        public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }

		#endregion

		#region Permission

		public DbSet<Permission> Permissions { get; set; }
		public DbSet<RolePermission> RolePermissions { get; set; }

		#endregion

		#region Course Category

		public DbSet<CourseCategory> CourseCategories { get; set; }

		#endregion

		#region Course

		public DbSet<Course> Courses { get; set; }

		public DbSet<CourseStatus> CourseStatus { get; set; }
		
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }

        public DbSet<CourseLike> CourseLikes { get; set; }

        public DbSet<CourseDiscount> CourseDiscounts { get; set; }

		#endregion

        #region Blog

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogCommentLike> BlogCommentLikes { get; set; }
        public DbSet<BlogCommentReply> BlogCommentReplies { get; set; }

        #endregion

		#region Course Comment

		public DbSet<CourseComment> CourseComments { get; set; }

        public DbSet<CommentReply> CommentReplies { get; set; }

        public DbSet<CommentLike> CommentLikes { get; set; }

        #endregion

        #region Order

        public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrdersDetails { get; set; }

		#endregion

        #region ContactUs

        public DbSet<ContactUs> ContactUs { get; set; }

        #endregion

		#endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
