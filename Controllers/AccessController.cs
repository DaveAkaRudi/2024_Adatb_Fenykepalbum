using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PhotoApp.Models;
using System.Security.Claims;
using PhotoApp.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System;

namespace PhotoApp.Controllers
{
    public class AccessController : Controller
    {
        private readonly EFContext _context;
       
        public AccessController(EFContext context)
        {
            _context = context;
           
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Felhasznalo felhasznalo)
        {
            
            var user = _context.felhasznalok.FirstOrDefault(u => u.nev == felhasznalo.nev && u.jelszo == felhasznalo.jelszo);
            
            if (user != null)
            {

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,felhasznalo.nev),
                    


                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = felhasznalo.rememberMe,
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }
           
            ViewData["ValidateMessage"] = "Invalid username or password";
            return View();
        }
    }
}
