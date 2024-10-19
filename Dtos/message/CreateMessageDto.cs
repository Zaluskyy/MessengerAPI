using System.ComponentModel.DataAnnotations;

namespace messenger.Dtos;

public record class CreateMessageDto(
    int Id,
    [Required] int SenderId,
    [Required] int ReceiverId,
    [Required] string Text,
    [Required] DateTime Time
);
