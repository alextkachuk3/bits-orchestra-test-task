using bits_orchestra_test_task.Data;
using bits_orchestra_test_task.Models;

namespace bits_orchestra_test_task.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationContext _context;

        public EmployeeService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddEmployeesAsync(IEnumerable<Employee> employees)
        {
            _context.Employees.AddRange(employees);
            await _context.SaveChangesAsync();
        }
    }
}
