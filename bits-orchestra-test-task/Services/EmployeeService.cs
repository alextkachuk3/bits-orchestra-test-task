using bits_orchestra_test_task.Data;
using bits_orchestra_test_task.Models;
using Microsoft.EntityFrameworkCore;

namespace bits_orchestra_test_task.Services
{
    public class EmployeeService(ApplicationContext context) : IEmployeeService
    {
        private readonly ApplicationContext _context = context;

        public async Task AddEmployeesAsync(IEnumerable<Employee> employees)
        {
            _context.Employees.AddRange(employees);
            await _context.SaveChangesAsync();
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
    }
}
