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
        public IActionResult Connected()
        {
            ViewBag.IsConnected = CheckDatabaseConnection();
            return View();
        }

        private bool CheckDatabaseConnection()
        {
            try
            {
                using (var dbCon = new EFContext())
                {
                    var data = dbCon.orszagok.FirstOrDefault();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
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
            var emailUsed = _context.felhasznalok.FirstOrDefault(u => u.nev == felhasznalo.nev);
            var usernameUsed = _context.felhasznalok.FirstOrDefault(u => u.email == felhasznalo.email);

            if((emailUsed!=null) && (usernameUsed==null)){

                ViewData["ValidateMessage"] = "Registration failed! Email already used.";

                return View();
            }

            if((emailUsed==null) && (usernameUsed!=null)){

                ViewData["ValidateMessage"] = "Registration failed! Username already used.";

                return View();
            }

            if((emailUsed!=null) && (usernameUsed!=null)){

                ViewData["ValidateMessage"] = "Registration failed! Email and Username already used.";

                return View();
            }

            var password = Request.Form["password"];
            var passwordCheck = Request.Form["password_check"];
            
            if((password != passwordCheck)){

                ViewData["ValidateMessage"] = "Registration failed! The password not matched.";

                return View();

            }

            felhasznalo.jelszo=password;


            _context.felhasznalok.Add(felhasznalo);

            _context.SaveChanges();

            ViewData["ValidateMessage"] = "Registration successfully! Now you can login to your account.";

            return RedirectToAction("Index", "Home");   


        }
    }
}
