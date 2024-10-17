using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Akycha;

static class AccountEndpoints
{
    public record LoginModel(string Password);

    public static async Task<IResult> LoginAsync(HttpContext context, IOptions<AkychaOptions> options, [FromForm] LoginModel model)
    {
        if (options.Value.Password is not null && model.Password == options.Value.Password)
        {
            await context.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.Name, "username")], "password")), new()
            {
                IsPersistent = true,
            });
        }

        return RedirectToReferer(context);
    }

    public static async Task<IResult> LogoutAsync(HttpContext context)
    {
        await context.SignOutAsync();

        return RedirectToReferer(context);
    }

    private static IResult RedirectToReferer(HttpContext context)
    {
        var referer = context.Request.Headers["Referer"].ToString();

        if (string.IsNullOrEmpty(referer))
        {
            referer = "/";
        }

        return Results.Redirect(referer);
    }
}