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
    public class ReservaController : Controller
    {
        private readonly QueViagemDbContext _context;

        public ReservaController(QueViagemDbContext context)
        {
            _context = context;
        }

        // GET: Reserva
        public async Task<IActionResult> Index()
        {
            var queViagemDbContext = _context.Reservas.Include(r => r.Cliente).Include(r => r.PacoteTuristico);
            return View(await queViagemDbContext.ToListAsync());
        }

        // GET: Reserva/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.PacoteTuristico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reserva/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
            ViewData["PacoteTuristicoId"] = new SelectList(_context.PacotesTuristicos, "Id", "Id");
            return View();
        }

        // POST: Reserva/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrecoTotal,ClienteId,PacoteTuristicoId,Id,CriadoEm,AtualizadoEm")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", reserva.ClienteId);
            ViewData["PacoteTuristicoId"] = new SelectList(_context.PacotesTuristicos, "Id", "Id", reserva.PacoteTuristicoId);
            return View(reserva);
        }

        // GET: Reserva/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", reserva.ClienteId);
            ViewData["PacoteTuristicoId"] = new SelectList(_context.PacotesTuristicos, "Id", "Id", reserva.PacoteTuristicoId);
            return View(reserva);
        }

        // POST: Reserva/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PrecoTotal,ClienteId,PacoteTuristicoId,Id,CriadoEm,AtualizadoEm")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", reserva.ClienteId);
            ViewData["PacoteTuristicoId"] = new SelectList(_context.PacotesTuristicos, "Id", "Id", reserva.PacoteTuristicoId);
            return View(reserva);
        }

        // GET: Reserva/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.PacoteTuristico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(long id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
