using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLMSProject.Models
{
    public class Comment : SharedProp
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CommentCreatedAt { get; set; }


        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

    }
}
