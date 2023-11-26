using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blanche.Client;
using MudBlazor.Services;
using Blanche.Shared.Reservations;
using Blanche.Client.Reservations;
using Blazored.SessionStorage;
using Blanche.Shared.Products;
using Blanche.Client.Products;
using Blanche.Shared.Formulas;
using Blanche.Client.Formulas;
using Blanche.Client.Admin.Products;
using Blanche.Client.Files;
using Append.Blazor.Sidepanel;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSidepanel();

builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFormulaService, FormulaService>();
builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddScoped<IStorageService, BlobStorageService>();

builder.Services.AddMudServices();
builder.Services.AddBlazoredSessionStorage();

await builder.Build().RunAsync();
