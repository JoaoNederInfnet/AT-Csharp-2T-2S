namespace AT_Csharp_2T_2S.Services.Delegates_Events;

public class MetodosLog
{
  private readonly List<string> _memoryLogs = new List<string>();
  
  public IReadOnlyCollection<string> MemoryLogs => _memoryLogs.AsReadOnly();

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
  
  //3) Para log na memória
  public void LogToMemory(string message)
  {
    _memoryLogs.Add($"[MEMORY LOG] {DateTime.Now:G}: {message}");
  }
}
