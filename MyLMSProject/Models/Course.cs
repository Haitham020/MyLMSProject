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

    }
}
