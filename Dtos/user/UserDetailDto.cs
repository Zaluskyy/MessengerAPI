using messenger.Entities;

namespace messenger.Dtos.user;

public record class UserDetailDto(
    int Id,
    string Name,
    string FriendCode,
    List<FriendDto>? FriendList = null
// string Password
);