using Microsoft.AspNetCore.Mvc;
using PhotoApp.Context;
using PhotoApp.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PhotoApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
       private readonly ILogger<HomeController> _logger; 
       private readonly EFContext _context; 
      

        public HomeController(ILogger<HomeController> logger, EFContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;

            ViewBag.nev = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Access");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}