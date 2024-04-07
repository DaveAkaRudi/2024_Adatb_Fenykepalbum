using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhotoApp.Context;
using PhotoApp.Models;

namespace PhotoApp.Controllers
{
    public class KommentController : Controller
    {
        private readonly EFContext _context;

        public KommentController(EFContext context)
        {
            _context = context;
        }

        // GET: Komment
        public async Task<IActionResult> Index()
        {
            var eFContext = _context.kommentek.Include(k => k.ReferencedFelhasznalo).Include(k => k.ReferencedKep);
            return View(await eFContext.ToListAsync());
        }

        // GET: Komment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.kommentek == null)
            {
                return NotFound();
            }

            var komment = await _context.kommentek
                .Include(k => k.ReferencedFelhasznalo)
                .Include(k => k.ReferencedKep)
                .FirstOrDefaultAsync(m => m.id == id);
            if (komment == null)
            {
                return NotFound();
            }

            return View(komment);
        }

        // GET: Komment/Create
        public IActionResult Create()
        {
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "id");
            ViewData["kep_id"] = new SelectList(_context.kepek, "id", "id");
            return View();
        }

        // POST: Komment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,felhasz_id,kep_id,megjegyzes")] Komment komment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(komment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "id", komment.felhasz_id);
            ViewData["kep_id"] = new SelectList(_context.kepek, "id", "id", komment.kep_id);
            return View(komment);
        }

        // GET: Komment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.kommentek == null)
            {
                return NotFound();
            }

            var komment = await _context.kommentek.FindAsync(id);
            if (komment == null)
            {
                return NotFound();
            }
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "id", komment.felhasz_id);
            ViewData["kep_id"] = new SelectList(_context.kepek, "id", "id", komment.kep_id);
            return View(komment);
        }

        // POST: Komment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,felhasz_id,kep_id,megjegyzes")] Komment komment)
        {
            if (id != komment.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(komment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KommentExists(komment.id))
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
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "id", komment.felhasz_id);
            ViewData["kep_id"] = new SelectList(_context.kepek, "id", "id", komment.kep_id);
            return View(komment);
        }

        // GET: Komment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.kommentek == null)
            {
                return NotFound();
            }

            var komment = await _context.kommentek
                .Include(k => k.ReferencedFelhasznalo)
                .Include(k => k.ReferencedKep)
                .FirstOrDefaultAsync(m => m.id == id);
            if (komment == null)
            {
                return NotFound();
            }

            return View(komment);
        }

        // POST: Komment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.kommentek == null)
            {
                return Problem("Entity set 'EFContext.kommentek'  is null.");
            }
            var komment = await _context.kommentek.FindAsync(id);
            if (komment != null)
            {
                _context.kommentek.Remove(komment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KommentExists(int id)
        {
          return (_context.kommentek?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
