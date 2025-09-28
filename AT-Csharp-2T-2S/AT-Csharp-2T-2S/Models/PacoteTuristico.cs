using System.ComponentModel.DataAnnotations.Schema;

namespace AT_Csharp_2T_2S.Models;

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
    public ICollection<CidadeDestino> Destinos { get; private set; }
    //--------------------------------------------/------------------------------------------

    //2) Reservas
    //A) Para armazenar as reservas
    private readonly List<Reserva> _reservas = new();

    //----------------------------------//--------------------------------
    //B) Para acessar as reservas
    public IReadOnlyCollection<Reserva> Reservas => _reservas.AsReadOnly();
    //========================================================

    /*/ ------------------------------- CONSTRUTORES ------------------------------- /*/
    //1) Cheio
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
}
