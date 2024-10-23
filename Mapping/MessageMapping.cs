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
        message.Sender!.Name, // uzyskujemy nazwę nadawcy
        message.Receiver!.Name, // uzyskujemy nazwę odbiorcy
        message.Sender!.Id, // uzyskujemy nazwę nadawcy
        message.Receiver!.Id, // uzyskujemy nazwę odbiorcy
        message.Text,
        message.Time
        );
    }
    // message.SenderId, // poprawka: to jest int, więc bez .Id
    // message.ReceiverId, // poprawka: to jest int, więc bez .Id

    // return new(
    //     message.Id,

    //     message.Sender!.Name,
    //     message.SenderId,
    //     message.Receiver!.Name,
    //     message.Receiver,

    //     message.Text,
    //     message.Time
    // );

}
