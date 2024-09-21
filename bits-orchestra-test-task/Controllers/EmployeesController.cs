using bits_orchestra_test_task.Models;
using bits_orchestra_test_task.Services;
using Microsoft.AspNetCore.Mvc;
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
            var errorMessages = new List<string>();

            var stream = new StreamReader(file.OpenReadStream());

            string[] requiredHeaders = ["Name", "Date of birth", "Married", "Phone", "Salary"];

            string? line = stream.ReadLine();

            if (line == null || !requiredHeaders.All(header => line.Contains(header)))
            {
                stream.BaseStream.Position = 0;
                stream.DiscardBufferedData();
            }

            while (!stream.EndOfStream)
            {
                line = stream.ReadLine();
                if (string.IsNullOrWhiteSpace(line) || line == ",,,,")
                {
                    continue;
                }

                var values = line.Split(',');

                if (values.Length == requiredHeaders.Length)
                {
                    try
                    {
                        var employee = new Employee
                        {
                            Name = values[0],
                            DateOfBirth = ParseDateOfBirth(values[1]),
                            IsMarried = ParseIsMarried(values[2]),
                            Phone = values[3],
                            Salary = ParseSalary(values[4])
                        };

                        employees.Add(employee);
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add($"Error processing line: '{line}' - {ex.Message}");
                    }
                }
                else
                {
                    errorMessages.Add($"Row has an unexpected number of columns: '{line}'");
                }
            }

            if (errorMessages.Count != 0)
            {
                return BadRequest(new { Errors = errorMessages });
            }

            await _employeeService.AddEmployeesAsync(employees);

            return Redirect("/Employees");
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

        private static DateOnly ParseDateOfBirth(string value)
        {
            if (DateOnly.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dob))
            {
                return dob;
            }
            throw new Exception($"Invalid date format for DateOfBirth: {value}");
        }

        private static bool ParseIsMarried(string value)
        {
            if (bool.TryParse(value, out var isMarried))
            {
                return isMarried;
            }
            throw new Exception($"Invalid boolean format for IsMarried: {value}");
        }

        private static decimal ParseSalary(string value)
        {
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var salary))
            {
                return salary;
            }
            throw new Exception($"Invalid salary format for Salary: {value}");
        }
    }
}
