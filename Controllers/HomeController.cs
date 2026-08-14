using System.Diagnostics;
using BOOTSTRAP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BOOTSTRAP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
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
            string selectQuery = user.GenerateSelectQuery("Users", "Id");
            string updateQuery = user.GenerateUpdateQuery("Users", "Id");
            string deleteQuery = user.GenerateDeleteQuery("Users", "Id");

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

            return Json(new
            {
                insert = insertQuery,
                select = selectQuery,
                update = updateQuery,
                delete = deleteQuery
            });
        }
    }
}