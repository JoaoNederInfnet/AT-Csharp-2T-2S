using System.ComponentModel.DataAnnotations.Schema;

namespace AT_Csharp_2T_2S.Models;

public class Reserva : Model
{
    /*/ ------------------------------- PROPRIEDADES ------------------------------- /*/
    //1) Preco total da reserva
    public decimal PrecoTotal { get; private set; }
    //========================================================
    
    /*/ ------------------------------- RELAÇÕES ------------------------------- /*/
    //1) Definindo a entidade com a qual o Endereço se relaciona (pertence)
    [ForeignKey("Cliente")]
    public long ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }
    //--------------------------------------------/------------------------------------------
    
    //2) Definindo a entidade com a qual a Reserva se relaciona (pertence)
    [ForeignKey("PacoteTuristico")]
    public long PacoteTuristicoId { get; private set; }
    public PacoteTuristico PacoteTuristico { get; private set; }
    //========================================================
    
    /*/ ------------------------------- CONSTRUTORES ------------------------------- /*/
    //1) Vazio
    protected Reserva() {}
    //--------------------------------------------/------------------------------------------
    //2) Cheio
    public Reserva(decimal precoTotal, long clienteId, long pacoteTuristicoId)
    {
        //•ETAPAS•//
        //•1) Validando os valores
        ValidarPrecoTotal(precoTotal);
        //--------------------------------------------/------------------------------------------
        ValidarClienteId(clienteId);
        //--------------------------------------------/------------------------------------------
        ValidarPacoteTuristicoId(pacoteTuristicoId);
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•2) Atribuindo os valores às propriedades
        PrecoTotal = precoTotal;
        ClienteId = clienteId;
        PacoteTuristicoId = pacoteTuristicoId;
    }
    //========================================================
    
    /*/ ------------------------------- VALIDAÇÕES PARA O CONSTRUTOR ------------------------------- /*/
    // Para a propriedade
    //1) Preco total da reserva 
    private void ValidarPrecoTotal(decimal precoTotal)
    {
        //•VALIDANDO•//
        //A) Se o preço é > que 0
        if (precoTotal <= 0) throw new ArgumentException("Preço total inválido!", nameof(precoTotal));
    }
    //--------------------------------------------/------------------------------------------
    //2) Id do cliente
    private void ValidarClienteId(long clienteId)
    {
        //•VALIDANDO•//
        //A) Se o id do cliente que fez a reserva é valido
        if (clienteId <= 0) throw new ArgumentException("Não existe um cliente com esse ID!", nameof(clienteId));
    }
    //--------------------------------------------/------------------------------------------
    //3) Id do pacote
    private void ValidarPacoteTuristicoId(long pacoteTuristicoId)
    {
        //•VALIDANDO•//
        //A) Se o id do pacoteTuristico que fez a reserva é valido
        if (pacoteTuristicoId <= 0) throw new ArgumentException("Não existe um Pacote Turistico com esse ID!", nameof(pacoteTuristicoId));
    }
}