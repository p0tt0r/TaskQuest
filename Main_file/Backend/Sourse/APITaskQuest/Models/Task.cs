namespace apTaskQuest.Models
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = "general";
        public int ExperienceReward { get; set; } = 100;
        public int CoinReward { get; set; } = 50;
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}