using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Nuova_cartella.Controllers
{
    public class RegistrazioneController : Controller
    {
        private readonly dbContext _context;

        public RegistrazioneController(dbContext context)
        {
            _context = context;
        }

        // GET: Registrazione
        public async Task<IActionResult> Index()
        {
            return View(await _context.Registrazioni.ToListAsync());
        }

        // GET: Registrazione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrazione = await _context.Registrazioni
                .FirstOrDefaultAsync(m => m.RegistrazioneId == id);
            if (registrazione == null)
            {
                return NotFound();
            }

            return View(registrazione);
        }

        // GET: Registrazione/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Registrazione/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrazioneId,Data,nome,cognome,email,password,sesso,ruolo")] Registrazione registrazione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registrazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registrazione);
        }

        // GET: Registrazione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrazione = await _context.Registrazioni.FindAsync(id);
            if (registrazione == null)
            {
                return NotFound();
            }
            return View(registrazione);
        }

        // POST: Registrazione/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrazioneId,Data,nome,cognome,email,password,sesso,ruolo")] Registrazione registrazione)
        {
            if (id != registrazione.RegistrazioneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registrazione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrazioneExists(registrazione.RegistrazioneId))
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
            return View(registrazione);
        }

        // GET: Registrazione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registrazione = await _context.Registrazioni
                .FirstOrDefaultAsync(m => m.RegistrazioneId == id);
            if (registrazione == null)
            {
                return NotFound();
            }

            return View(registrazione);
        }

        // POST: Registrazione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registrazione = await _context.Registrazioni.FindAsync(id);
            if (registrazione != null)
            {
                _context.Registrazioni.Remove(registrazione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrazioneExists(int id)
        {
            return _context.Registrazioni.Any(e => e.RegistrazioneId == id);
        }
    }
}
