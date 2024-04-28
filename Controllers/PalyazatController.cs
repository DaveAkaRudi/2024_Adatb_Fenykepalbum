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
    public class PalyazatController : Controller
    {
        private readonly EFContext _context;

        public PalyazatController(EFContext context)
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

        // GET: Palyazat
        public async Task<IActionResult> Index()
        {
            var eFContext = _context.palyazatok.Include(p => p.ReferencedKategoria);
            return View(await eFContext.ToListAsync());
        }

        // GET: Palyazat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.palyazatok == null)
            {
                return NotFound();
            }

            var palyazat = await _context.palyazatok
                .Include(p => p.ReferencedKategoria)
                .FirstOrDefaultAsync(m => m.id == id);
            if (palyazat == null)
            {
                return NotFound();
            }

            return View(palyazat);
        }

        // GET: Palyazat/Create
        public IActionResult Create()
        {
            ViewData["kategoria_id"] = new SelectList(_context.kategoriak, "id", "id");
            return View();
        }

        // POST: Palyazat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nev,hatarido,leiras,nyertes,kategoria_id")] Palyazat palyazat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(palyazat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["kategoria_id"] = new SelectList(_context.kategoriak, "id", "id", palyazat.kategoria_id);
            return View(palyazat);
        }

        // GET: Palyazat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.palyazatok == null)
            {
                return NotFound();
            }

            var palyazat = await _context.palyazatok.FindAsync(id);
            if (palyazat == null)
            {
                return NotFound();
            }
            ViewData["kategoria_id"] = new SelectList(_context.kategoriak, "id", "id", palyazat.kategoria_id);
            return View(palyazat);
        }

        // POST: Palyazat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nev,hatarido,leiras,nyertes,kategoria_id")] Palyazat palyazat)
        {
            if (id != palyazat.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(palyazat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PalyazatExists(palyazat.id))
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
            ViewData["kategoria_id"] = new SelectList(_context.kategoriak, "id", "id", palyazat.kategoria_id);
            return View(palyazat);
        }

        // GET: Palyazat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.palyazatok == null)
            {
                return NotFound();
            }

            var palyazat = await _context.palyazatok
                .Include(p => p.ReferencedKategoria)
                .FirstOrDefaultAsync(m => m.id == id);
            if (palyazat == null)
            {
                return NotFound();
            }

            return View(palyazat);
        }

        // POST: Palyazat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.palyazatok == null)
            {
                return Problem("Entity set 'EFContext.palyazatok'  is null.");
            }
            var palyazat = await _context.palyazatok.FindAsync(id);
            if (palyazat != null)
            {
                _context.palyazatok.Remove(palyazat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PalyazatExists(int id)
        {
          return (_context.palyazatok?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
