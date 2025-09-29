using AT_Csharp_2T_2S.Models;
using AT_Csharp_2T_2S.ViewModels.PacoteTuristico;

namespace AT_Csharp_2T_2S.Services;

public interface IPacoteTuristicoService
{ 
   /*/ ------------------------------- MÉTODOS ------------------------------- /*/
   // # Usados pelo próprio Pacote Turistico#
   //#1) Para criar um novo pacote a partir do Input Model no CreateViewModel
   Task<PacoteTuristico?> CriarNovoPacoteTuristicoAsync(CreatePacoteTuristicoViewModel model);
   //--------------------------------------------/------------------------------------------
   
   //#2) Para achar um pacote pelo id 
   Task<PacoteTuristico?> PegarPacoteTuristicoCompletoAsync(long id);
   //--------------------------------------------/------------------------------------------
   
   //#3) Para criar um novo pacote a partir do Input Model no CreateViewModel
   Task<PacoteTuristico?> EditarPacoteTuristicoAsync(EditPacoteTuristicoViewModel model);
   //--------------------------------------------/------------------------------------------
   
   //#4) Para fazer a deleção lógica
   Task DeletarLogicamentePacoteTuristicoAsync(long id);
   //--------------------------------------------/------------------------------------------
   
   //#5) Para pegar a lista com todos os pacotes que não estão deletados 
   Task<List<PacoteTuristico>> PegarTodosPacotesTuristicosNaoDeletadosAsync();
   //--------------------------------------------/------------------------------------------
   
   //#6) Para pegar a lista com todos os pacotes selecionáveis por um cliente no momento presente
   Task<List<PacoteTuristico>> ListarPacotesTuristicosDisponiveisParaReservaAsync();
}