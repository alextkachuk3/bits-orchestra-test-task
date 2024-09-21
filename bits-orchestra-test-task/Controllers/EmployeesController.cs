using bits_orchestra_test_task.Models;
using bits_orchestra_test_task.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace bits_orchestra_test_task.Controllers
{
    public class EmployeesController(ILogger<EmployeesController> logger, IEmployeeService employeeService) : Controller
    {
        private readonly ILogger<EmployeesController> _logger = logger;
        private readonly IEmployeeService _employeeService = employeeService;

        public IActionResult Index()
        {
            List<Employee>? files = _employeeService.GetAllEmployees();
            return View(files);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var employees = new List<Employee>();

            using var stream = new StreamReader(file.OpenReadStream());
            string? headerLine = stream.ReadLine();
            if (headerLine == null)
            {
                return BadRequest("CSV file is empty or invalid.");
            }
            var headers = headerLine.Split(',');

            while (!stream.EndOfStream)
            {
                var line = stream.ReadLine()!;
                var values = line.Split(',');

                if (values.Length == headers.Length)
                {
                    var employee = new Employee
                    {
                        Name = values[0],
                        DateOfBirth = DateOnly.Parse(values[1], CultureInfo.InvariantCulture),
                        IsMarried = bool.Parse(values[2]),
                        Phone = values[3],
                        Salary = decimal.Parse(values[4])
                    };
                    employees.Add(employee);
                }
            }

            await _employeeService.AddEmployeesAsync(employees);

            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(int id, string name, DateOnly dateOfBirth, bool isMarried, string phone, decimal salary)
        {
            var employee = _employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = name;
            employee.DateOfBirth = dateOfBirth;
            employee.IsMarried = isMarried;
            employee.Phone = phone;
            employee.Salary = salary;

            await _employeeService.UpdateEmployeeAsync(employee);

            return Ok();
        }
    }
}
