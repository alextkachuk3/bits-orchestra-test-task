using Microsoft.EntityFrameworkCore;
using bits_orchestra_test_task.Models;

namespace bits_orchestra_test_task.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; } = null!;
    }
}
