using System;

namespace messenger.Endpoints;

public static class MessagesEndpoints
{
    public static RouteGroupBuilder MapMessagesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("messages");



        return group;
    }
}