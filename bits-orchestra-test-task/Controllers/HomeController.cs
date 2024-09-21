using bits_orchestra_test_task.Models;
using bits_orchestra_test_task.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace bits_orchestra_test_task.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IEmployeeService employeeService) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IEmployeeService _employeeService = employeeService;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("upload")]
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
                        DateOfBirth = DateTime.Parse(values[1], CultureInfo.InvariantCulture),
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
