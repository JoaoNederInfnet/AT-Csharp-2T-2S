using AT_Csharp_2T_2S.Data;
using AT_Csharp_2T_2S.Services;
using AT_Csharp_2T_2S.Services.Delegates_Events;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<MetodosLog>();//

builder.Services.AddScoped<IPacoteTuristicoService, PacoteTuristicoService>();//

builder.Services.AddScoped<IReservaService, ReservaService>();//

builder.Services.AddScoped<IViewNotesService, ViewNotesService>();//

// /*/ ------------------------------- CONFIGURANDO A DATABASE ------------------------------- /*/
//1)Adicionando o DbContext
builder.Services.AddDbContext<QueViagemDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

/*/ ------------------------------- CONFIGURANDO OS DELEGATES ------------------------------- /*/
builder.Services.AddSingleton<Action<string>>(serviceProvider =>
{
    var logger = serviceProvider.GetRequiredService<MetodosLog>();
    
    Action<string> logAction = logger.LogToConsole; 
    logAction += logger.LogToFile;                  
    logAction += logger.LogToMemory;                
    
    return logAction;
});
//========================================================


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();