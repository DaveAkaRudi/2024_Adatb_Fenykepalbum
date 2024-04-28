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
    public class OrszagController : Controller
    {
        private readonly EFContext _context;

        public OrszagController(EFContext context)
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

        // GET: Orszag
        public async Task<IActionResult> Index()
        {
              return _context.orszagok != null ? 
                          View(await _context.orszagok.ToListAsync()) :
                          Problem("Entity set 'EFContext.orszagok'  is null.");
        }

        // GET: Orszag/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.orszagok == null)
            {
                return NotFound();
            }

            var orszag = await _context.orszagok
                .FirstOrDefaultAsync(m => m.id == id);
            if (orszag == null)
            {
                return NotFound();
            }

            return View(orszag);
        }

        // GET: Orszag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orszag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nev,rovidites")] Orszag orszag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orszag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orszag);
        }

        // GET: Orszag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.orszagok == null)
            {
                return NotFound();
            }

            var orszag = await _context.orszagok.FindAsync(id);
            if (orszag == null)
            {
                return NotFound();
            }
            return View(orszag);
        }

        // POST: Orszag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nev,rovidites")] Orszag orszag)
        {
            if (id != orszag.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orszag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrszagExists(orszag.id))
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
            return View(orszag);
        }

        // GET: Orszag/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.orszagok == null)
            {
                return NotFound();
            }

            var orszag = await _context.orszagok
                .FirstOrDefaultAsync(m => m.id == id);
            if (orszag == null)
            {
                return NotFound();
            }

            return View(orszag);
        }

        // POST: Orszag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.orszagok == null)
            {
                return Problem("Entity set 'EFContext.orszagok'  is null.");
            }
            var orszag = await _context.orszagok.FindAsync(id);
            if (orszag != null)
            {
                _context.orszagok.Remove(orszag);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrszagExists(int id)
        {
          return (_context.orszagok?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
