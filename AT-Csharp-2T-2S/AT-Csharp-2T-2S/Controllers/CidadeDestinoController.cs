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
    public class CidadeDestinoController : Controller
    {
        private readonly QueViagemDbContext _context;

        public CidadeDestinoController(QueViagemDbContext context)
        {
            _context = context;
        }

        // GET: CidadeDestino
        public async Task<IActionResult> Index()
        {
            var queViagemDbContext = _context.CidadesDestino.Include(c => c.PaisDestino);
            return View(await queViagemDbContext.ToListAsync());
        }

        // GET: CidadeDestino/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidadeDestino = await _context.CidadesDestino
                .Include(c => c.PaisDestino)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cidadeDestino == null)
            {
                return NotFound();
            }

            return View(cidadeDestino);
        }

        // GET: CidadeDestino/Create
        public IActionResult Create()
        {
            ViewData["PaisDestinoId"] = new SelectList(_context.PaisesDestino, "Id", "Id");
            return View();
        }

        // POST: CidadeDestino/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,PaisDestinoId,Id,CriadoEm,AtualizadoEm")] CidadeDestino cidadeDestino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cidadeDestino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaisDestinoId"] = new SelectList(_context.PaisesDestino, "Id", "Id", cidadeDestino.PaisDestinoId);
            return View(cidadeDestino);
        }

        // GET: CidadeDestino/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidadeDestino = await _context.CidadesDestino.FindAsync(id);
            if (cidadeDestino == null)
            {
                return NotFound();
            }
            ViewData["PaisDestinoId"] = new SelectList(_context.PaisesDestino, "Id", "Id", cidadeDestino.PaisDestinoId);
            return View(cidadeDestino);
        }

        // POST: CidadeDestino/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Nome,PaisDestinoId,Id,CriadoEm,AtualizadoEm")] CidadeDestino cidadeDestino)
        {
            if (id != cidadeDestino.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cidadeDestino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CidadeDestinoExists(cidadeDestino.Id))
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
            ViewData["PaisDestinoId"] = new SelectList(_context.PaisesDestino, "Id", "Id", cidadeDestino.PaisDestinoId);
            return View(cidadeDestino);
        }

        // GET: CidadeDestino/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidadeDestino = await _context.CidadesDestino
                .Include(c => c.PaisDestino)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cidadeDestino == null)
            {
                return NotFound();
            }

            return View(cidadeDestino);
        }

        // POST: CidadeDestino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cidadeDestino = await _context.CidadesDestino.FindAsync(id);
            if (cidadeDestino != null)
            {
                _context.CidadesDestino.Remove(cidadeDestino);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CidadeDestinoExists(long id)
        {
            return _context.CidadesDestino.Any(e => e.Id == id);
        }
    }
}
