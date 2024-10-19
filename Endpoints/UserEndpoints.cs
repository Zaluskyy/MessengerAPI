using System;
using messenger.Data;
using messenger.Dtos.user;
using messenger.Entities;
using messenger.Mapping;
using Microsoft.EntityFrameworkCore;

namespace messenger.Endpoints;

public static class UsersEndpoints
{

    const string GetUserEndpointName = "GetUser";
    public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("users");

        group.MapGet("/", async (MessagesContext dbContext) =>
            await dbContext.Users.AsNoTracking().ToListAsync()
        );

        group.MapGet("/{id}", async (int id, MessagesContext dbContext) =>
        {
            User? user = await dbContext.Users.FindAsync(id);

            return user is null ?
            Results.NotFound() : Results.Ok(user.ToUserDetailsDto());
        })
        .WithName(GetUserEndpointName);

        group.MapPost("/", async (CreateUserDto newUser, MessagesContext dbcontext) =>
        {
            User user = newUser.ToEntity();

            dbcontext.Users.Add(user);
            await dbcontext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetUserEndpointName, new { id = user.Id }, user.ToUserDetailsDto());
        });

        group.MapPut("/{id}", async (int id, UpdateUserDto updatedUser, MessagesContext dbContext) =>
        {

            var existingUser = await dbContext.Users.FindAsync(id);

            if (existingUser is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingUser).CurrentValues.SetValues(updatedUser.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();

        });

        group.MapDelete("/{id}", async (int id, MessagesContext dbContext) =>
        {
            await dbContext.Users.Where(user => user.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
