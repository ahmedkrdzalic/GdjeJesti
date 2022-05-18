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
    public class JelaController : Controller
    {
        private readonly GdjeJestiContext _context;

        public JelaController(GdjeJestiContext context)
        {
            _context = context;
        }

        // GET: Jela
        public async Task<IActionResult> Index()
        {
              return View(await _context.Jelo.ToListAsync());
        }

        // GET: Jela/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jelo == null)
            {
                return NotFound();
            }

            var jelo = await _context.Jelo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jelo == null)
            {
                return NotFound();
            }

            return View(jelo);
        }

        // GET: Jela/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jela/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Cijena,RestoranId")] Jelo jelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jelo);
        }

        // GET: Jela/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jelo == null)
            {
                return NotFound();
            }

            var jelo = await _context.Jelo.FindAsync(id);
            if (jelo == null)
            {
                return NotFound();
            }
            return View(jelo);
        }

        // POST: Jela/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Cijena,RestoranId")] Jelo jelo)
        {
            if (id != jelo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JeloExists(jelo.Id))
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
            return View(jelo);
        }

        // GET: Jela/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jelo == null)
            {
                return NotFound();
            }

            var jelo = await _context.Jelo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jelo == null)
            {
                return NotFound();
            }

            return View(jelo);
        }

        // POST: Jela/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jelo == null)
            {
                return Problem("Entity set 'GdjeJestiContext.Jelo'  is null.");
            }
            var jelo = await _context.Jelo.FindAsync(id);
            if (jelo != null)
            {
                _context.Jelo.Remove(jelo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JeloExists(int id)
        {
          return _context.Jelo.Any(e => e.Id == id);
        }

        // GET: Jela/RandomJelo
        public async Task<IActionResult> RandomJelo()
        {
            var Jela = await _context.Jelo.ToListAsync();
            var random = new Random();
            int randomID = random.Next(1, Jela.Count); 
            //ne sviđa mi se jer uzima sve iz DB i onda trazi random item... 
            var jelo = Jela[randomID];
            var restoran = await _context.Restoran
                .FirstOrDefaultAsync(m => m.Id == jelo.Id);

            ViewData["restoran"] = restoran;

            if (jelo == null)
            {
                return NotFound();
            }
            if (restoran == null)
            {
                ViewData["restoran"] = "can not be found";
            }

            return View(jelo);
        } 
    }
}
