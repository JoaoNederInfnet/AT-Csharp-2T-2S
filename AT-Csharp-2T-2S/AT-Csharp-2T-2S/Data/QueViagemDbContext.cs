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

        //todo Se der tempo eu faço tudo explicitamente
        // //•1) Definindo as hierarquias das tabelas de dados
        // //A) Para o cliente
        // modelBuilder.Entity<Cliente>()
        //     .HasMany(c => c.Reservas)
        //     .WithOne(r => r.Cliente)
        //     .HasForeignKey(r => r.ClienteId);
        // //•••••••••••••••••••••••••••••••••••••••••••••••••••••••••

        //•2) Data seeding (configurações iniciais do sistema
        
        var seedDate = new DateTime(2025, 9, 28, 0, 0, 0, DateTimeKind.Utc);
        
        //A) Para os paises destino
        modelBuilder.Entity<PaisDestino>().HasData(
            new PaisDestino("Brasil") { Id = 1L, CriadoEm = seedDate, AtualizadoEm = seedDate },
            new PaisDestino("França") { Id = 2L, CriadoEm = seedDate, AtualizadoEm = seedDate },
            new PaisDestino("Japão") { Id = 3L, CriadoEm = seedDate, AtualizadoEm = seedDate },
            new PaisDestino("China") { Id = 4L, CriadoEm = seedDate, AtualizadoEm = seedDate }
        );
        //--------------------------------------------/------------------------------------------
        
        //B) Para as cidades destino
        modelBuilder.Entity<CidadeDestino>().HasData(
            new CidadeDestino("Rio de Janeiro", 1L) { Id = 1L, CriadoEm = seedDate, AtualizadoEm = seedDate },
            new CidadeDestino("Paris", 2L) { Id = 2L, CriadoEm = seedDate, AtualizadoEm = seedDate },
            new CidadeDestino("Tóquio", 3L) { Id = 3L, CriadoEm = seedDate, AtualizadoEm = seedDate },
            new CidadeDestino("Hong Kong", 4L) { Id = 4L, CriadoEm = seedDate, AtualizadoEm = seedDate }
        );
        //--------------------------------------------/------------------------------------------
        
        //C) Para os pacotes turisticos
        modelBuilder.Entity<PacoteTuristico>().HasData(
        //a) Para o Rio 
        new { Id = 1L, Titulo = "Comer", DataInicio = new DateTime(2026, 2, 25), Dias = 7, CapacidadeMaxima = 100, Preco = 550.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 2L, Titulo = "Beber", DataInicio = new DateTime(2025, 12, 29), Dias = 5, CapacidadeMaxima = 150, Preco = 750.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 3L, Titulo = "Cair", DataInicio = new DateTime(2025, 10, 10), Dias = 4, CapacidadeMaxima = 50,  Preco = 400.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 4L, Titulo = "Levantar", DataInicio = new DateTime(2025, 11, 15), Dias = 3, CapacidadeMaxima = 30,  Preco = 600.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        //----------------------------------//--------------------------------
        
        //b) Para Paris
        new { Id = 5L, Titulo = "Manger", DataInicio = new DateTime(2026, 2, 12), Dias = 5, CapacidadeMaxima = 40,  Preco = 800.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 6L, Titulo = "Boire", DataInicio = new DateTime(2025, 12, 20), Dias = 6, CapacidadeMaxima = 60,  Preco = 950.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 7L, Titulo = "Tomber", DataInicio = new DateTime(2025, 10, 5), Dias = 7, CapacidadeMaxima = 30,  Preco = 700.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 8L, Titulo = "Se lever", DataInicio = new DateTime(2026, 7, 15), Dias = 10, CapacidadeMaxima = 80, Preco = 1100.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        //----------------------------------//--------------------------------
        
        //c) Para Tokyo
        new { Id = 9L, Titulo = "Taberu", DataInicio = new DateTime(2026, 4, 1), Dias = 8, CapacidadeMaxima = 25, Preco = 1500.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 10L, Titulo = "Nomu", DataInicio = new DateTime(2026, 3, 20), Dias = 7, CapacidadeMaxima = 50, Preco = 1800.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 11L, Titulo = "Ochiru", DataInicio = new DateTime(2025, 11, 1), Dias = 5, CapacidadeMaxima = 40, Preco = 1300.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 12L, Titulo = "Tatsu", DataInicio = new DateTime(2025, 12, 28), Dias = 6, CapacidadeMaxima = 100, Preco = 2000.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        //----------------------------------//--------------------------------
        
        //d) Para Hong Kong
        new { Id = 13L, Titulo = "Chī", DataInicio = new DateTime(2025, 10, 15), Dias = 5, CapacidadeMaxima = 70, Preco = 1200.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 14L, Titulo = "Hē", DataInicio = new DateTime(2026, 1, 20), Dias = 6, CapacidadeMaxima = 40, Preco = 1000.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 15L, Titulo = "Shuāidǎo", DataInicio = new DateTime(2026, 7, 5), Dias = 7, CapacidadeMaxima = 50, Preco = 1400.00m, CriadoEm = seedDate, AtualizadoEm = seedDate },
        new { Id = 16L, Titulo = "Qǐlái", DataInicio = new DateTime(2025, 11, 25), Dias = 4, CapacidadeMaxima = 20, Preco = 2500.00m, CriadoEm = seedDate, AtualizadoEm = seedDate }
        );
        //--------------------------------------------/------------------------------------------
        
        //D) Para conectar as cidades aos seus pacotes turísticos respectivos
        modelBuilder.Entity("CidadeDestinoPacoteTuristico").HasData(
            //a) Para o Rio 
            new { DestinosId = 1L, PacotesTuristicosId = 1L },
            new { DestinosId = 1L, PacotesTuristicosId = 2L },
            new { DestinosId = 1L, PacotesTuristicosId = 3L },
            new { DestinosId = 1L, PacotesTuristicosId = 4L },
            //----------------------------------//--------------------------------
        
            //b) Para Paris
            new { DestinosId = 2L, PacotesTuristicosId = 5L },
            new { DestinosId = 2L, PacotesTuristicosId = 6L },
            new { DestinosId = 2L, PacotesTuristicosId = 7L },
            new { DestinosId = 2L, PacotesTuristicosId = 8L },
            //----------------------------------//--------------------------------
        
            //c) Para Tokyo
            new { DestinosId = 3L, PacotesTuristicosId = 9L },
            new { DestinosId = 3L, PacotesTuristicosId = 10L },
            new { DestinosId = 3L, PacotesTuristicosId = 11L },
            new { DestinosId = 3L, PacotesTuristicosId = 12L },
            //----------------------------------//--------------------------------
        
            //d) Para Hong Kong
            new { DestinosId = 4L, PacotesTuristicosId = 13L },
            new { DestinosId = 4L, PacotesTuristicosId = 14L },
            new { DestinosId = 4L, PacotesTuristicosId = 15L },
            new { DestinosId = 4L, PacotesTuristicosId = 16L }
        );
    }
    //---------------#---------------#---------------#---------------#---------------
    
    //#2) Para sobrescrever o SaveChangesAsync do EF e fazer com que, além de salvar os novos dados no sistema, ele também registre a data do salvamento
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