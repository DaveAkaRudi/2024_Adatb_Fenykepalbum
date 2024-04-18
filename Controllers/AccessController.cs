using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PhotoApp.Models;
using System.Security.Claims;
using PhotoApp.Context;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

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
			var user = await _context.felhasznalok
				.FirstOrDefaultAsync(u => u.nev == felhasznalo.nev);

			if (user != null)
			{

				string hashedPassword = HashPassword(felhasznalo.jelszo);

				if (hashedPassword == user.jelszo)
				{
					List<Claim> claims = new List<Claim>()
					{
						new Claim(ClaimTypes.NameIdentifier, felhasznalo.nev),
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
            try
            {
            var usernameUsed = _context.felhasznalok.FirstOrDefault(u => u.nev == felhasznalo.nev);
            var emailUsed = _context.felhasznalok.FirstOrDefault(u => u.email == felhasznalo.email);

            if ((emailUsed != null) && (usernameUsed != null))
            {
                ViewData["ValidateMessage"] = "Registration failed! Email and Username already used.";
                return View();
            }
            
            if((emailUsed==null) && (usernameUsed!=null)){
                ViewData["ValidateMessage"] = "Registration failed! Username already used.";
                return View();
            }

            if ((emailUsed != null) && (usernameUsed == null))
            {
                ViewData["ValidateMessage"] = "Registration failed! Email already used.";
                return View();
            }

            var password = Request.Form["password"];
            var passwordCheck = Request.Form["password_check"];
            
            if((password != passwordCheck)){
                ViewData["ValidateMessage"] = "Registration failed! The password not matched.";
                return View();
            }

			string hashedPassword = HashPassword(password);
			felhasznalo.jelszo = hashedPassword;

            
            _context.felhasznalok.Add(felhasznalo);

            _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return View();
            }
            TempData["SuccessMessage"] = "Sikeres regisztráció";

            return RedirectToAction("Index", "Home");
        

        }

		private string HashPassword(string password)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				StringBuilder builder = new StringBuilder();
				foreach (byte b in hashedBytes)
				{
					builder.Append(b.ToString("x2"));
				}
				return builder.ToString();
			}
		}
	}
}
