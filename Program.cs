using Akycha;
using Akycha.Components;
using Akycha.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddDbContextFactory<FactoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(FactoryContext))));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<AkychaOptions>(builder.Configuration.GetRequiredSection("Akycha"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseStaticFiles();
app.UseAuthentication();
app.UseAntiforgery();

app.MapPost("/Account/Login", AccountEndpoints.LoginAsync);
app.MapPost("/Account/Logout", (Delegate)AccountEndpoints.LogoutAsync);

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapGet("/", () => Results.Redirect("/Facilities"));

app.Run();
