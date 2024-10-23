using messenger.Entities;

namespace messenger.Dtos;

public record class MessageDetailDto(
    int Id,
// User Sender,
int SenderId,
int ReceiverId,
string Sender,
string Receiver,
    // User Receiver,
    string Text,
    DateTime Time
);