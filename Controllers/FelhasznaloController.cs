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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PhotoApp.Controllers
{
    public class FelhasznaloController : Controller
    {
        private readonly EFContext _context;

        public FelhasznaloController(EFContext context)
        {
            _context = context;
        }

        // GET: Felhasznalo
        public async Task<IActionResult> Index()
        {
              return _context.felhasznalok != null ? 
                          View(await _context.felhasznalok.ToListAsync()) :
                          Problem("Entity set 'EFContext.felhasznalok'  is null.");
        }

        // GET: Felhasznalo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.felhasznalok == null)
            {
                return NotFound();
            }

            var felhasznalo = await _context.felhasznalok
                .FirstOrDefaultAsync(m => m.id == id);
            if (felhasznalo == null)
            {
                return NotFound();
            }

            return View(felhasznalo);
        }

        // GET: Felhasznalo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Felhasznalo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nev,email,jelszo,szuletes_datuma,role")] Felhasznalo felhasznalo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(felhasznalo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(felhasznalo);
        }

        // GET: Felhasznalo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.felhasznalok == null)
            {
                return NotFound();
            }

            var felhasznalo = await _context.felhasznalok.FindAsync(id);
            if (felhasznalo == null)
            {
                return NotFound();
            }
            return View(felhasznalo);
        }

        // POST: Felhasznalo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nev,email,jelszo,szuletes_datuma,role")] Felhasznalo felhasznalo)
        {
            if (id != felhasznalo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(felhasznalo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FelhasznaloExists(felhasznalo.id))
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
            return View(felhasznalo);
        }

        // GET: Felhasznalo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.felhasznalok == null)
            {
                return NotFound();
            }

            var felhasznalo = await _context.felhasznalok
                .FirstOrDefaultAsync(m => m.id == id);
            if (felhasznalo == null)
            {
                return NotFound();
            }

            return View(felhasznalo);
        }

        // POST: Felhasznalo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.felhasznalok == null)
            {
                return Problem("Entity set 'EFContext.felhasznalok'  is null.");
            }
            var felhasznalo = await _context.felhasznalok.FindAsync(id);
            if (felhasznalo != null)
            {
                _context.felhasznalok.Remove(felhasznalo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FelhasznaloExists(int id)
        {
          return (_context.felhasznalok?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
