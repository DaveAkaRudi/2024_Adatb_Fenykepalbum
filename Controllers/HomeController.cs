using Microsoft.AspNetCore.Mvc;
using PhotoApp.Context;
using PhotoApp.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Newtonsoft.Json.Linq;

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

        public Felhasznalo userInfo(string nev){
            return _context.felhasznalok.FirstOrDefault(a => a.nev == nev);
        }
       
        public Felhasznalo loggedUserInfo(){
            var identity = (ClaimsIdentity)User.Identity;
            var nev = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userInfo(nev);
        }

        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var nev= identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ViewBag.nev = nev;
            ViewBag.utvonal = GetBestImage();

            var images = GetOthersImages(userInfo(nev).id);

            return View(images);

        }

        public static string GetBestImage()
        {
            string bestImage = null;

            using (var conn = new OracleConnection(@"User Id=LAJOS;Password=lajos;Data Source=localhost:1521/XEPDB1"))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText =@"begin :prm_Result := get_best_rated_image(); end;";

                    command.Parameters.Add(":prm_Result", OracleDbType.RefCursor, ParameterDirection.Output);

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bestImage = reader.GetString(0);
                        }
                    }
                }
            }

            return bestImage;
        }


        public static List<Kep> GetOthersImages(int id)
        {
            List<Kep> images = new List<Kep>();

            using (var conn = new OracleConnection(@"User Id=LAJOS;Password=lajos;Data Source=localhost:1521/XEPDB1"))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"begin :prm_Result := get_other_users_images(:prm_Argument); end;";

                    command.Parameters.Add(":prm_Result", OracleDbType.RefCursor, ParameterDirection.Output);
                    command.Parameters.Add(":prm_Argument", OracleDbType.Int64).Value = id;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Kep kep = new Kep();
                            kep.id = reader.GetInt32(0);
                            kep.cim = reader.GetString(1);
                            kep.orszag_id = reader.GetInt32(2);
                            kep.ertekeles = reader.GetDouble(3);
                            kep.album_id = reader.GetInt32(4);
                            kep.felhasz_id = reader.GetInt32(5);
                            kep.fenykeputvonal = reader.GetString(6);
                            images.Add(kep);
                        }
                    }
                }
            }

            return images;
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