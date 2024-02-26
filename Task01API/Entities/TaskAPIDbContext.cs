using Microsoft.EntityFrameworkCore;
using Task01API.Models;

namespace Task01API.Entities
{
    public class TaskAPIDbContext:DbContext
    {
        public TaskAPIDbContext(DbContextOptions option) : base(option)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get;set; }
    }
}
