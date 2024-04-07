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
    public class AlbumController : Controller
    {
        private readonly EFContext _context;

        public AlbumController(EFContext context)
        {
            _context = context;
        }

        // GET: Album
        public async Task<IActionResult> Index()
        {
            var eFContext = _context.albumok.Include(a => a.ReferencedFelhasznalo);
            return View(await eFContext.ToListAsync());
        }

        // GET: Album/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.albumok == null)
            {
                return NotFound();
            }

            var album = await _context.albumok
                .Include(a => a.ReferencedFelhasznalo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Album/Create
        public IActionResult Create()
        {
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "id");
            return View();
        }

        // POST: Album/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,cim,felhasz_id")] Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "id", album.felhasz_id);
            return View(album);
        }

        // GET: Album/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.albumok == null)
            {
                return NotFound();
            }

            var album = await _context.albumok.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "id", album.felhasz_id);
            return View(album);
        }

        // POST: Album/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,cim,felhasz_id")] Album album)
        {
            if (id != album.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.id))
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
            ViewData["felhasz_id"] = new SelectList(_context.felhasznalok, "id", "id", album.felhasz_id);
            return View(album);
        }

        // GET: Album/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.albumok == null)
            {
                return NotFound();
            }

            var album = await _context.albumok
                .Include(a => a.ReferencedFelhasznalo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Album/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.albumok == null)
            {
                return Problem("Entity set 'EFContext.albumok'  is null.");
            }
            var album = await _context.albumok.FindAsync(id);
            if (album != null)
            {
                _context.albumok.Remove(album);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
          return (_context.albumok?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
