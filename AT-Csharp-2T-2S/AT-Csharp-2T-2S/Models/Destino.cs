using System.ComponentModel.DataAnnotations.Schema;

namespace AT_Csharp_2T_2S.Models;

public class Destino
{
    /*/ ------------------------------- PROPRIEDADES ------------------------------- /*/
    //1) Cidade de destino
    public string Cidade { get; private set; }
    //--------------------------------------------/------------------------------------------
    //2) País de destino 
    public string Pais { get; private set; }
    //========================================================
    
    /*/ ------------------------------- PROPRIEDADES LIGADAS ÀS RELAÇÕES ------------------------------- /*/
    //1) Definindo a entidade com a qual o Destino se relaciona (pertence)
    [ForeignKey("PacoteTuristico")]
    public long PacoteTuristicoId { get; set; }
    public PacoteTuristico PacoteTuristico { get; set; }
    //========================================================
    
    /*/ ------------------------------- CONSTRUTORES ------------------------------- /*/
    //1) Vazio
    protected Destino() {}
    //--------------------------------------------/------------------------------------------
    //2) Cheio
    public Destino(string cidade, string pais)
    {
        Cidade = cidade;
        Pais = pais;
    }
    //========================================================
}