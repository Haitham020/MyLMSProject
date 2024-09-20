using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLMSProject.Models
{
    public class Course : SharedProp
    {
        public int CourseId { get; set; }
        public string? CourseTitle { get; set; }
        public string? CourseDescription { get; set; }
        public string? CourseImg { get; set; }
        public int CourseHours { get; set; }
        public string? CourseLanguage { get; set; }
        public decimal CoursePrice { get; set; }
        public int CourseLecture { get; set; }
        public Type CourseType { get; set; }
        public enum Type
        {
            Online, Recorded
        }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<UserCourse>? UserCourses { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        [Required]
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
