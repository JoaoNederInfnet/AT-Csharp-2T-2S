using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AT_Csharp_2T_2S.Data;
using AT_Csharp_2T_2S.Models;

namespace AT_Csharp_2T_2S.Controllers
{
    public class PaisDestinoController : Controller
    {
        private readonly QueViagemDbContext _context;

        public PaisDestinoController(QueViagemDbContext context)
        {
            _context = context;
        }

        // GET: PaisDestino
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaisesDestino.ToListAsync());
        }

        // GET: PaisDestino/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paisDestino = await _context.PaisesDestino
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paisDestino == null)
            {
                return NotFound();
            }

            return View(paisDestino);
        }

        // GET: PaisDestino/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaisDestino/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Id,CriadoEm,AtualizadoEm")] PaisDestino paisDestino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paisDestino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paisDestino);
        }

        // GET: PaisDestino/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paisDestino = await _context.PaisesDestino.FindAsync(id);
            if (paisDestino == null)
            {
                return NotFound();
            }
            return View(paisDestino);
        }

        // POST: PaisDestino/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Nome,Id,CriadoEm,AtualizadoEm")] PaisDestino paisDestino)
        {
            if (id != paisDestino.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paisDestino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaisDestinoExists(paisDestino.Id))
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
            return View(paisDestino);
        }

        // GET: PaisDestino/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paisDestino = await _context.PaisesDestino
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paisDestino == null)
            {
                return NotFound();
            }

            return View(paisDestino);
        }

        // POST: PaisDestino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var paisDestino = await _context.PaisesDestino.FindAsync(id);
            if (paisDestino != null)
            {
                _context.PaisesDestino.Remove(paisDestino);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaisDestinoExists(long id)
        {
            return _context.PaisesDestino.Any(e => e.Id == id);
        }
    }
}
