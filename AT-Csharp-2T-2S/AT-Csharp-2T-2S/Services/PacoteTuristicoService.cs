using AT_Csharp_2T_2S.Data;
using AT_Csharp_2T_2S.Models;
using AT_Csharp_2T_2S.ViewModels.PacoteTuristico;
using Microsoft.EntityFrameworkCore;
using AT_Csharp_2T_2S.Services.Delegates_Events;

namespace AT_Csharp_2T_2S.Services;

public class PacoteTuristicoService : IPacoteTuristicoService
{
    /*/ ------------------------------- CONFIGURANDO INJEÇÃO DE DEPENDÊNCIA ------------------------------- /*/
    //1) Para a Db
    private readonly QueViagemDbContext _context;
    
    public PacoteTuristicoService(QueViagemDbContext context)
    {
        _context = context;
    }
    //========================================================
  
    /*/ ------------------------------- MÉTODOS ------------------------------- /*/
    // # Usados pelo próprio Pacote Turistico#
    //#1) Para criar um novo pacote a partir do Input Model no CreateViewModel
    public async Task<PacoteTuristico?> CriarNovoPacoteTuristicoAsync(CreatePacoteTuristicoViewModel viewModel)
    {
        //•ETAPAS•//
        //•1) Pegando as cidades de destino
        var destinosSelecionados = await _context.CidadesDestino
            .Where(c => viewModel.DestinosIds.Contains(c.Id))
            .ToListAsync();
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•2) Criando um novo pacote turístico
        var novoPacoteTuristico = new PacoteTuristico
        (
            viewModel.Titulo,
            viewModel.DataInicio,
            viewModel.CapacidadeMaxima,
            viewModel.Preco,
            destinosSelecionados,
            viewModel.Dias
        );
        
        await _context.PacotesTuristicos.AddAsync(novoPacoteTuristico);
        await _context.SaveChangesAsync();
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•3) 
        return novoPacoteTuristico;
    }
    //--------------------------------------------/------------------------------------------
    
    //#2) Para achar um pacote pelo id 
    public async Task<PacoteTuristico?> PegarPacoteTuristicoCompletoAsync(long id)
    {
        return await _context.PacotesTuristicos
            .Include(p => p.Destinos)
            .ThenInclude(d => d.PaisDestino)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    //--------------------------------------------/------------------------------------------
    
    //#3) Para editar um pacote
    public async Task<PacoteTuristico?> EditarPacoteTuristicoAsync(EditPacoteTuristicoViewModel viewModel)
    {
        //•ETAPAS•//
        //•1) Pegando o pacote turistico com os destinos
         var pacote = await _context.PacotesTuristicos
             .Include(p => p.Destinos)
             .FirstOrDefaultAsync(p => p.Id == viewModel.Id);
         
         if(pacote == null) throw new KeyNotFoundException("Pacote não encontrado.");
         //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
         //•2) Pegando a lista de cidades incluídas no pacote
         var destinosSelecionados = await _context.CidadesDestino
             .Where(c => viewModel.DestinosIds.Contains(c.Id))
             .ToListAsync();
         //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
         //•3) Editando as informacoes com método do model
         pacote.EditarInformacoes
         (
             viewModel.Titulo,
             viewModel.DataInicio,
             viewModel.CapacidadeMaxima,
             viewModel.Preco,
             destinosSelecionados,
             viewModel.Dias
         );

         await _context.SaveChangesAsync();
         //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
         //•4)
         return pacote;
    }
    //--------------------------------------------/------------------------------------------
   
    //#4) Para fazer a deleção lógica
    public async Task DeletarLogicamentePacoteTuristicoAsync(long id)
    {
        //•ETAPAS•//
        //•1) Pegando o pacote
        var pacote = await _context.PacotesTuristicos.FindAsync(id);
        
        if(pacote == null) throw new KeyNotFoundException("Pacote não encontrado.");
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•2) Usando o método da classe para marcar como deletado
        pacote.Deletar(); 
        await _context.SaveChangesAsync();
    }
    //--------------------------------------------/------------------------------------------
   
    //#5) Para pegar a lista com todos os pacotes que não estão deletados 
    public async Task<List<PacoteTuristico>> PegarTodosPacotesTuristicosNaoDeletadosAsync()
    {
        return await _context.PacotesTuristicos
            .Where(p => !p.EstaDeletado)
            .ToListAsync();
    }
    //--------------------------------------------/------------------------------------------
   
    //#6) Para pegar a lista com todos os pacotes selecionáveis por um cliente no momento presente
    public async Task<List<PacoteTuristico>> ListarPacotesTuristicosDisponiveisParaReservaAsync()
    {
        // A LÓGICA DE FILTRAGEM AGORA VIVE AQUI!
        return await _context.PacotesTuristicos
            .Where(pacote => 
                !pacote.EstaDeletado &&
                pacote.DataInicio > DateTime.Now &&
                pacote.Reservas.Count < pacote.CapacidadeMaxima)
            .OrderBy(pacote => pacote.Titulo)
            .ToListAsync();
    }
    //---------------#---------------#---------------#---------------#---------------
    
    // # Usados fora do próprio Pacote Turistico#
}