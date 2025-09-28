using System.ComponentModel.DataAnnotations.Schema;

namespace AT_Csharp_2T_2S.Models;

public class CidadeDestino : Model
{
  /*/ ------------------------------- PROPRIEDADES ------------------------------- /*/
  //1) Nome da cidade 
  public string Nome { get; private set; }
  //========================================================
    
    /*/ ------------------------------- PROPRIEDADES LIGADAS ÀS RELAÇÕES ------------------------------- /*/
    //1) Definindo a entidade com a qual a cidade se relaciona (pertence)
    [ForeignKey("PaisDestino")]
    public long PaisDestinoId { get; private set; }
    public PaisDestino PaisDestino { get; private set; }
    //--------------------------------------------/------------------------------------------
    
    //2) Para armazenar quais pacotes turisticos incluem a cidade
    private readonly List<PacoteTuristico> _pacotesTuristicos = new();

    //Para acessar os pacotes turisticos
    public IReadOnlyCollection<PacoteTuristico> PacotesTuristicos => _pacotesTuristicos.AsReadOnly();
    //========================================================
    
    /*/ ------------------------------- CONSTRUTORES ------------------------------- /*/
    //1) Vazio
    protected CidadeDestino() {}
    //--------------------------------------------/------------------------------------------
    
    //2) Cheio
    public CidadeDestino(string nome, long paisDestinoId)
    {
        //•ETAPAS•//
        //•1) Validando os valores
        ValidarNome(nome);
        //--------------------------------------------/------------------------------------------
        ValidarPaisDestinoId(paisDestinoId);
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•2) Atribuindo os valores às propriedades
        Nome = nome;
        //--------------------------------------------/------------------------------------------
        PaisDestinoId = paisDestinoId;
    }
    //========================================================
    
    /*/ ------------------------------- VALIDAÇÕES PARA O CONSTRUTOR ------------------------------- /*/
    // Para a propriedade
    //1) Nome da cidade
    private void ValidarNome(string nome)
    {
        //•VALIDANDO•//
        //A) Se não é nulo ou está vazio
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome é obrigatório!", nameof(nome));
        //--------------------------------------------/------------------------------------------
        //B) Se tem mais de 1 caracteres 
        if (nome.Length < 2) throw new ArgumentException("O nome precisa ter pelo menos 2 caracteres!", nameof(nome));
    }
    //--------------------------------------------/------------------------------------------
    
    //2) Id do país a que pertence
    private void ValidarPaisDestinoId(long paisDestinoId)
    {
        //•VALIDANDO•//
        //A) Se não é nulo ou está vazio
        if (paisDestinoId <= 0) throw new ArgumentException("Não existe um país com esse ID!", nameof(paisDestinoId));
    }
}