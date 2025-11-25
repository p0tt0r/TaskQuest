namespace apTaskQuest.Models
{
    public class CreateUserTaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = "general";
        public int ExperienceReward { get; set; } = 100;
        public int CoinReward { get; set; } = 50;
        public string UserId { get; set; } = string.Empty;
    }
}
