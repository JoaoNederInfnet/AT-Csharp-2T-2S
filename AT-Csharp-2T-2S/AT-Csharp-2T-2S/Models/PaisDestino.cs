namespace AT_Csharp_2T_2S.Models;

public class PaisDestino : Model
{
    /*/ ------------------------------- PROPRIEDADES ------------------------------- /*/
  //1) Nome da cidade 
  public string Nome { get; private set; }
  //========================================================
    
    /*/ ------------------------------- PROPRIEDADES LIGADAS ÀS RELAÇÕES ------------------------------- /*/
    //1) Cidades do país
    //A) Para armazenar cidades estão dentro desse país
    private readonly List<CidadeDestino> _cidadesDestino = new();
    //----------------------------------//-------------------------------- 
    //Para acessar as cidades
    public IReadOnlyCollection<CidadeDestino> CidadesDestino => _cidadesDestino.AsReadOnly();
    //========================================================
    
    /*/ ------------------------------- CONSTRUTORES ------------------------------- /*/
    //1) Vazio
    protected PaisDestino() {}
    //--------------------------------------------/------------------------------------------
    
    //2) Cheio
    public PaisDestino(string nome)
    {
        //•ETAPAS•//
        //•1) Validando os valores
        ValidarNome(nome);
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•2) Atribuindo os valores às propriedades
        Nome = nome;
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
}