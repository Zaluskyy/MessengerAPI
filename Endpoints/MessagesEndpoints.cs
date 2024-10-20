using System;
using messenger.Data;
using messenger.Dtos;
using messenger.Entities;
using messenger.Mapping;
using Microsoft.EntityFrameworkCore;

namespace messenger.Endpoints;

public static class MessagesEndpoints
{
    const string GetMessageEndpointName = "GetMessages";
    public static RouteGroupBuilder MapMessagesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("messages");

        group.MapGet("/", async (MessagesContext dbContext) =>
            await dbContext.Messages.Include(message => message.Sender).Include(message => message.Receiver).Select(message => message.ToMessageSummaryDto()).AsNoTracking().ToListAsync()
        );

        group.MapGet("/{id}", async (int id, MessagesContext dbContext) =>
        {
            Message? message = await dbContext.Messages.FindAsync(id);

            return message is null ?
            Results.NotFound() : Results.Ok(message.ToMessageDetailsDto());
        })
        .WithName(GetMessageEndpointName);


        group.MapPost("/", async (CreateMessageDto newMessage, MessagesContext dbcontext) =>
        {
            Message message = newMessage.ToEntity();
            message.Time = DateTime.Now;

            dbcontext.Messages.Add(message);
            await dbcontext.SaveChangesAsync();

            return Results.CreatedAtRoute(GetMessageEndpointName, new { id = message.Id }, message.ToMessageDetailsDto());
        });

        return group;
    }
}