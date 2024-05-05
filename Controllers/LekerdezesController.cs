﻿using Microsoft.AspNetCore.Mvc;
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
            //Átlagos értékelés kategóriánként
            var categoryRatings = from kk in _context.KepKategoria
                                   join kp in _context.kepek on kk.kep_id equals kp.id
                                   join k in _context.kategoriak on kk.kategoria_id equals k.id
                                   group kp by k.nev into g
                                   select new CategoryRatingViewModel
                                   {
                                       KategoriaNev = g.Key,
                                       AtlagErtekeles = g.Average(kp => kp.ertekeles)
                                   };

            //Felhasználóknak hány kommentjük van
            var userCommentCounts = from ko in _context.kommentek
                                     join f in _context.felhasznalok on ko.felhasz_id equals f.id
                                     group ko by f.nev into g
                                     orderby g.Count() descending
                                     select new UserCommentCountViewModel
                                     {
                                         FelhasznaloNev = g.Key,
                                         KommentSzam = g.Count()
                                     };
            //Országonként hány képfeltöltés
            var telepulesPhotoCounts = from k in _context.kepek
                                       join o in _context.orszagok
                                       on k.orszag_id equals o.id
                                       group k by o.nev into t
                                       orderby t.Count() descending
                                       select new TelepulesPhotoCountViewModel {
                                           Telepules = t.Key,
                                           Darab = t.Count()
                                       };

            //Kategóriákként hány képfeltöltés
            var categoryImageCounts = from kk in _context.KepKategoria
                                      join k in _context.kategoriak on kk.kategoria_id equals k.id
                                      group kk by k.nev into g
                                      orderby g.Count() descending
                                      select new CategoryImageCountViewModel
                                      {
                                            KategoriaNev = g.Key,
                                            KepSzam = g.Count()
                                      };

            //Felhasználóknak hány képfeltöltésük van
            var userImageCounts =  from kp in _context.kepek
                                   join f in _context.felhasznalok on kp.felhasz_id equals f.id
                                   group kp by f.nev into g
                                   select new UserImageCountViewModel
                                   {
                                       FelhasznaloNev = g.Key,
                                       KepSzam = g.Count()
                                   };
            //Albumok hány kategoriával rendelkeznek
            var kategoriakAlbumonkent = from a in _context.albumok
                                        join kp in _context.kepek on a.id equals kp.album_id
                                        join kk in _context.KepKategoria on kp.id equals kk.kep_id
                                        group kk by a.cim into g
                                        select new KategoriakAlbumonkent
                                        {
                                            AlbumCime = g.Key,
                                            KategoriakSzama = g.Select(kk => kk.kategoria_id).Distinct().Count()
                                        };
            //Felhasználók átlagos értékelése
            var atlagosErtekelesFelhasznalonkent = from f in _context.felhasznalok
                                                   join kp in _context.kepek on f.id equals kp.felhasz_id
                                                   group kp by f.nev into g
                                                   select new AtlagosErtekelesFelhasznalonkent
                                                   {
                                                       FelhasznaloNeve = g.Key,
                                                       AtlagosErtekeles = g.Average(kp => kp.ertekeles)
                                                   };
         
            //Országonkénti átlagos értékelés
            var countryRatings = from k in _context.kepek
                                 join o in _context.orszagok on k.orszag_id equals o.id
                                 group k by o.nev into g
                                 select new CountryRatingViewModel
                                 {
                                     CountryName = g.Key,
                                     AverageRating = g.Average(k => k.ertekeles)
                                 };

            //Albumonkénti átlagos értékelés
            var albumAverageRatings = from a in _context.albumok
                                      join k in _context.kepek on a.id equals k.album_id
                                      group k by a.cim into g
                                      select new AlbumRatingViewModel
                                      {
                                          AlbumTitle = g.Key,
                                          AverageRating = g.Average(k => k.ertekeles)
                                      };

            //Kategóriánként legjobb értékelés
            var maxCategoryRatings = from kk in _context.KepKategoria
                                     join k in _context.kepek on kk.kep_id equals k.id
                                     join cat in _context.kategoriak on kk.kategoria_id equals cat.id
                                     group k by cat.nev into g
                                     select new MaxCategoryRatingViewModel
                                     {
                                         CategoryName = g.Key,
                                         MaxRating = g.Max(k => k.ertekeles)
                                     };


            var viewModel = new AllQueryResultsViewModel
            {
                CategoryRatings = categoryRatings,
                UserCommentCounts = userCommentCounts,
                TelepulesPhotoCounts = telepulesPhotoCounts,
                CategoryImageCounts = categoryImageCounts,
                UserImageCounts = userImageCounts,
                KategoriakAlbumonkent =kategoriakAlbumonkent,
                AtlagosErtekelesFelhasznalonkent = atlagosErtekelesFelhasznalonkent,
                AlbumRatingViewModel = albumAverageRatings,
                CountryRatingViewModel = countryRatings,
                MaxCategoryRatingViewModel = maxCategoryRatings,
            };

            return View(viewModel);
        }

       
    }
}
