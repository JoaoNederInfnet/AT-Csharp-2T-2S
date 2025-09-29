using AT_Csharp_2T_2S.Services;
using AT_Csharp_2T_2S.ViewModels.ViewNotes;
using Microsoft.AspNetCore.Mvc;

namespace AT_Csharp_2T_2S.Controllers;

public class ViewNotesController : Controller
{
    private readonly IViewNotesService _viewNotesService;

    public ViewNotesController(IViewNotesService viewNotesService)
    {
        _viewNotesService = viewNotesService;
    }

    // GET: Notes
    public async Task<IActionResult> Index()
    {
        var files = await _viewNotesService.ListarNotasAsync();

        var vm = new ListViewNotesViewModel
        {
            Files = files,
            CreateNote = new CreateViewNoteViewModel()
        };

        return View(vm);
    }

    // POST: Notes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ListViewNotesViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Files = await _viewNotesService.ListarNotasAsync();
            return View("Index", model);
        }

        await _viewNotesService.CriarNotaAsync(model.CreateNote.NoteText);
        return RedirectToAction(nameof(Index));
    }

    // GET: Notes/Details/fileName
    public async Task<IActionResult> Details(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return NotFound();

        var conteudo = await _viewNotesService.LerNotaAsync(fileName);
        if (conteudo == null)
            return NotFound();

        var vm = new ListViewNotesViewModel
        {
            Files = await _viewNotesService.ListarNotasAsync(),
            FileName = fileName,
            FileContent = conteudo,
            CreateNote = new CreateViewNoteViewModel()
        };

        return View(vm);
    }
}