using WorkflowAutomation.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using WorkflowAutomation.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddHttpClient("WorkflowAutomation.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient("Authentication", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project

//builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WorkflowAutomation.ServerAPI"));

//builder.Services.AddOidcAuthentication(options =>
//{
//    builder.Configuration.Bind("oidc", options.ProviderOptions);
//});

builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddApiAuthorization();
builder.Services.AddAuthorizationCore(o =>
{
    o.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Админ"));
    o.AddPolicy("RegisterUserPolicy", policy => policy.RequireClaim("Зарегистрированный пользователь"));
});
builder.Services.AddScoped<SignOutSessionStateManager>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();
