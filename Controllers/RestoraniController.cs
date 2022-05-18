using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GdjeJesti.Models;

namespace GdjeJesti.Controllers
{
    public class RestoraniController : Controller
    {
        private readonly GdjeJestiContext _context;

        public RestoraniController(GdjeJestiContext context)
        {
            _context = context;
        }

        // GET: Restorani
        public async Task<IActionResult> Index()
        {
              return _context.Restoran != null ? 
                          View(await _context.Restoran.ToListAsync()) :
                          Problem("Entity set 'GdjeJestiContext.Restoran'  is null.");
        }

        // GET: Restorani/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Restoran == null)
            {
                return NotFound();
            }

            var restoran = await _context.Restoran
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restoran == null)
            {
                return NotFound();
            }

            return View(restoran);
        }

        // GET: Restorani/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restorani/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Adresa,Telefon")] Restoran restoran)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restoran);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restoran);
        }

        // GET: Restorani/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restoran == null)
            {
                return NotFound();
            }

            var restoran = await _context.Restoran.FindAsync(id);
            if (restoran == null)
            {
                return NotFound();
            }
            return View(restoran);
        }

        // POST: Restorani/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Adresa,Telefon")] Restoran restoran)
        {
            if (id != restoran.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restoran);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestoranExists(restoran.Id))
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
            return View(restoran);
        }

        // GET: Restorani/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Restoran == null)
            {
                return NotFound();
            }

            var restoran = await _context.Restoran
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restoran == null)
            {
                return NotFound();
            }

            return View(restoran);
        }

        // POST: Restorani/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Restoran == null)
            {
                return Problem("Entity set 'GdjeJestiContext.Restoran'  is null.");
            }
            var restoran = await _context.Restoran.FindAsync(id);
            if (restoran != null)
            {
                _context.Restoran.Remove(restoran);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestoranExists(int id)
        {
          return (_context.Restoran?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
