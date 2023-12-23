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
using Blanche.Shared.Invoices;
using Blanche.Client.Invoice;
using MudBlazor;
/*using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Blanche.Client.Admin;*/

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

/*builder.Services.AddHttpClient("BlancheAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
       .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlancheAPI"));

builder.Services.AddHttpClient("BlancheAPI.NoAuthenticationClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlancheAPI.NoAuthenticationClient"));

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
}).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();*/

builder.Services.AddSidepanel();

builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFormulaService, FormulaService>();
builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddScoped<IStorageService, BlobStorageService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
  
builder.Services.AddMudServices();
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

await builder.Build().RunAsync();
