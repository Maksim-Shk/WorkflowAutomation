using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using WorkflowAutomation.Client;
using WorkflowAutomation.Client.Services;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddHttpClient(WorkflowHttpClientDefaults.Default, client =>
    {
        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    });

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(WorkflowHttpClientDefaults.Default));

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore(o =>
{
    o.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Админ"));
    o.AddPolicy("RegisterUserPolicy", policy => policy.RequireClaim("Зарегистрированный пользователь"));
});
builder.Services.AddScoped<SignOutSessionStateManager>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();
