﻿namespace MyLMSProject.Models
{
    public class Category : SharedProp
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
