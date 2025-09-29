using AT_Csharp_2T_2S.Data;

namespace AT_Csharp_2T_2S.Services;

public class ViewNotesService : IViewNotesService
{
    /*/ ------------------------------- CONFIGURANDO INJEÇÃO DE DEPENDÊNCIA ------------------------------- /*/
    //1) Para a Db
    private readonly QueViagemDbContext _context;
    
    //2) Para o logger
    private readonly Action<string>? _logger;
    
    //3) Para o arquivo
    private readonly string _folder;

    public ViewNotesService(QueViagemDbContext context, IWebHostEnvironment env, Action<string>? logger = null)
    {
        _context = context;
        _logger = logger;
        _folder = Path.Combine(env.WebRootPath, "files");

        if (!Directory.Exists(_folder))
            Directory.CreateDirectory(_folder);
    }

    /*/ ------------------------------- MÉTODOS ------------------------------- /*/
    //1) Para criar uma nota
    public async Task CriarNotaAsync(string conteudo)
    {
        _logger?.Invoke("Iniciando criação de nota...");

        var fileName = $"note_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        var filePath = Path.Combine(_folder, fileName);

        await File.WriteAllTextAsync(filePath, conteudo);

        _logger?.Invoke($"Nota criada com sucesso: {fileName}");
    }
    //--------------------------------------------/------------------------------------------
    
    //2) Para listar as notas
    public async Task<List<string>> ListarNotasAsync()
    {
        _logger?.Invoke("Listando as notas");

        return await Task.Run(() =>
            Directory.GetFiles(_folder, "*.txt")
                .Select(Path.GetFileName)
                .ToList()
        );
    }
    //--------------------------------------------/------------------------------------------
    
    //3) Para ler a nota
    public async Task<string?> LerNotaAsync(string fileName)
    {
        _logger?.Invoke($"Lendo nota: {fileName}");

        var filePath = Path.Combine(_folder, fileName);

        if (!File.Exists(filePath))
        {
            _logger?.Invoke("Arquivo não encontrado.");
            return null;
        }

        return await File.ReadAllTextAsync(filePath);
    }
}
