using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoApp.Context;
using PhotoApp.Models;
using System.Security.Claims;


namespace PhotoApp.Controllers
{
    public class KepKategoriaController : Controller
    {
        private readonly EFContext _context;

        public KepKategoriaController(EFContext context)
        {
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

        // GET: KepKategoria
        public async Task<IActionResult> Index()
        {
            var eFContext = _context.KepKategoria.Include(k => k.ReferencedKategoria).Include(k => k.ReferencedKep);
            return View(await eFContext.ToListAsync());
        }

        // GET: KepKategoria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KepKategoria == null)
            {
                return NotFound();
            }

            var kepKategoria = await _context.KepKategoria
                .Include(k => k.ReferencedKategoria)
                .Include(k => k.ReferencedKep)
                .FirstOrDefaultAsync(m => m.id == id);
            if (kepKategoria == null)
            {
                return NotFound();
            }

            return View(kepKategoria);
        }

        // GET: KepKategoria/Create
        public IActionResult Create()
        {
            ViewData["kategoria_id"] = new SelectList(_context.kategoriak, "id", "id");
            ViewData["kep_id"] = new SelectList(_context.kepek, "id", "id");
            return View();
        }

        // POST: KepKategoria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,kategoria_id,kep_id")] KepKategoria kepKategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kepKategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["kategoria_id"] = new SelectList(_context.kategoriak, "id", "id", kepKategoria.kategoria_id);
            ViewData["kep_id"] = new SelectList(_context.kepek, "id", "id", kepKategoria.kep_id);
            return View(kepKategoria);
        }

        // GET: KepKategoria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KepKategoria == null)
            {
                return NotFound();
            }

            var kepKategoria = await _context.KepKategoria.FindAsync(id);
            if (kepKategoria == null)
            {
                return NotFound();
            }
            ViewData["kategoria_id"] = new SelectList(_context.kategoriak, "id", "id", kepKategoria.kategoria_id);
            ViewData["kep_id"] = new SelectList(_context.kepek, "id", "id", kepKategoria.kep_id);
            return View(kepKategoria);
        }

        // POST: KepKategoria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,kategoria_id,kep_id")] KepKategoria kepKategoria)
        {
            if (id != kepKategoria.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kepKategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KepKategoriaExists(kepKategoria.id))
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
            ViewData["kategoria_id"] = new SelectList(_context.kategoriak, "id", "id", kepKategoria.kategoria_id);
            ViewData["kep_id"] = new SelectList(_context.kepek, "id", "id", kepKategoria.kep_id);
            return View(kepKategoria);
        }

        // GET: KepKategoria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KepKategoria == null)
            {
                return NotFound();
            }

            var kepKategoria = await _context.KepKategoria
                .Include(k => k.ReferencedKategoria)
                .Include(k => k.ReferencedKep)
                .FirstOrDefaultAsync(m => m.id == id);
            if (kepKategoria == null)
            {
                return NotFound();
            }

            return View(kepKategoria);
        }

        // POST: KepKategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KepKategoria == null)
            {
                return Problem("Entity set 'EFContext.KepKategoria'  is null.");
            }
            var kepKategoria = await _context.KepKategoria.FindAsync(id);
            if (kepKategoria != null)
            {
                _context.KepKategoria.Remove(kepKategoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KepKategoriaExists(int id)
        {
          return (_context.KepKategoria?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
