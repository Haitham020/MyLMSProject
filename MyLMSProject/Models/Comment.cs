namespace MyLMSProject.Models
{
    public class Comment : SharedProp
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CommentCreatedAt { get; set; }
    }
}
