using System;
using messenger.Data;
using messenger.Dtos.user;
using messenger.Entities;
using messenger.Mapping;
using Microsoft.EntityFrameworkCore;
using messenger.Generate;
using messenger.Migrations;

namespace messenger.Endpoints;

public static class UsersEndpoints
{

    const string GetUserEndpointName = "GetUser";
    public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("users");

        group.MapGet("/", async (MessagesContext dbContext) =>
            await dbContext.Users.Include(u => u.FriendList).AsNoTracking().Select(user => user.ToUserDetailsDto()).ToListAsync()
        );

        group.MapGet("/{id}", async (int id, MessagesContext dbContext) =>
        {
            User? user = await dbContext.Users.Include(u => u.FriendList).FirstOrDefaultAsync(u => u.Id == id);

            return user is null ?
            Results.NotFound() : Results.Ok(user.ToUserDetailsDto());
        })
        .WithName(GetUserEndpointName);

        // add new user
        group.MapPost("/", async (CreateUserDto newUser, MessagesContext dbcontext) =>
        {
            User user = newUser.ToEntity();
            user.FriendCode = await FriendCode.GenerateUniqueFriendCode(dbcontext);

            dbcontext.Users.Add(user);
            await dbcontext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetUserEndpointName, new { id = user.Id }, user.ToUserDetailsDto());
        });

        // add friend
        //   addfriend/4/12345
        group.MapPut("/addfriend/{userId}/{friendCode}", async (int userId, string friendCode, MessagesContext dbContext) =>
        {

            var user = await dbContext.Users.Include(u => u.FriendList).FirstOrDefaultAsync(u => u.Id == userId);

            var friend = await dbContext.Users.Include(u => u.FriendList).FirstOrDefaultAsync(u => u.FriendCode == friendCode);
            if (user is null)
            {
                return Results.NotFound("User not found");
            }
            if (friend is null)
            {
                return Results.NotFound("Friend not found");
            }
            if (user.FriendList.Contains(friend) || friend.FriendList.Contains(user))
            {
                return Results.BadRequest("Already friends");
            }
            if (friend == user)
            {
                return Results.BadRequest("You cant be friend with yourself");
            }

            user.FriendList.Add(friend);
            friend.FriendList.Add(user);

            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.Entry(friend).State = EntityState.Modified;

            await dbContext.SaveChangesAsync();

            return Results.Ok("Friend added successfully");

        });


        // group.MapPut("/{id}", async (int id, UpdateUserDto updatedUser, MessagesContext dbContext) =>
        // {

        //     var existingUser = await dbContext.Users.FindAsync(id);

        //     if (existingUser is null)
        //     {
        //         return Results.NotFound();
        //     }

        //     dbContext.Entry(existingUser).CurrentValues.SetValues(updatedUser.ToEntity(id));

        //     await dbContext.SaveChangesAsync();

        //     return Results.NoContent();

        // });


        app.MapPost("/login", async (LoginDto loginDto, MessagesContext dbContext, HttpContext httpContext) =>
        {
            var user = await dbContext.Users.Include(u => u.FriendList).FirstOrDefaultAsync(user => user.Name == loginDto.Name && user.Password == loginDto.Password);

            if (user != null)
            {
                httpContext.Session.SetString("UserId", user.Id.ToString());
                return Results.Ok(new { Message = "Zalogowano opmyślnie", name = user.Name, id = user.Id, friendcode = user.FriendCode, friendlist = user.FriendList?.Select(f => new { f.Id, f.Name }).ToList() ?? [] });
            }
            return Results.Unauthorized();

        });

        app.MapPost("/logout", (HttpContext context) =>
        {
            context.Session.Clear();
            context.Response.Cookies.Delete(".AspNetCore.Session");
            return Results.Ok(new { message = "wylogowano pomyślnie" });
        });

        app.MapGet("/check-auth", async (HttpContext context, MessagesContext dbContext) =>
        {
            var userId = context.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return Results.Unauthorized();
            }

            var userIdInt = int.Parse(userId);

            var user = await dbContext.Users.Include(u => u.FriendList).FirstOrDefaultAsync(u => u.Id == userIdInt);

            if (user == null)
            {
                return Results.Unauthorized();
            }

            return Results.Ok(new { isAuthenticated = true, name = user.Name, id = user.Id, friendcode = user.FriendCode, friendlist = user.FriendList?.Select(f => new { f.Id, f.Name }).ToList() ?? [] });
        });

        return group;
    }
}




// group.MapPut("/{id}", async (int id, UpdateUserDto updatedUser, MessagesContext dbContext) =>
// {

//     var existingUser = await dbContext.Users.FindAsync(id);

//     if (existingUser is null)
//     {
//         return Results.NotFound();
//     }

//     dbContext.Entry(existingUser).CurrentValues.SetValues(updatedUser.ToEntity(id));

//     await dbContext.SaveChangesAsync();

//     return Results.NoContent();

// });

// group.MapDelete("/{id}", async (int id, MessagesContext dbContext) =>
// {
//     await dbContext.Users.Where(user => user.Id == id).ExecuteDeleteAsync();

//     return Results.NoContent();
// });