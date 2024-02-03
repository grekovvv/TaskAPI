using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskAPI.Data;
using TaskAPI.Repositories;
using TaskAPI.Repositories.Interfaces;

namespace TaskAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile($"appsettings.json").Build();

           /* Log.Logger = new LoggerConfiguration()
            .WriteTo.File("logs/myapp-.txt", rollingInterval: RollingInterval.Day) // Добавляем логирование в файл
            .CreateLogger();*/


            builder.Services.AddControllers();
            builder.Services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("SQLiteConnection")));
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
