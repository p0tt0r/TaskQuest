using apTaskQuest.Models;

namespace apTaskQuest.Services
{
    public interface IUserTaskService
    {
        Task<List<UserTask>> GetUserTasksAsync(string userId);
        Task<UserTask?> GetTaskByIdAsync(Guid id);
        Task<UserTask> CreateTaskAsync(CreateUserTaskRequest request);
        Task<UserTask?> UpdateTaskAsync(Guid id, CreateUserTaskRequest request);
        Task<bool> DeleteTaskAsync(Guid id);
        Task<UserTask?> CompleteTaskAsync(Guid id);
    }

    public class UserTaskService : IUserTaskService
    {
        private static readonly List<UserTask> _tasks = new();
        private readonly ILogger<UserTaskService> _logger;

        public UserTaskService(ILogger<UserTaskService> logger)
        {
            _logger = logger;
        }

        public Task<List<UserTask>> GetUserTasksAsync(string userId)
        {
            var userTasks = _tasks.Where(t => t.UserId == userId).ToList();
            return Task.FromResult(userTasks);
        }

        public Task<UserTask?> GetTaskByIdAsync(Guid id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(task);
        }

        public Task<UserTask> CreateTaskAsync(CreateUserTaskRequest request)
        {
            var task = new UserTask
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Category = request.Category,
                ExperienceReward = request.ExperienceReward,
                CoinReward = request.CoinReward,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow
            };

            _tasks.Add(task);
            _logger.LogInformation("Created task: {TaskId} for user: {UserId}", task.Id, task.UserId);

            return Task.FromResult(task);
        }

        public Task<UserTask?> UpdateTaskAsync(Guid id, CreateUserTaskRequest request)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null) return Task.FromResult<UserTask?>(null);

            existingTask.Title = request.Title;
            existingTask.Description = request.Description;
            existingTask.Category = request.Category;
            existingTask.ExperienceReward = request.ExperienceReward;
            existingTask.CoinReward = request.CoinReward;

            _logger.LogInformation("Updated task: {TaskId}", id);
            return Task.FromResult<UserTask?>(existingTask);
        }

        public Task<bool> DeleteTaskAsync(Guid id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return Task.FromResult(false);

            _tasks.Remove(task);
            _logger.LogInformation("Deleted task: {TaskId}", id);
            return Task.FromResult(true);
        }

        public Task<UserTask?> CompleteTaskAsync(Guid id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return Task.FromResult<UserTask?>(null);

            task.IsCompleted = true;
            task.CompletedAt = DateTime.UtcNow;

            _logger.LogInformation("Completed task: {TaskId}. Reward: {Exp} exp, {Coins} coins",
                id, task.ExperienceReward, task.CoinReward);

            return Task.FromResult<UserTask?>(task);
        }
    }
}