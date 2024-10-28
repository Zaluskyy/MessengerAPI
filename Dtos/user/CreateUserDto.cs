using System.ComponentModel.DataAnnotations;
using messenger.Entities;

namespace messenger.Dtos.user;

public record class CreateUserDto(
    int Id,
    [Required][StringLength(15)] string Name,
    [Required] string Password,
    string FriendCode,
    List<User> FriendList
);