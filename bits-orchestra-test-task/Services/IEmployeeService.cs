using bits_orchestra_test_task.Models;
using Microsoft.EntityFrameworkCore;

namespace bits_orchestra_test_task.Services
{
    public interface IEmployeeService
    {
        public Task AddEmployeesAsync(IEnumerable<Employee> employees);

        public List<Employee> GetAllEmployees();
    }
}
