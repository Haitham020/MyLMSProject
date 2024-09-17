namespace MyLMSProject.Models
{
    public class Article : SharedProp
    {
        public int ArticleId { get; set; }
        public string? ArticleTitle { get; set; }
        public string? ArticleDescription { get; set; }
        public string? ArticleAuthor { get; set; }
        public DateTime ArticleDate { get; set; }
        public string? ArticleImg { get; set; }
    }
}
