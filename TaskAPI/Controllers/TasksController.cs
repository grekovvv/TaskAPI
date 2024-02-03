using Microsoft.AspNetCore.Mvc;
using TaskAPI.Filters;
using TaskAPI.Model;
using TaskAPI.Repositories.Interfaces;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorization]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskRepository taskRepository, ILogger<TasksController> logger)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MyTask>> GetMyTasks()
        {
            try
            {
                return Ok(_taskRepository.GetAllTasks());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return StatusCode(500, "Internal Server Error");
            }
            
        }

        [HttpGet("{id}")]
        public ActionResult<MyTask> GetMyTask(int id)
        {
            try
            { 
                var task = _taskRepository.GetTaskById(id);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpPost]
        public ActionResult<MyTask> CreateMyTask(MyTask task)
        {
            try
            {
                var createdTask = _taskRepository.AddTask(task);
                return CreatedAtAction(nameof(GetMyTask), new { id = createdTask.Id }, createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMyTask(int id, MyTask task)
        {
            try
            {
                if (id != task.Id)
                {
                    return BadRequest();
                }

                var existingTask = _taskRepository.GetTaskById(id);
                if (existingTask == null)
                {
                    return NotFound();
                }

                _taskRepository.UpdateTask(task);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return StatusCode(500, "Internal Server Error");
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMyTask(int id)
        {
            try
            {
                var existingTask = _taskRepository.GetTaskById(id);
                if (existingTask == null)
                {
                    return NotFound();
                }

                _taskRepository.DeleteTask(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
