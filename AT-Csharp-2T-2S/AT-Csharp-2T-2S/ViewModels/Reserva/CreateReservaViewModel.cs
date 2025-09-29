using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AT_Csharp_2T_2S.ViewModels.Reserva;

public class CreateReservaViewModel
{
    /*/ ------------------------------- PROPRIEDADES ------------------------------- /*/
    [Required(ErrorMessage = "É obrigatório selecionar um pacote.")]
    [Range(1, long.MaxValue, ErrorMessage = "Por favor, selecione um pacote válido.")]
    [Display(Name = "Pacote Turístico")]
    public long PacoteTuristicoId { get; set; } // Corrigido de "Idl" para "Id"
    //--------------------------------------------/------------------------------------------
    
     // Para carregar as opções para o pacote turístico
    public SelectList? PacotesDisponiveis { get; set; }
}