using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using ReactCoreRegisterationPage.Server;
namespace ReactCoreRegisterationPage.Server.Controllers;

public static class RegisteredPersonEndpointsController
{
    public static void MapRegisteredPersonEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/RegisteredPerson").WithTags(nameof(RegisteredPerson));

        group.MapGet("/", async (RegisterationDbContext db) =>
        {
            return await db.RegisteredPersonEntities.ToListAsync();
        })
        .WithName("GetAllRegisteredPeople")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<RegisteredPerson>, NotFound>> (int personid, RegisterationDbContext db) =>
        {
            return await db.RegisteredPersonEntities.AsNoTracking()
                .FirstOrDefaultAsync(model => model.PersonID == personid)
                is RegisteredPerson model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetRegisteredPersonById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int personid, RegisteredPerson registeredPerson, RegisterationDbContext db) =>
        {
            var affected = await db.RegisteredPersonEntities
                .Where(model => model.PersonID == personid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.PersonID, registeredPerson.PersonID)
                    .SetProperty(m => m.FirstName, registeredPerson.FirstName)
                    .SetProperty(m => m.LastName, registeredPerson.LastName)
                    .SetProperty(m => m.Gender, registeredPerson.Gender)
                    .SetProperty(m => m.Company, registeredPerson.Company)
                    .SetProperty(m => m.EmailID, registeredPerson.EmailID)
                    .SetProperty(m => m.Phone, registeredPerson.Phone)
                    .SetProperty(m => m.Skills, registeredPerson.Skills)
                    .SetProperty(m => m.YearsOfExp, registeredPerson.YearsOfExp)
                    .SetProperty(m => m.LastUpdated, registeredPerson.LastUpdated)
                   
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateRegisteredPerson")
        .WithOpenApi();

        group.MapPost("/", async (RegisteredPerson registeredPerson, RegisterationDbContext db) =>
        {
            db.RegisteredPersonEntities.Add(registeredPerson);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/RegisteredPerson/{registeredPerson.PersonID}",registeredPerson);
        })
        .WithName("CreateRegisteredPerson")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int personid, RegisterationDbContext db) =>
        {
            var affected = await db.RegisteredPersonEntities
                .Where(model => model.PersonID == personid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteRegisteredPerson")
        .WithOpenApi();
    }
}
