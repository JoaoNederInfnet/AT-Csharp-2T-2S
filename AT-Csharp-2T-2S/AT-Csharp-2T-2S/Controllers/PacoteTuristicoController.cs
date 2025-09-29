using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AT_Csharp_2T_2S.Data;
using AT_Csharp_2T_2S.Models;
using AT_Csharp_2T_2S.Services;
using AT_Csharp_2T_2S.Services.Delegates_Events;
using AT_Csharp_2T_2S.ViewModels.PacoteTuristico;

namespace AT_Csharp_2T_2S.Controllers
{
    public class CreatePacoteSuccessViewModel
    {
        public PacoteTuristico PacoteCriado { get; set; }
        public decimal PrecoDescontado { get; set; }
    }
    
    public class PacoteTuristicoController : Controller
    {
        /*/ ------------------------------- CONFIGURANDO INJEÇÃO DE DEPENDÊNCIA ------------------------------- /*/
        //1) Para a Db
        private readonly QueViagemDbContext _context;
        //--------------------------------------------/------------------------------------------
        //2) Para o service
        private readonly IPacoteTuristicoService _pacoteTuristicoService;
        
        public PacoteTuristicoController(IPacoteTuristicoService pacoteTuristicoService, QueViagemDbContext context)
        {
            _pacoteTuristicoService = pacoteTuristicoService;
            _context = context;
        }
        //========================================================
        
        // GET: PacoteTuristico
        public async Task<IActionResult> Index()
        {
            var pacotesNaoDeletados = await _pacoteTuristicoService.PegarTodosPacotesTuristicosNaoDeletadosAsync();
            return View(pacotesNaoDeletados);
        }

        // GET: PacoteTuristico/Details/5
        // Para mostrar os detalhes de uma entidade pelo id
        public async Task<IActionResult> Details(long? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            
            var pacoteTuristico = await _pacoteTuristicoService.PegarPacoteTuristicoCompletoAsync(id.Value);

            // 3. Verificação do resultado (continua igual)
            if (pacoteTuristico == null)
            {
                return NotFound();
            }

            // 4. Envia o objeto completo (com os destinos já carregados) para a View.
            return View(pacoteTuristico);
        }

        // GET: PacoteTuristico/Create
        // Para carregar o view model com a lista de cidades
        public async Task<IActionResult> Create()
        {
            var cidades = await _context.CidadesDestino
                .OrderBy(c => c.Nome)
                .ToListAsync();

            var viewModel = new CreatePacoteTuristicoViewModel
            {
                CidadesDisponiveis = new SelectList(cidades, "Id", "Nome")
            };
    
            return View(viewModel);
        }

        // POST: PacoteTuristico/Create
        // 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePacoteTuristicoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var novoPacote = await _pacoteTuristicoService.CriarNovoPacoteTuristicoAsync(viewModel);
                
                return RedirectToAction(
                    "CreateSuccess",
                    new { id = novoPacote.Id });
            
            }
            
            var cidades = await _context.CidadesDestino.OrderBy(c => c.Nome).ToListAsync();
            viewModel.CidadesDisponiveis = new SelectList(cidades, "Id", "Nome");
    
            return View(viewModel);
        }

        // POST: PacoteTuristico/CreateSucces
        public async Task<IActionResult> CreateSuccess(long id)
        {
            var pacoteCriado = await _pacoteTuristicoService.PegarPacoteTuristicoCompletoAsync(id);
            if (pacoteCriado == null)
            {
                return NotFound();
            }
            
            CalculateDelegate calculateDelegate = MetodosCalculate.DescontoAplicado;
            
            decimal precoDescontado = calculateDelegate(pacoteCriado.Preco);

            var viewModel = new CreateSuccessPacoteTuristicoViewModel
            {
                PacoteCriado = pacoteCriado,
                PrecoDescontado = precoDescontado
            };

            return View(viewModel);
        }
        
        // GET: PacoteTuristico/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacoteTuristico = await _context.PacotesTuristicos
                .Include(p => p.Destinos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pacoteTuristico == null)
            {
                return NotFound();
            }

            // Transformar entidade em ViewModel
            var viewModel = new EditPacoteTuristicoViewModel
            {
                Id = pacoteTuristico.Id,
                Titulo = pacoteTuristico.Titulo,
                DataInicio = pacoteTuristico.DataInicio,
                CapacidadeMaxima = pacoteTuristico.CapacidadeMaxima,
                Preco = pacoteTuristico.Preco,
                Dias = pacoteTuristico.Dias,
                DestinosIds = pacoteTuristico.Destinos.Select(d => d.Id).ToList(),
                CidadesDisponiveis = new SelectList(_context.CidadesDestino, "Id", "Nome")
            };

            return View(viewModel);
        }

        // POST: PacoteTuristico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditPacoteTuristicoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _pacoteTuristicoService.EditarPacoteTuristicoAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }

            // Se der erro de validação, recarrega cidades
            viewModel.CidadesDisponiveis = new SelectList(
                await _context.CidadesDestino.OrderBy(c => c.Nome).ToListAsync(),
                "Id", "Nome"
            );

            return View(viewModel);
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
        // Para fazer a deleção lógica
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _pacoteTuristicoService.DeletarLogicamentePacoteTuristicoAsync(id);
    
            
            return RedirectToAction(nameof(Index));
        }

        private bool PacoteTuristicoExists(long id)
        {
            return _context.PacotesTuristicos.Any(e => e.Id == id);
        }
    }
}
