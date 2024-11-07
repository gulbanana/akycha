using Akycha.Model;
using Microsoft.AspNetCore.Mvc;

namespace Akycha;

static class IconEndpoints
{
    public static void Map(IEndpointRouteBuilder routes)
    {
        routes.MapGet("/Icon/Part/{id:int}", GetPartIcon);
        routes.MapGet("/Icon/Machine/{id:int}", GetMachineIcon);
    }

    public static async Task<IResult> GetPartIcon(int id, [FromServices] FactoryContext dbContext)
    {
        var part = await dbContext.Parts.FindAsync(id);
        if (part?.Icon is null)
        {
            return Results.File("Icon_Cross.png");
        }
        else
        {
            return Results.Bytes(part.Icon, "image/png", $"{part.Name}.png");
        }
    }

    public static async Task<IResult> GetMachineIcon(int id, [FromServices] FactoryContext dbContext)
    {
        var machine = await dbContext.Machines.FindAsync(id);
        if (machine?.Icon is null)
        {
            return Results.File("Icon_Cross.png");
        }
        else
        {
            return Results.Bytes(machine.Icon, "image/png", $"{machine.Name}.png");
        }
    }
}