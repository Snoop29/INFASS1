using System.Diagnostics;
using BOOTSTRAP.Models;
using Microsoft.AspNetCore.Mvc;
namespace BOOTSTRAP.Controllers
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
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            User user = new User(model.FullName, model.Email, model.Password);

            string insertQuery = user.GenerateInsertQuery("Users");
            string selectQuery = user.GenerateSelectQuery("Users");
            string updateQuery = user.GenerateUpdateQuery("Users");
            string deleteQuery = user.GenerateDeleteQuery("Users");

            return Json(new
            {
                insert = insertQuery,
                select = selectQuery,
                update = updateQuery,
                delete = deleteQuery
            });
        }
        [HttpPost]
        public IActionResult Login([FromBody] User model)
        {
            return Json(model);
        }
        [HttpPost]
        public IActionResult Select([FromBody] User model)
        {
            string query = model.GenerateSelectQuery("Users");
            return Json(new { query = query });
        }

        [HttpPost]
        public IActionResult Update([FromBody] User model)
        {
            string query = model.GenerateUpdateQuery("Users");
            return Json(new { query = query });
        }

        [HttpPost]
        public IActionResult Delete([FromBody] User model)
        {
            string query = model.GenerateDeleteQuery("Users");
            return Json(new { query = query });
        }
    }
}
