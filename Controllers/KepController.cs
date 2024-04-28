using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoApp.Context;
using PhotoApp.Models;
using System.Security.Claims;

namespace PhotoApp.Controllers
{
    public class KepController : Controller
    {
        private readonly EFContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public KepController(EFContext context, IWebHostEnvironment env)
        {
            _context = context;
            _webHostEnvironment = env;
        }

        public Felhasznalo userInfo(string nev){
            return _context.felhasznalok.FirstOrDefault(a => a.nev == nev);
        }

        public Felhasznalo loggedUserInfo(){
            var identity = (ClaimsIdentity)User.Identity;
            var nev = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userInfo(nev);
        }

        // GET: Kep
        public async Task<IActionResult> Index()
        {
            var eFContext = _context.kepek.Include(k => k.ReferencedAlbum).Include(k => k.ReferencedFelhasznalo).Include(k => k.ReferencedOrszag);
            return View(await eFContext.ToListAsync());
        }

        // GET: Kep/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.kepek == null)
            {
                return NotFound();
            }

            var kep = await _context.kepek
                .Include(k => k.ReferencedAlbum)
                .Include(k => k.ReferencedFelhasznalo)
                .Include(k => k.ReferencedOrszag)
                .FirstOrDefaultAsync(m => m.id == id);
            if (kep == null)
            {
                return NotFound();
            }

            return View(kep);
        }

        // GET: Kep/Create
        public IActionResult Create()
        {
            ViewData["album_id"] = new SelectList(_context.albumok, "id", "id");
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "id");
            ViewData["orszag_id"] = new SelectList(_context.orszagok, "id", "id");
            return View();
        }

        // POST: Kep/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Kep kep)
        {
            string filename = "";
            try
            {
                if (kep.ImageFile == null || kep.ImageFile.Length == 0)
                {
                    // Handle the case where no image is selected
                    ModelState.AddModelError("ImageFile", "Please select an image.");
                    ViewData["album_id"] = new SelectList(_context.albumok, "id", "cim", kep.album_id);
                    ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "nev", kep.felhasz_id);
                    ViewData["orszag_id"] = new SelectList(_context.felhasznalok, "id", "nev", kep.orszag_id);
                    return View(kep); // Return to the view with an error message
                }


                filename = Guid.NewGuid() + Path.GetExtension(kep.ImageFile.FileName);
                string path = Path.GetFullPath(Path.Combine(_webHostEnvironment.WebRootPath, "images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await kep.ImageFile.CopyToAsync(filestream);
                }
            }
            catch (Exception)
            {
                throw;
            }


            kep.fenykeputvonal = filename;


            if (ModelState.IsValid)
            {
                _context.Add(kep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["album_id"] = new SelectList(_context.albumok, "id", "cim", kep.album_id);
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "nev", kep.felhasz_id);
            ViewData["orszag_id"] = new SelectList(_context.felhasznalok, "id", "nev", kep.orszag_id);
            return View(kep);
        }

        // GET: Kep/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.kepek == null)
            {
                return NotFound();
            }

            var kep = await _context.kepek.FindAsync(id);
            if (kep == null)
            {
                return NotFound();
            }
            ViewData["album_id"] = new SelectList(_context.albumok, "id", "id", kep.album_id);
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "id", kep.felhasz_id);
            ViewData["orszag_id"] = new SelectList(_context.orszagok, "id", "id", kep.orszag_id);
            return View(kep);
        }

        // POST: Kep/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kep kep)
        {
            if (id != kep.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (kep.ImageFile == null || kep.ImageFile.Length == 0)
                    {

                        ModelState.AddModelError("ImageFile", "Please select an image.");
                        ViewData["album_id"] = new SelectList(_context.albumok, "id", "cim", kep.album_id);
                        ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "nev", kep.felhasz_id);
                        ViewData["orszag_id"] = new SelectList(_context.felhasznalok, "id", "nev", kep.orszag_id);
                        return View(kep); // Return to the view with an error message
                    }


                    var existingPet = await _context.kepek
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.id == id);

                    string existingImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", existingPet.fenykeputvonal);

                    if (System.IO.File.Exists(existingImagePath))
                    {
                        System.IO.File.Delete(existingImagePath);
                    }

                    string newFilename = Guid.NewGuid() + Path.GetExtension(kep.ImageFile.FileName);
                    string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", newFilename);

                    using (var fileStream = new FileStream(newPath, FileMode.Create))
                    {
                        await kep.ImageFile.CopyToAsync(fileStream);
                    }


                    kep.fenykeputvonal = newFilename;

                    _context.Update(kep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KepExists(kep.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["album_id"] = new SelectList(_context.albumok, "id", "cim", kep.album_id);
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "nev", kep.felhasz_id);
            ViewData["orszag_id"] = new SelectList(_context.felhasznalok, "id", "nev", kep.orszag_id);
            return View(kep);
        }

        // GET: Kep/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.kepek == null)
            {
                return NotFound();
            }

            var kep = await _context.kepek
                .Include(k => k.ReferencedAlbum)
                .Include(k => k.ReferencedFelhasznalo)
                .Include(k => k.ReferencedOrszag)
                .FirstOrDefaultAsync(m => m.id == id);
            if (kep == null)
            {
                return NotFound();
            }

            return View(kep);
        }

        // POST: Kep/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.kepek == null)
            {
                return Problem("Entity set 'EFContext.kepek'  is null.");
            }
            var kep = await _context.kepek.FindAsync(id);
            if (kep != null)
            {
                _context.kepek.Remove(kep);
            }

            string imageFileName = kep.fenykeputvonal;


            _context.kepek.Remove(kep);
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(imageFileName))
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageFileName);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KepExists(int id)
        {
          return (_context.kepek?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
