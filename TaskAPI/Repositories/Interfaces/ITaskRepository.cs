using TaskAPI.Model;

namespace TaskAPI.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        IEnumerable<MyTask> GetAllTasks();
        MyTask GetTaskById(int id);
        MyTask AddTask(MyTask task);
        void UpdateTask(MyTask task);
        void DeleteTask(int id);
    }
}
