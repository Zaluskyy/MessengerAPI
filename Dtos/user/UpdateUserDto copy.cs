using System.ComponentModel.DataAnnotations;

namespace messenger.Dtos.user;

public record class UpdateUserDto(
    int Id,
    [Required][StringLength(15)] string Name,
    [Required] string Password
);