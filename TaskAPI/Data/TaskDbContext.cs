using Microsoft.EntityFrameworkCore;
using TaskAPI.Model;

namespace TaskAPI.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        public DbSet<MyTask> Tasks { get; set; }
    }
}
