namespace messenger.Dtos;

public record class MessageDetailDto(
    int Id,
    int SenderId,
    int ReceiverId,
    string Text,
    DateTime Time
);