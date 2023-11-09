using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blanche.Client;
using MudBlazor.Services;
using Blanche.Shared.Reservations;
using Blanche.Client.Reservations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
