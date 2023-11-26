using Microsoft.EntityFrameworkCore; 
using Microsoft.Data.SqlClient;
using Blanche.Shared.Products;
using Blanche.Shared.Reservations;
using Blanche.Server;
using Blanche.Server.Services.Products;
using Blanche.Server.Services.Reservations;
using Blanche.Server.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("Blanche"));
conStrBuilder.Password = builder.Configuration["LocalDbPassword"];
var connection = conStrBuilder.ConnectionString;

builder.Services.AddDbContext<BlancheDbContext>(
    options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
 

app.MapRazorPages(); 
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
