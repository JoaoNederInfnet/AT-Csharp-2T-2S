namespace AT_Csharp_2T_2S.Models;

public class PacoteTuristico : Model
{
    /*/ ------------------------------- PROPRIEDADES ------------------------------- /*/
    //1) Titulo do pacote
    public string Titulo { get; set; }
    //--------------------------------------------/------------------------------------------
    
    //2) Data de Inicio do pacote
    public DateTime DataInicio { get; set; }
    //--------------------------------------------/------------------------------------------
    
    //3) Capacidade maxima de pessoas
    public int CapacidadeMaxima { get; set; }
    //--------------------------------------------/------------------------------------------
    
    //4) Preço do pacote
    public decimal Preco { get; set; }
    //--------------------------------------------/------------------------------------------
    
    //5) Destinos incluídos no pacote
    public ICollection<string> Destinos { get; set; }
    //========================================================
    
    /*/ ------------------------------- CONSTRUTORES ------------------------------- /*/
    //1) Cheio
    public PacoteTuristico(string titulo, DateTime dataInicio, int capacidadeMaxima, decimal preco, ICollection<string> destinos)
    {
        Titulo = titulo;
        DataInicio = dataInicio;
        CapacidadeMaxima = capacidadeMaxima;
        Preco = preco;
        Destinos = destinos;
    }
    //========================================================
}