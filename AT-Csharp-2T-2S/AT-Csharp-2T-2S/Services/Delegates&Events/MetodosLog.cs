namespace AT_Csharp_2T_2S.Services.Delegates_Events;

public class MetodosLog
{
  private readonly List<string> _memoryLogs = new List<string>();

  // 2. A VITRINE: Uma propriedade pública para LER os logs, mas não para modificá-los.
  public IReadOnlyCollection<string> MemoryLogs => _memoryLogs.AsReadOnly();

  // 3. AS AÇÕES: Os métodos de log agora são métodos de instância.

  /*/ ------------------------------- MÉTODOS ------------------------------- /*/
  //1) Para log no console
  public void LogToConsole(string message)
  {
    Console.WriteLine($"[CONSOLE LOG] {DateTime.Now:G}: {message}");
  }
  //--------------------------------------------/------------------------------------------
  
  //2) Para log em um arquivo
  public void LogToFile(string message)
  {
    try
    {
      Directory.CreateDirectory("logs");
      File.AppendAllText("logs/system_log.txt", $"{DateTime.Now:G}: {message}\n");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"ERRO AO GRAVAR NO ARQUIVO: {ex.Message}");
    }
  }
  //--------------------------------------------/------------------------------------------
  
  //Para log 
  public void LogToMemory(string message)
  {
    // Este método agora usa a lista privada da própria classe.
    _memoryLogs.Add($"[MEMORY LOG] {DateTime.Now:G}: {message}");
  }
}
