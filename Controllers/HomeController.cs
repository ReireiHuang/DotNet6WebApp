using DotNet6WebApp.Service;
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
        private readonly GoogleReCaptchaService _captchaService;

        public HomeController(ILogger<HomeController> logger, SQL_TestContext context, GoogleReCaptchaService captchaService)
        {
            _logger = logger;
            _context = context;
            _captchaService = captchaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            LoginViewModel loginModel = new LoginViewModel() { GoogleSiteKey = _context.DbconfigSetting.Where(x => x.Id == 1).FirstOrDefault().Value };
            return View(loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            //backend verify
            if (!ModelState.IsValid)
            {
                login.Message = "Please Confirm column is filled";
                return View(login);
            }

            if (Request.Form["g-recaptcha-response"] == "")
            {
                login.Message = "Please Confirm Not Robot";
                return View(login);
            }

            var captchaResult = await _captchaService.VerifyToken(Request.Form["g-recaptcha-response"].ToString());

            if (!captchaResult)
            {
                login.Message = "CaptchAuth Fail";
                return View(login);
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