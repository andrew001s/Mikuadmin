using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Protov4.DAO;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Configuraci�n para acceder a la cadena de conexi�n desde appsettings.json
IConfiguration configuration = builder.Configuration;
builder.Services.AddSingleton(new DbConnection(configuration));
builder.Services.AddTransient<UsuariosDAO>();
builder.Services.AddSingleton<IMongoClient>(new MongoClient(configuration.GetConnectionString("MongoDBConnection")));
builder.Services.AddScoped<DBMongo>();
builder.Services.AddScoped<MikuTechFactory, MikutechDAO>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();