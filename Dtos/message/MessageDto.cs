namespace messenger.Dtos;

public record class MessageDto(
    int Id,
    string SenderId,
    string ReceiverId,
    string Text
);