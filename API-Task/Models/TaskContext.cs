using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API_Task.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext()
        {
            
        }


        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TasksModel> sample { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Data Source=localhost\\sql ,1401; Initial Catalog=task_database; User Id=sa; Password=test@123;Trust Server Certificate=True;");
    }
}
