using AT_Csharp_2T_2S.Models;
using Microsoft.EntityFrameworkCore;

namespace AT_Csharp_2T_2S.Data;

public class QueViagemDbContext : DbContext
{

    public QueViagemDbContext(DbContextOptions<QueViagemDbContext> options) : base(options)
    {
        
    }
    //========================================================
   
    /*/ ------------------------------- TABELAS DE DADOS ------------------------------- /*/
    //1) Clientes
    public DbSet<Cliente> Clientes { get; set; }
    //--------------------------------------------/------------------------------------------
   
    //2) Cidades Destino
    public DbSet<CidadeDestino> CidadesDestino { get; set; }
    //--------------------------------------------/------------------------------------------
   
    //3) Paises Destino
    public DbSet<PaisDestino> PaisesDestino { get; set; }
    //--------------------------------------------/------------------------------------------
   
    //4) Pacotes Turisticos
    public DbSet<PacoteTuristico> PacotesTuristicos { get; set; }
    //--------------------------------------------/------------------------------------------
   
    //5) Reservas
    public DbSet<Reserva> Reservas { get; set; }
    //========================================================
    
    /*/ ------------------------------- MÉTODOS ------------------------------- /*/
    //#) Para
    //#1) Criar os dados iniciais
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //•1) Data seeding (configurações iniciais do sistema
        //A) Para os paises destino
        modelBuilder.Entity<PaisDestino>().HasData(
            new PaisDestino("Brasil") { Id = 1L},
            new PaisDestino("França") { Id = 2L},
            new PaisDestino("Japão") { Id = 3L},
            new PaisDestino("China") { Id = 4L}
        );
        //--------------------------------------------/------------------------------------------
        
        //2) Para as cidades destino
        modelBuilder.Entity<CidadeDestino>().HasData(
            new CidadeDestino("Rio de Janeiro", 1) { Id = 1L},
            new CidadeDestino("Paris", 2) { Id = 2L},
            new CidadeDestino("Tokyo", 3) { Id = 3L},
            new CidadeDestino("Hong Kong", 4) { Id = 4L}
        );
        //--------------------------------------------/------------------------------------------
        
        //3) Para os pacotes turisticos
        modelBuilder.Entity<PacoteTuristico>().HasData(
        //A) Para o Rio 
        new { Id = 1L, Titulo = "Comer", DataInicio = new DateTime(2026, 2, 25), DuracaoEmDias = 7, CapacidadeMaxima = 100, PrecoPorDiaria = 550.00m },
        new { Id = 2L, Titulo = "Beber", DataInicio = new DateTime(2025, 12, 29), DuracaoEmDias = 5, CapacidadeMaxima = 150, PrecoPorDiaria = 750.00m },
        new { Id = 3L, Titulo = "Cair", DataInicio = new DateTime(2025, 10, 10), DuracaoEmDias = 4, CapacidadeMaxima = 50,  PrecoPorDiaria = 400.00m },
        new { Id = 4L, Titulo = "Levantar", DataInicio = new DateTime(2025, 11, 15), DuracaoEmDias = 3, CapacidadeMaxima = 30,  PrecoPorDiaria = 600.00m },
        //----------------------------------//--------------------------------
        
        //B) Para Paris
        new { Id = 5L, Titulo = "Manger", DataInicio = new DateTime(2026, 2, 12), DuracaoEmDias = 5, CapacidadeMaxima = 40,  PrecoPorDiaria = 800.00m },
        new { Id = 6L, Titulo = "Boire", DataInicio = new DateTime(2025, 12, 20), DuracaoEmDias = 6, CapacidadeMaxima = 60,  PrecoPorDiaria = 950.00m },
        new { Id = 7L, Titulo = "Tomber", DataInicio = new DateTime(2025, 10, 5), DuracaoEmDias = 7, CapacidadeMaxima = 30,  PrecoPorDiaria = 700.00m },
        new { Id = 8L, Titulo = "Se lever", DataInicio = new DateTime(2026, 7, 15), DuracaoEmDias = 10, CapacidadeMaxima = 80, PrecoPorDiaria = 1100.00m },
        //----------------------------------//--------------------------------
        
        //C) Para Tokyo
        new { Id = 9L, Titulo = "Taberu", DataInicio = new DateTime(2026, 4, 1), DuracaoEmDias = 8, CapacidadeMaxima = 25, PrecoPorDiaria = 1500.00m },
        new { Id = 10L, Titulo = "Nomu", DataInicio = new DateTime(2026, 3, 20), DuracaoEmDias = 7, CapacidadeMaxima = 50, PrecoPorDiaria = 1800.00m },
        new { Id = 11L, Titulo = "Ochiru", DataInicio = new DateTime(2025, 11, 1), DuracaoEmDias = 5, CapacidadeMaxima = 40, PrecoPorDiaria = 1300.00m },
        new { Id = 12L, Titulo = "Tatsu", DataInicio = new DateTime(2025, 12, 28), DuracaoEmDias = 6, CapacidadeMaxima = 100, PrecoPorDiaria = 2000.00m },
        //----------------------------------//--------------------------------
        
        //D) Para Hong Kong
        new { Id = 13L, Titulo = "Chī", DataInicio = new DateTime(2025, 10, 15), DuracaoEmDias = 5, CapacidadeMaxima = 70, PrecoPorDiaria = 1200.00m },
        new { Id = 14L, Titulo = "Hē", DataInicio = new DateTime(2026, 1, 20), DuracaoEmDias = 6, CapacidadeMaxima = 40, PrecoPorDiaria = 1000.00m },
        new { Id = 15L, Titulo = "Shuāidǎo", DataInicio = new DateTime(2026, 7, 5), DuracaoEmDias = 7, CapacidadeMaxima = 50, PrecoPorDiaria = 1400.00m },
        new { Id = 16L, Titulo = "Qǐlái", DataInicio = new DateTime(2025, 11, 25), DuracaoEmDias = 4, CapacidadeMaxima = 20, PrecoPorDiaria = 2500.00m }
        );
        //--------------------------------------------/------------------------------------------
        
        //4) Para conectar as cidades aos seus pacotes turísticos respectivos
        modelBuilder.Entity("CidadeDestinoPacoteTuristico").HasData(
            //A) Para o Rio 
            new { DestinosId = 1L, PacotesTuristicosId = 1L },
            new { DestinosId = 1L, PacotesTuristicosId = 2L },
            new { DestinosId = 1L, PacotesTuristicosId = 3L },
            new { DestinosId = 1L, PacotesTuristicosId = 4L },
            //----------------------------------//--------------------------------
        
            //B) Para Paris
            new { DestinosId = 2L, PacotesTuristicosId = 5L },
            new { DestinosId = 2L, PacotesTuristicosId = 6L },
            new { DestinosId = 2L, PacotesTuristicosId = 7L },
            new { DestinosId = 2L, PacotesTuristicosId = 8L },
            //----------------------------------//--------------------------------
        
            //C) Para Tokyo
            new { DestinosId = 3L, PacotesTuristicosId = 9L },
            new { DestinosId = 3L, PacotesTuristicosId = 10L },
            new { DestinosId = 3L, PacotesTuristicosId = 11L },
            new { DestinosId = 3L, PacotesTuristicosId = 12L },
            //----------------------------------//--------------------------------
        
            //D) Para Hong Kong
            new { DestinosId = 4L, PacotesTuristicosId = 13L },
            new { DestinosId = 4L, PacotesTuristicosId = 14L },
            new { DestinosId = 4L, PacotesTuristicosId = 15L },
            new { DestinosId = 4L, PacotesTuristicosId = 16L }
        );
    }
    //---------------#---------------#---------------#---------------#---------------
    
    //2) Para sobrescrever o SaveChangesAsync do EF e fazer com que, além de salvar os novos dados no sistema, ele também registre a data do salvamento
    public override Task<int> SaveChangesAsync(CancellationToken sinalizador = default)
    {
        //•ETAPAS•//
        //•1) Encontrando todos os dados pertencentes aos DbSets que foram adicionados ou alterados 
        var dados = ChangeTracker
            .Entries()
            .Where(d => d.Entity is Model && 
                        ( 
                            d.State == EntityState.Added 
                            ||
                            d.State == EntityState.Modified
                        ));
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
      
        //•2) Definindo a data de adição/alteração dos dados encontrados como a data no momento da modificação
        DateTime agora = DateTime.UtcNow;
        
        foreach (var dado in dados)
        {
            var abstractModelPai = (Model)dado.Entity;
         
            abstractModelPai.AtualizadoEm = agora;

         
            if (dado.State == EntityState.Added)
            {
                abstractModelPai.CriadoEm = agora;
            }
        }
        //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••
        
        //•3) Chamando o SaveChangesAsync original (do EF) para fazer o salvamento padrão dos dados modificados no sistema se o sinalizador tiver o valor default 
        return base.SaveChangesAsync(sinalizador);
    }
}