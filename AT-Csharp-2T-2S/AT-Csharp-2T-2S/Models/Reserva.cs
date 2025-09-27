using System.ComponentModel.DataAnnotations.Schema;

namespace AT_Csharp_2T_2S.Models;

public class Reserva : Model
{
    /*/ ------------------------------- PROPRIEDADES ------------------------------- /*/
    //1) Data da reserva
    public DateTime DataReserva { get; set; }    
    //========================================================
    
    /*/ ------------------------------- RELAÇÕES ------------------------------- /*/
    //1) Definindo a entidade com a qual o Endereço se relaciona (pertence)
    [ForeignKey("Cliente")]
    public long ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    //--------------------------------------------/------------------------------------------
    
    //2) Definindo a entidade com a qual a Reserva se relaciona (pertence)
    [ForeignKey("PacoteTuristico")]
    public long PacoteTuristicoId { get; set; }
    public PacoteTuristico PacoteTuristico { get; set; }
    //========================================================
    
    /*/ ------------------------------- CONSTRUTORES ------------------------------- /*/
    //1) Vazio
    protected Reserva() {}
    //--------------------------------------------/------------------------------------------
    //2) Cheio
    public Reserva(DateTime dataReserva)
    {
        DataReserva = dataReserva;
    }
    //========================================================
}