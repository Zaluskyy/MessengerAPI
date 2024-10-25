using System;
using messenger.Dtos;
using messenger.Entities;

namespace messenger.Mapping;

public static class MessageMapping
{
    public static Message ToEntity(this CreateMessageDto message)
    {
        return new Message()
        {
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId,
            Text = message.Text,
        };
    }
    public static Message ToEntity(this UpdateMessageDto message, int id)
    {
        return new Message()
        {
            Id = id,
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId!,
            Text = message.Text,
            Time = message.Time
        };
    }
    public static MessageSummaryDto ToMessageSummaryDto(this Message message)
    {
        return new(
            message.Id,
            message.SenderId,
            message.ReceiverId,
            message.Text,
            message.Time
        );
    }
    public static MessageDetailDto ToMessageDetailsDto(this Message message)
    {
        return new(
        message.Id,
        message.Sender!.Name,
        message.Receiver!.Name,
        message.Sender!.Id,
        message.Receiver!.Id,
        message.Text,
        message.Time
        );
    }
}
