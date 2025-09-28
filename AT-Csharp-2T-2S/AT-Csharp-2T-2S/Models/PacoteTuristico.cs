using System.ComponentModel.DataAnnotations.Schema;

namespace AT_Csharp_2T_2S.Models;

/*/ ------------------------------- SETUP EVENTO ------------------------------- /*/
//Classe com os dados que serão enviados junto ao evento no handler
public class CapacityReachedEventArgs : EventArgs
{
    public string TituloPacote { get; }
    public int NumeroReservasAtualPacote { get; }
    public int CapacidadeMaximaPacote { get; }

    public CapacityReachedEventArgs(string tituloPacote, int numeroReservasAtualPacote, int capacidadeMaximaPacote)
    {
        TituloPacote = tituloPacote;
        NumeroReservasAtualPacote = numeroReservasAtualPacote;
        CapacidadeMaximaPacote = capacidadeMaximaPacote;
    }
}
//========================================================

public class PacoteTuristico : Model
{
    /*/ ------------------------------- PROPRIEDADES ------------------------------- /*/
    //1) Titulo do pacote
    public string Titulo { get; private set; }
    //--------------------------------------------/------------------------------------------

    //2) Data de Inicio do pacote
    public DateTime DataInicio { get; private set; }
    //--------------------------------------------/------------------------------------------

    //3) Capacidade maxima de pessoas
    public int CapacidadeMaxima { get; private set; }
    //--------------------------------------------/------------------------------------------

    //4) Preço do pacote
    public decimal Preco { get; private set; }
    //--------------------------------------------/------------------------------------------
    
    //5) Duração em dias
    public int Dias { get; private set; }
    //--------------------------------------------/------------------------------------------
    
    //6) Data de termino
    [NotMapped]
    public DateTime DataTermino
    {
        get
        {
            return DataInicio.AddDays(Dias);
        }
    }
    //--------------------------------------------/------------------------------------------
    
    //7) Número de reservas atual
    [NotMapped]
    public int NumeroDeReservasAtual
    {
        get
        {
            return _reservas.Count;
        }
    }
    //========================================================

    /*/ ------------------------------- PROPRIEDADES LIGADAS ÀS RELAÇÕES ------------------------------- /*/
    //1) Para definir a relação com as cidades de destino
    public ICollection<CidadeDestino> Destinos { get; private set; } = new List<CidadeDestino>();
    //--------------------------------------------/------------------------------------------

    //2) Reservas
    //A) Para armazenar as reservas
    private readonly List<Reserva> _reservas = new();

    //----------------------------------//--------------------------------
    //B) Para acessar as reservas
    public IReadOnlyCollection<Reserva> Reservas => _reservas.AsReadOnly();
    //========================================================

    /*/ ------------------------------- CONSTRUTORES ------------------------------- /*/
    //1) Vazio
    protected PacoteTuristico() {}
    //--------------------------------------------/------------------------------------------
    
    //2) Cheio
    public PacoteTuristico(string titulo, DateTime dataInicio, int capacidadeMaxima, decimal preco,
        List<CidadeDestino> destinos, int dias)
    {
        //•ETAPAS•//
        //•1) Validando os valores
        ValidarTitulo(titulo);
        //--------------------------------------------/------------------------------------------
        ValidarDataInicio(dataInicio);
        //--------------------------------------------/------------------------------------------
        ValidarCapacidadeMaxima(capacidadeMaxima);
        //--------------------------------------------/------------------------------------------
        ValidarPreco(preco);
        //--------------------------------------------/------------------------------------------
        ValidarDestinos(destinos);
        //--------------------------------------------/------------------------------------------
        ValidarDias(dias);
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•2) Atribuindo os valores às propriedades
        Titulo = titulo;
        DataInicio = dataInicio;
        CapacidadeMaxima = capacidadeMaxima;
        Preco = preco;
        Destinos = destinos;
        Dias = dias;
    }
    //========================================================

    /*/ ------------------------------- VALIDAÇÕES PARA O CONSTRUTOR ------------------------------- /*/
    // Para a propriedade
    //1) Titulo do pacote
    private void ValidarTitulo(string titulo)
    {
        //•VALIDANDO•//
        //A) Se não é nulo ou está vazio
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("O titulo é obrigatório!", nameof(titulo));
        //--------------------------------------------/------------------------------------------
        //B) Se tem mais de 1 caracteres 
        if (titulo.Length < 2)
            throw new ArgumentException("O titulo precisa ter pelo menos 2 caracteres!", nameof(titulo));
    }
    //--------------------------------------------/------------------------------------------

    //2) Data de inicio
    private void ValidarDataInicio(DateTime dataInicio)
    {
        //•VALIDANDO•//
        //A) Se a data é futura
        if (dataInicio <= DateTime.Now)
            throw new ArgumentException("Essa data de ínicio não é válida!", nameof(dataInicio));
    }
    //--------------------------------------------/------------------------------------------

    //3) Capacidade Máxima
    private void ValidarCapacidadeMaxima(int capacidadeMaxima)
    {
        //•VALIDANDO•//
        //A) Se a capacidade máxima é maior que 0
        if (capacidadeMaxima <= 0)
            throw new ArgumentException("Essa capacidade máxima não é válida!", nameof(capacidadeMaxima));
    }
    //--------------------------------------------/------------------------------------------

    //4) Preco do pacote
    private void ValidarPreco(decimal Preco)
    {
        //•VALIDANDO•//
        //A) Se o preco é > = a 0
        if (Preco <= 0) throw new ArgumentException("Esse preço não é válido!", nameof(Preco));
    }
    //--------------------------------------------/------------------------------------------

    //5) Destinos incluídos
    private void ValidarDestinos(ICollection<CidadeDestino> destinos)
    {
        //•VALIDANDO•//
        //A) Se os destinos foram preenchidos
        if (destinos == null) throw new ArgumentException("Esses destinos não são válidos!", nameof(destinos));
    }
    //--------------------------------------------/------------------------------------------

    //6) Dias de duração
    private void ValidarDias(int dias)
    {
        //•VALIDANDO•//
        //A) Se a quantidade de dias é >= 1
        if (dias < 1) throw new ArgumentException("Esses dias não são válidos!", nameof(dias));
    }
    
    /*/ ------------------------------- LÓGICA EVENTO ------------------------------- /*/
    //#)Evento para monitorar a quantidade de reservas e disparar quando a quantidade ultrapassar o limite
    //#1) Declarando o evento usando o delegate EventHandler<TEventArgs> com os event args (dados) declarados na linha 6 
    public event EventHandler<CapacityReachedEventArgs>? CapacityReached; 
    //--------------------------------------------/------------------------------------------
    
    //#2) Definindo o método gatilho para chamar o disparador o evento no services de reserva
    public void CadastrarReservaOuDispararEvento(Reserva reserva)
    {
        //•ETAPAS•//
        //•1) Conferindo a capacidade para disparar o gatilho e disparar exceção
        if (NumeroDeReservasAtual > CapacidadeMaxima)
        {
            //a) Disparando o evento
            OnCapacityReached();
            
            //b) Lançando excessão e retornando mensagem de erro
            throw new InvalidOperationException($"Não há mais reservas disponíveis para o pacote '{Titulo}'.");
        }
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•2) Cadastrando a reserva caso o limite não tenha sido ultrapassado
        _reservas.Add(reserva);
    }
    //--------------------------------------------/------------------------------------------
    
    //#3) Definindo o método auxiliar para disparar o evento de forma mais segura
    protected virtual void OnCapacityReached()
    {
        CapacityReached?.Invoke(this, new CapacityReachedEventArgs(this.Titulo, this.NumeroDeReservasAtual, this.CapacidadeMaxima));
    }
}
// //Evento que será disparado por CapacityReached(colocarei no reserva services eu acho) 
// public class AlertaDeCapacidadeHandler
// {
//     public void LidarComCapacityReached(object? sender, CapacityReachedEventArgs e)
//     {
//         Console.WriteLine($"--- ALERTA DE CAPACIDADE MÁXIMA ULTRAPASSADA PARA O PACOTE: {e.TituloPacote} -> {e.NumeroReservasAtualPacote}/{e.CapacidadeMaximaPacote} ---");
//     }
// }
//
// //Lógica no reserva services
// public void CriarNovaReserva(long pacoteId, long clienteId)
// {
//     var pacote = _context.PacotesTuristicos.Find(pacoteId);
//     var cliente = _context.Clientes.Find(clienteId);
//     // ... tratar erros se não encontrar ...
//
//     // 1. Crie o "ouvinte"
//     var alertaHandler = new AlertaDeCapacidadeHandler();
//
//     // 2. Inscreva o ouvinte no evento do pacote específico
//     pacote.CapacidadeAtingida += alertaHandler.LidarComCapacidadeAtingida;
//
//     try
//     {
//         // Crie a reserva
//         var novaReserva = new Reserva(clienteId, pacoteId, /*...preço...*/);
//         
//         // 3. Execute o método de negócio que pode disparar o evento
//         pacote.AdicionarReserva(novaReserva);
//
//         _context.Reservas.Add(novaReserva);
//         _context.SaveChanges();
//     }
//     catch (InvalidOperationException ex)
//     {
//         Console.WriteLine($"FALHA AO RESERVAR: {ex.Message}");
//     }
//     finally
//     {
//         // 4. Desinscreva o ouvinte (boa prática)
//         pacote.CapacidadeAtingida -= alertaHandler.LidarComCapacidadeAtingida;
//     }
// }