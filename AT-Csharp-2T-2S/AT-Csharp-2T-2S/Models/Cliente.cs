using System.ComponentModel.DataAnnotations;

namespace AT_Csharp_2T_2S.Models;

public class Cliente : Model
{
    /*/ ------------------------------- PROPRIEDADES ------------------------------- /*/
    //1) Nome do cliente 
    public string Nome { get; private set; }
    //--------------------------------------------/------------------------------------------
    
    //2) Email do cliente
    public string Email { get; private set; }
    //========================================================
    
    /*/ ------------------------------- PROPRIEDADES LIGADAS ÀS RELAÇÕES ------------------------------- /*/
    //1) Reservas do Cliente
    //A) Para armazenar as reservas
    private readonly List<Reserva> _reservas = new();
    //----------------------------------//--------------------------------
    //B) Para acessar as reservas 
    public IReadOnlyCollection<Reserva> Reservas => _reservas.AsReadOnly();
    //========================================================
    
    /*/ ------------------------------- CONSTRUTORES ------------------------------- /*/
    //1) Vazio
    protected Cliente() {}
    //--------------------------------------------/------------------------------------------
    
    //2) Cheio
    public Cliente(string nome, string email)
    {
        //•ETAPAS•//
        //•1) Validando os valores
        ValidarNome(nome);
        //--------------------------------------------/------------------------------------------
        ValidarEmail(email);
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•2) Atribuindo os valores às propriedades
        Nome = nome;
        //--------------------------------------------/------------------------------------------
        Email = email;
    }
    //========================================================
    
    /*/ ------------------------------- VALIDAÇÕES PARA O CONSTRUTOR ------------------------------- /*/
    // Para a propriedade
    //1) Nome do cliente 
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
    
    //2) Email do cliente
    private void ValidarEmail(string email)
    {
        //•VALIDANDO•
        //A) Se não é nulo ou está vazio
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("O e-mail é obrigatório!", nameof(email));
        //--------------------------------------------/------------------------------------------
        //B) Se tem @ e .
        if (!email.Contains("@") || !email.Contains(".")) throw new ArgumentException("O e-mail está em um formato inválido!", nameof(email));
    }
}