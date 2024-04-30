using Microsoft.AspNetCore.Mvc;
using PhotoApp.Context;
using PhotoApp.Models;

namespace PhotoApp.Controllers
{
    public class LekerdezesController : Controller
    {
       
        private readonly EFContext _context;

        public LekerdezesController(EFContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categoryRatings = (from kk in _context.KepKategoria
                                   join kp in _context.kepek on kk.kep_id equals kp.id
                                   join k in _context.kategoriak on kk.kategoria_id equals k.id
                                   group kp by k.nev into g
                                   select new CategoryRatingViewModel
                                   {
                                       KategoriaNev = g.Key,
                                       AtlagErtekeles = g.Average(kp => kp.ertekeles)
                                   }).ToList();

            var userCommentCounts = (from ko in _context.kommentek
                                     join f in _context.felhasznalok on ko.felhasz_id equals f.id
                                     group ko by f.nev into g
                                     orderby g.Count() descending
                                     select new UserCommentCountViewModel
                                     {
                                         FelhasznaloNev = g.Key,
                                         KommentSzam = g.Count()
                                     }).ToList();

            var telepulesPhotoCounts = (
                from k in _context.kepek
                join o in _context.orszagok
                on k.orszag_id equals o.id
                group k by o.nev into t
                orderby t.Count() descending
                select new TelepulesPhotoCountViewModel {
                    Telepules = t.Key,
                    Darab = t.Count()
                }
            ).ToList();

            var viewModel = new AllQueryResultsViewModel
            {
                CategoryRatings = categoryRatings,
                UserCommentCounts = userCommentCounts,
                TelepulesPhotoCounts = telepulesPhotoCounts
                // Set other properties of the view model as needed
            };

            return View(viewModel);
        }

       
    }
}
