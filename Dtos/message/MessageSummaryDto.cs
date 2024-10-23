namespace messenger.Dtos;

public record class MessageSummaryDto(
    int Id,
    int SenderId,
    int ReceiverId,
    string Text,
    DateTime Time
);