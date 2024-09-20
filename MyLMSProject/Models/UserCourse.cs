using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace MyLMSProject.Models
{
    public class UserCourse
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
