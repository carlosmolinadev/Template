using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SimpleTemplate.Client;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SimpleTemplate.Client.Auth;
using SimpleTemplate.Client.Services;
using SimpleTemplate.Client.Contracts;
using SimpleTemplate.Client.Services.Base;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddTransient<CustomAuthorizationMessageHandler>();
builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

builder.Services.AddMudServices();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


await builder.Build().RunAsync();
