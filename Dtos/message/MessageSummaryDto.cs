namespace messenger.Dtos;

public record class MessageSummaryDto(
    int Id,
    string Sender,
    string Receiver,
    string Text,
    DateTime Time
);