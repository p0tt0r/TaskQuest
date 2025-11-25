namespace apTaskQuest.Models
{
    public class UserTaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int ExperienceReward { get; set; }
        public int CoinReward { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}