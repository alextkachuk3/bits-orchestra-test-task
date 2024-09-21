using bits_orchestra_test_task.Data;
using bits_orchestra_test_task.Models;

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

        public Employee? GetEmployeeById(int id)
        {
            return _context.Employees.Find(id);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
