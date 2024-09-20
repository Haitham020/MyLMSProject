using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLMSProject.Models;


namespace MyLMSProject.Data
{
    public class LmsDbContext : IdentityDbContext
    {
        public LmsDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<UserCourse> UserCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Course>()
                .HasMany(c => c.Comments)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);

            
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany() 
                .HasForeignKey(c => c.UserId);


            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.User)
                .WithMany()
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCourse>()
                .HasOne(uc => uc.Course)
                .WithMany(c => c.UserCourses)
                .HasForeignKey(uc => uc.CourseId);
        }
    }
}
