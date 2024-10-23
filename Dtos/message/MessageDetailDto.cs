using messenger.Entities;

namespace messenger.Dtos;

public record class MessageDetailDto(
    int Id,
    string SenderName,
    string ReceiverName,
    int SenderId,
    int ReceiverId,
    string Text,
    DateTime Time
);
// int SenderId,
// int ReceiverId,
// string Sender,
// string Receiver,