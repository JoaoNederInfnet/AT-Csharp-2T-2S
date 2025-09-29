namespace AT_Csharp_2T_2S.Services;

public interface IViewNotesService
{
    /*/ ------------------------------- MÉTODOS ------------------------------- /*/
    //1) Para criar uma nota
    Task CriarNotaAsync(string conteudo);
    //--------------------------------------------/------------------------------------------
    
    //2) Para listar as notas
    Task<List<string>> ListarNotasAsync();
    //--------------------------------------------/------------------------------------------
    
    //3) Para ler a nota
    Task<string?> LerNotaAsync(string fileName);
}