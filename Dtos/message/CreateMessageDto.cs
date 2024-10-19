using System.ComponentModel.DataAnnotations;

namespace messenger.Dtos;

public record class CreateMessageDto(
    int Id,
    [Required] string SenderId,
    [Required] string ReceiverId,
    [Required] string Text
);
