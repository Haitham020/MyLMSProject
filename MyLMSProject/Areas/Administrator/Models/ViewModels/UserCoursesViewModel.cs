using MyLMSProject.Models;

namespace MyLMSProject.Areas.Administrator.Models.ViewModels
{
    public class UserCoursesViewModel
    {
        public ApplicationUser? User { get; set; }
        public List<Course>? Courses { get; set; }
    }
}
