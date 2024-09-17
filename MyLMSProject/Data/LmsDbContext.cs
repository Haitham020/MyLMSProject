using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLMSProject.Models;


namespace MyLMSProject.Data
{
    public class LmsDbContext : IdentityDbContext
    {
        public LmsDbContext(DbContextOptions options) : base (options)
        {
            
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Menu> Menus { get; set; }

    }
}
