using AT_Csharp_2T_2S.Data;
using AT_Csharp_2T_2S.Models;

namespace AT_Csharp_2T_2S.Services;

/*/ ------------------------------- DELEGATE ------------------------------- /*/
//Delegate
//1) Declarando o delegate
public delegate decimal CalculateDelegate(decimal preco);

//2)Definindo os métodos que serão usados com o delegate
public class MetodosCalculate
{
    public static decimal DescontoAplicado(decimal preco)
    {
        decimal precoDescontado = preco * (1 - 0.1m);

        return precoDescontado;
    }
}
//========================================================

/*/ ------------------------------- EVENT HANDLER ------------------------------- /*/
//Evento que será disparado por CapacityReached
public class AlertaDeCapacidadeHandler
{
    public void LidarComCapacityReached(object? sender, CapacityReachedEventArgs e)
    {
        Console.WriteLine($"--- ALERTA DE CAPACIDADE MÁXIMA ULTRAPASSADA PARA O PACOTE: {e.TituloPacote} -> {e.NumeroReservasAtualPacote}/{e.CapacidadeMaximaPacote} ---");
    }
}
//========================================================

public class ReservaService : IReservaService
{
    /*/ ------------------------------- CONFIGURANDO INJEÇÃO DE DEPENDÊNCIA ------------------------------- /*/
    //1) Para a Db
    private readonly QueViagemDbContext _context;

    //2) Para o logger
    private readonly Action<string> _logger;

    public ReservaService(QueViagemDbContext context, Action<string> logger)
    {
        _context = context;
        _logger = logger;
    }
    //========================================================
  
    /*/ ------------------------------- MÉTODOS ------------------------------- /*/
    // # Usados pela própria Reserva #
    //Lógica no reserva services
    public async Task<Reserva?> CriarNovaReservaAsync(long clienteId, long pacoteTuristicoId)
    {
        //•ETAPAS•//
        //•1) Pegando os dados do pacote e cliente
        var pacote = await _context.PacotesTuristicos.FindAsync(pacoteTuristicoId);
        var cliente = await _context.Clientes.FindAsync(clienteId);

        if (pacote == null) throw new KeyNotFoundException("Pacote Turístico não encontrado.");
        if (cliente == null) throw new KeyNotFoundException("Cliente não encontrado.");
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•2) Calculando o preço total da reserva
        decimal precoBase = pacote.Preco;
        int diasBase = pacote.Dias;
        
        //Definindo e aplicando o delegate de desconto
        CalculateDelegate calcularDesconto = p => p * 0.90m;
        decimal precoDesconto = calcularDesconto(precoBase);

        //Definindo e aplicando o delegate de calcular o preco total
        Func<int, decimal, decimal> calcularPrecoTotal = (dias, preco) => dias * preco;
        decimal precoFinal = calcularPrecoTotal(diasBase, precoDesconto);
       
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•3) Criando e salvando a reserva o preço total da reserva
        //Chamando o evento
        var alertaHandler = new AlertaDeCapacidadeHandler();

        //Inscrevendo o ouvinte no evento
        pacote.CapacityReached += alertaHandler.LidarComCapacityReached;

        Reserva novaReserva = null;
        //Tentando criar uma nova reserva
        try
        {
             novaReserva = new Reserva(precoFinal, clienteId, pacoteTuristicoId);
            
            pacote.CadastrarReservaOuDispararEvento(novaReserva);

            await _context.Reservas.AddAsync(novaReserva);
            await _context.SaveChangesAsync();
            
            _logger?.Invoke($"Reserva ID {novaReserva.Id} criada com sucesso para o cliente " + $"'{cliente.Nome}' no pacote '{pacote.Titulo}'.");
        }
        catch (InvalidOperationException ex)
        {
            _logger?.Invoke($"FALHA AO RESERVAR: {ex.Message}");
            throw;
        }
        finally
        {
            //Desisncrevendo o ouvinte
            pacote.CapacityReached -= alertaHandler.LidarComCapacityReached;
        }

        return novaReserva;
    }
}
