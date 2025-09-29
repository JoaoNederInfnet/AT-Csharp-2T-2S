using System.ComponentModel.DataAnnotations;

namespace AT_Csharp_2T_2S.ViewModels.ViewNotes;

public class CreateViewNoteViewModel
{
    [Required(ErrorMessage = "A nota não pode estar vazia.")]
    [StringLength(500, ErrorMessage = "A nota não pode ter mais que 500 caracteres.")]
    [Display(Name = "Escreva a nota aqui:")]
    public string NoteText { get; set; } = string.Empty;
}