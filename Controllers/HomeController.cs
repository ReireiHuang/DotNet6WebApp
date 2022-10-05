using DotNet6WebApp.Business;
using DotNet6WebApp.DBModels;
using DotNet6WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DotNet6WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SQL_TestContext _context;

        public HomeController(ILogger<HomeController> logger, SQL_TestContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                login.Message = "Please Confirm column is filled";
                return View();
            }

            Users User = _context.Users.Where(x => x.Acc == login.Acc).SingleOrDefault();
            if (User == null)
            {
                login.Message = "Acc Not Exist";
                return View(login);
            }

            if (User.Pass != login.Pass)
            {
                login.Message = "Pass is not True";
                return View(login);
            }

            return RedirectToAction("Index", "Home");
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
    }
}