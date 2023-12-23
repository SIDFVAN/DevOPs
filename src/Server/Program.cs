using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Blanche.Shared.Products;
using Blanche.Shared.Reservations;
using Blanche.Shared.Formulas;
using Blanche.Server;
using Blanche.Server.Services.Products;
using Blanche.Server.Services.Reservations;
using Blanche.Server.Services.Formulas;
using Blanche.Server.Services.Files;
using Blanche.Server.Persistence;
using RazorLight;
using Blanche.Server.Services.Util;
using Blanche.Server.Util;
using Blanche.Shared.Invoices;
using Blanche.Server.Seeding;
using Blanche.Server.Services.Util.Mail;
using Blanche.Server.Services.Invoices;
/*using Microsoft.AspNetCore.Authentication.JwtBearer;*/


var builder = WebApplication.CreateBuilder(args);

//Add authentication to the container
/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Authority"];
    options.Audience = builder.Configuration["Auth0:ApiIdentifier"];
});*/

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IFormulaService, FormulaService>();
builder.Services.AddScoped<IStorageService, BlobStorageService>();
builder.Services.AddScoped<IReservationItemService, ReservationItemService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("Blanche"));
conStrBuilder.Password = builder.Configuration["LocalDbPassword"];
var connection = conStrBuilder.ConnectionString;

builder.Services.AddDbContext<BlancheDbContext>(options =>
{
    //options.UseMySql(connection, ServerVersion.AutoDetect(connection));
     options.UseSqlServer(connection);
});

builder.Services.AddControllersWithViews(opt => opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddRazorPages();

IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = true;
IronPdf.Logging.Logger.EnableDebugging = true;  
IronSoftware.Logger.LoggingMode = IronSoftware.Logger.LoggingModes.All;

var engine = new RazorLightEngineBuilder()
    .UseFileSystemProject(Path.Combine(Environment.CurrentDirectory, "Pages", "Templates"))
    .UseMemoryCachingProvider()
    .Build();

builder.Services.AddSingleton<IRazorLightEngine>(engine);

builder.Services.AddTransient<IPdfGeneratorService, PdfGeneratorService>();
builder.Services.AddTransient<IHtmlGenerationService, HtmlGeneratorService>();
builder.Services.AddTransient<IEmailService, EmailService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BlancheDbContext>();
    SeedHelper.SeedData(dbContext);
}

app.Run();
