using bits_orchestra_test_task.Models;

namespace bits_orchestra_test_task.Services
{
    public interface IEmployeeService
    {
        Task AddEmployeesAsync(IEnumerable<Employee> employees);
    }
}
