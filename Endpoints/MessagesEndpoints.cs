using System;
using messenger.Data;
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


        return group;
    }
}