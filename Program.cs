using System.Security.Claims;
using Akycha.Components;
using Akycha.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddDbContextFactory<FactoryContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString(nameof(FactoryContext))));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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
app.UseAntiforgery();

app.UseAuthentication();
app.MapPost("/Account/Login", async (HttpContext context, [FromForm] LoginModel model) => { 
    if (model.Password == "naspw")
    {
        await context.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.Name, "username")], "password")), new()
        {
            IsPersistent = true,
        });
    }
    return Results.Redirect("/");
});
app.MapPost("/Account/Logout", async (HttpContext context) => {
    await context.SignOutAsync();
    return Results.Redirect("/");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

record LoginModel(string Password);
