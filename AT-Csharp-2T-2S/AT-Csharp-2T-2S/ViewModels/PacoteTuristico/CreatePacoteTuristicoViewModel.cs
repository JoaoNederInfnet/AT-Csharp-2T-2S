using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AT_Csharp_2T_2S.ViewModels.PacoteTuristico;

public class CreatePacoteTuristicoViewModel
{
    /*/ ------------------------------- PROPRIEDADES ------------------------------- /*/
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O título deve ter entre 3 e 50 caracteres.")]
    public string Titulo { get; set; }
    //--------------------------------------------/------------------------------------------
    [Required(ErrorMessage = "A data de início é obrigatória.")]
    [DataType(DataType.Date)]
    [Display(Name = "Data de Início")]
    public DateTime DataInicio { get; set; }
    //--------------------------------------------/------------------------------------------
    [Required(ErrorMessage = "A capacidade máxima é obrigatória.")]
    [Range(1, 200, ErrorMessage = "A capacidade deve ser entre 1 e 200.")]
    [Display(Name = "Capacidade Máxima")]
    public int CapacidadeMaxima { get; set; }
    //--------------------------------------------/------------------------------------------   
    [Required(ErrorMessage = "O preço da diária é obrigatório.")]
    [Range(1, 10000, ErrorMessage = "O preço deve ser um valor positivo.")]
    [Display(Name = "Preço por Diária")]
    public decimal Preco { get; set; }
    //--------------------------------------------/------------------------------------------
    [Required(ErrorMessage = "Selecione pelo menos um destino.")]
    [Display(Name = "Destinos")]
    public List<long> DestinosIds { get; set; } = new List<long>();
    //--------------------------------------------/------------------------------------------
    [Required(ErrorMessage = "A duração é obrigatória.")]
    [Range(1, 90, ErrorMessage = "A duração deve ser entre 1 e 90 dias.")]
    [Display(Name = "Duração em Dias")]
    public int Dias { get; set; }
    //--------------------------------------------/------------------------------------------
    
    // Para carregar as opções para cidade do formulário
    public SelectList? CidadesDisponiveis { get; set; }
}