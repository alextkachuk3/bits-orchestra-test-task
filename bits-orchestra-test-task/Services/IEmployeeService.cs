using bits_orchestra_test_task.Models;
using Microsoft.EntityFrameworkCore;

namespace bits_orchestra_test_task.Services
{
    public interface IEmployeeService
    {
        Employee? GetEmployeeById(int id);
        public Task AddEmployeesAsync(IEnumerable<Employee> employees);
        Task UpdateEmployeeAsync(Employee employee);
        public List<Employee> GetAllEmployees();        
    }
}
