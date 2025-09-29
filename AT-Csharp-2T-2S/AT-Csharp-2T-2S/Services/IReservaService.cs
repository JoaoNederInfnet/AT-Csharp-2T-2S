using AT_Csharp_2T_2S.Models;

namespace AT_Csharp_2T_2S.Services;

public interface IReservaService
{
    /*/ ------------------------------- MÉTODOS ------------------------------- /*/ 
    // # Usados pela próprio Reserva#
    //---------------#---------------#---------------#---------------#---------------
    
    // # Usados fora da próprio Reserva#
    //#1) Para cadastro do reserva
    Task<Reserva?> CriarNovaReservaAsync(long clienteId, long pacoteTuristicoId);
    //--------------------------------------------/------------------------------------------
}