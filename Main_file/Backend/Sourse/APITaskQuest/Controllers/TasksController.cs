using Microsoft.AspNetCore.Mvc;
using apTaskQuest.Models;
using apTaskQuest.Services;

namespace apTaskQuest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTasksController : ControllerBase
    {
        private readonly IUserTaskService _taskService;
        private readonly ILogger<UserTasksController> _logger;

        public UserTasksController(IUserTaskService taskService, ILogger<UserTasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<UserTaskResponse>>> GetUserTasks(string userId)
        {
            try
            {
                var tasks = await _taskService.GetUserTasksAsync(userId);
                var response = tasks.Select(MapToResponse).ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tasks for user: {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserTaskResponse>> GetTask(Guid id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                if (task == null) return NotFound();

                return Ok(MapToResponse(task));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting task: {TaskId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserTaskResponse>> CreateTask(CreateUserTaskRequest request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var task = await _taskService.CreateTaskAsync(request);
                var response = MapToResponse(task);

                return CreatedAtAction(nameof(GetTask), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserTaskResponse>> UpdateTask(Guid id, CreateUserTaskRequest request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var task = await _taskService.UpdateTaskAsync(id, request);
                if (task == null) return NotFound();

                return Ok(MapToResponse(task));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task: {TaskId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(Guid id)
        {
            try
            {
                var deleted = await _taskService.DeleteTaskAsync(id);
                if (!deleted) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task: {TaskId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{id}/complete")]
        public async Task<ActionResult<UserTaskResponse>> CompleteTask(Guid id)
        {
            try
            {
                var task = await _taskService.CompleteTaskAsync(id);
                if (task == null) return NotFound();

                return Ok(MapToResponse(task));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing task: {TaskId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("categories")]
        public ActionResult<List<string>> GetCategories()
        {
            var categories = new List<string>
            {
                "learning",
                "sport",
                "nutrition",
                "career",
                "creativity",
                "health",
                "social",
                "finance"
            };

            return Ok(categories);
        }

        private static UserTaskResponse MapToResponse(UserTask task)
        {
            return new UserTaskResponse
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Category = task.Category,
                ExperienceReward = task.ExperienceReward,
                CoinReward = task.CoinReward,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                CompletedAt = task.CompletedAt,
                UserId = task.UserId
            };
        }
    }
}