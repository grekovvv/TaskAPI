using Microsoft.EntityFrameworkCore;
using TaskAPI.Data;
using TaskAPI.Model;
using TaskAPI.Repositories.Interfaces;

namespace TaskAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MyTask> GetAllTasks()
        {
            return _context.Tasks.AsNoTracking().ToList();
        }

        public MyTask GetTaskById(int id)
        {
            return _context.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public MyTask AddTask(MyTask task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public void UpdateTask(MyTask task)
        {
            try
            {
                _context.Tasks.Update(task);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public void DeleteTask(int id)
        {
            var taskToRemove = _context.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == id);
            if (taskToRemove != null)
            {
                _context.Tasks.Remove(taskToRemove);
                _context.SaveChanges();
            }
        }
    }
}
