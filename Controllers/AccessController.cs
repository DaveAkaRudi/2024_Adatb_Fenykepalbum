using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PhotoApp.Models;
using System.Security.Claims;
using PhotoApp.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace PhotoApp.Controllers
{
    public class AccessController : Controller
    {
        private readonly EFContext _context;
       
        public AccessController(EFContext context)
        {
            _context = context;
           
        }



        //  LOGIN METHODS

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



        //  REGIST METHODS

        public IActionResult Regist()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Regist(Felhasznalo felhasznalo)
        {
            // SqlCommand registCommand = new SqlCommand($"INSERT INTO felhasznalok (nev,email,jelszo,szuletes_datuma,role) VALUES ('{felhasznalo.nev}','{felhasznalo.email}','{felhasznalo.jelszo}',TO_DATE('{felhasznalo.szuletes_datuma}','YYYY-MM-DD'),'member');");

            // registCommand.ExecuteNonQuery();


            _context.felhasznalok.Add(felhasznalo);


            _context.SaveChanges();

            // ViewData["ValidateMessage"] = felhasznalo.nev;

            return View();
        }
    }
}
