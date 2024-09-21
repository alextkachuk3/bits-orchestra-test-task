using bits_orchestra_test_task.Models;

namespace bits_orchestra_test_task.Services
{
    public interface IEmployeeService
    {
        public Employee? GetEmployeeById(int id);
        public Task AddEmployeesAsync(IEnumerable<Employee> employees);
        public Task UpdateEmployeeAsync(Employee employee);
        public List<Employee> GetAllEmployees();
        public Task DeleteEmployeeAsync(int id);
    }
}
