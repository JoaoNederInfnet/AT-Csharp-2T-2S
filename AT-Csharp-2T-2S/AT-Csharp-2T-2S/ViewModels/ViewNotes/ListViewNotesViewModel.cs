namespace AT_Csharp_2T_2S.ViewModels.ViewNotes;

public class ListViewNotesViewModel
{
    /*/ ------------------------------- PROPRIEDADES ------------------------------- /*/
    public List<string> Files { get; set; } = new();
    //--------------------------------------------/------------------------------------------
    public string? FileName { get; set; }
    //--------------------------------------------/------------------------------------------
    public string? FileContent { get; set; }
    //--------------------------------------------/------------------------------------------
    
    //Para conseguir usar as 2 views de uma vez
    public CreateViewNoteViewModel CreateNote { get; set; } = new();
}