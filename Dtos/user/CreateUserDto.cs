using System.ComponentModel.DataAnnotations;

namespace messenger.Dtos.user;

public record class CreateUserDto(
    int Id,
    [Required][StringLength(15)] string Name
);