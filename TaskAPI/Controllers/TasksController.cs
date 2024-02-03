using Microsoft.AspNetCore.Mvc;
using TaskAPI.Model;
using TaskAPI.Repositories.Interfaces;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
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

                return StatusCode(500, "Internal Server Error");
            }
            
        }

        [HttpGet("{id}")]
        public ActionResult<MyTask> GetMyTask(int id)
        {
            var task = _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public ActionResult<MyTask> CreateMyTask(MyTask task)
        {
            var createdTask = _taskRepository.AddTask(task);
            return CreatedAtAction(nameof(GetMyTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMyTask(int id, MyTask task)
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

        [HttpDelete("{id}")]
        public IActionResult DeleteMyTask(int id)
        {
            var existingTask = _taskRepository.GetTaskById(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            _taskRepository.DeleteTask(id);
            return NoContent();
        }
    }
}
