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
    public class PacoteTuristicoController : Controller
    {
        private readonly QueViagemDbContext _context;

        public PacoteTuristicoController(QueViagemDbContext context)
        {
            _context = context;
        }

        // GET: PacoteTuristico
        public async Task<IActionResult> Index()
        {
            return View(await _context.PacotesTuristicos.ToListAsync());
        }

        // GET: PacoteTuristico/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteTuristico = await _context.PacotesTuristicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacoteTuristico == null)
            {
                return NotFound();
            }

            return View(pacoteTuristico);
        }

        // GET: PacoteTuristico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PacoteTuristico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,DataInicio,CapacidadeMaxima,Preco,Dias,Id,CriadoEm,AtualizadoEm")] PacoteTuristico pacoteTuristico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacoteTuristico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pacoteTuristico);
        }

        // GET: PacoteTuristico/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteTuristico = await _context.PacotesTuristicos.FindAsync(id);
            if (pacoteTuristico == null)
            {
                return NotFound();
            }
            return View(pacoteTuristico);
        }

        // POST: PacoteTuristico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Titulo,DataInicio,CapacidadeMaxima,Preco,Dias,Id,CriadoEm,AtualizadoEm")] PacoteTuristico pacoteTuristico)
        {
            if (id != pacoteTuristico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacoteTuristico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacoteTuristicoExists(pacoteTuristico.Id))
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
            return View(pacoteTuristico);
        }

        // GET: PacoteTuristico/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteTuristico = await _context.PacotesTuristicos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacoteTuristico == null)
            {
                return NotFound();
            }

            return View(pacoteTuristico);
        }

        // POST: PacoteTuristico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var pacoteTuristico = await _context.PacotesTuristicos.FindAsync(id);
            if (pacoteTuristico != null)
            {
                _context.PacotesTuristicos.Remove(pacoteTuristico);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacoteTuristicoExists(long id)
        {
            return _context.PacotesTuristicos.Any(e => e.Id == id);
        }
    }
}
