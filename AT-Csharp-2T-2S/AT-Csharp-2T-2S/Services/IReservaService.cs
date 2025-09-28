using AT_Csharp_2T_2S.Models;

namespace AT_Csharp_2T_2S.Services;

public interface IReservaService
{
    /*/ ------------------------------- MÉTODOS ------------------------------- /*/ 
    // # Usados pelo próprio Cliente #
    //1) Para cadastro do cliente 
    Task<Reserva?> CriarNovaReservaAsync(long clienteId, long pacoteTuristicoId);
    //--------------------------------------------/------------------------------------------
}