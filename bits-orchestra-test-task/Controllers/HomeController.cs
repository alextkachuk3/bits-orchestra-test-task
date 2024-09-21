using bits_orchestra_test_task.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace bits_orchestra_test_task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
            {
                bool skipHeader = true;

                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();

                    if (skipHeader)
                    {
                        skipHeader = false;
                        continue;
                    }

                    var values = line.Split(',');

                    var name = values[0];
                    var dob = DateTime.Parse(values[1], CultureInfo.InvariantCulture);
                    var married = bool.Parse(values[2]);
                    var phone = values[3];
                    var salary = decimal.Parse(values[4]);

                    Console.WriteLine($"Name: {name}, Date of Birth: {dob.ToShortDateString()}, Married: {married}, Phone: {phone}, Salary: {salary}");
                }
            }

            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
