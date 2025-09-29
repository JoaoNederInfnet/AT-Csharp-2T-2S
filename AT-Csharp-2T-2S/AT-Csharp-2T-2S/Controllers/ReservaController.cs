using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AT_Csharp_2T_2S.Data;
using AT_Csharp_2T_2S.Models;
using AT_Csharp_2T_2S.Services;
using AT_Csharp_2T_2S.ViewModels.Reserva;

namespace AT_Csharp_2T_2S.Controllers
{
    public class ReservaController : Controller
    {
        /*/ ------------------------------- CONFIGURANDO INJEÇÃO DE DEPENDÊNCIA ------------------------------- /*/
        //1) Para a Db
        private readonly QueViagemDbContext _context;
        //--------------------------------------------/------------------------------------------
        //2) Para o service
        //a) De Reserva
        private readonly IReservaService _reservaService;
        
        //b) De PacoteTuristico
        private readonly IPacoteTuristicoService _pacoteTuristicoService;
        public ReservaController(QueViagemDbContext context, IReservaService reservaService, IPacoteTuristicoService pacoteTuristicoService)
        {
            _context = context;
            _reservaService = reservaService;
            _pacoteTuristicoService = pacoteTuristicoService;
        }
        //========================================================

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
        // Para carregar o view model com a lista de pacotes disponíveis para reserva
        public async Task<IActionResult> Create()
        {
            var pacotesDisponiveis = await _pacoteTuristicoService.ListarPacotesTuristicosDisponiveisParaReservaAsync();

            var viewModel = new CreateReservaViewModel
            {
                PacotesDisponiveis = new SelectList(
                    pacotesDisponiveis, 
                    "Id", 
                    "Titulo")
            };

            return View(viewModel);
        }

        // GET: Reserva/CreateSuccess/5
        //
        public async Task<IActionResult> CreateSuccess(long id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.PacoteTuristico)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound();
            }

            var viewModel = new CreateSuccessReservaViewModel
            {
                ReservaCriada = reserva,
                PrecoTotal = reserva.PrecoTotal
            };

            return View(viewModel);
        }

        // POST: Reserva/Create
        // Para 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReservaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var pacotesDisponiveis = await _pacoteTuristicoService
                    .ListarPacotesTuristicosDisponiveisParaReservaAsync();

                viewModel.PacotesDisponiveis = new SelectList(pacotesDisponiveis, "Id", "Titulo");
                return View(viewModel);
            }

            try
            {
                long clienteIdLogado = 1;

                var reservaCriada = await _reservaService.CriarNovaReservaAsync(
                    clienteIdLogado,
                    viewModel.PacoteTuristicoId
                );

                if (reservaCriada == null)
                {
                    ModelState.AddModelError("", "Não foi possível criar a reserva.");
                    return View(viewModel);
                }

                
                return RedirectToAction("CreateSuccess", new { id = reservaCriada.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var pacotesDisponiveis = await _pacoteTuristicoService
                    .ListarPacotesTuristicosDisponiveisParaReservaAsync();
                viewModel.PacotesDisponiveis = new SelectList(pacotesDisponiveis, "Id", "Titulo");

                return View(viewModel);
            }
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
