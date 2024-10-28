using System;
using messenger.Dtos.user;
using messenger.Entities;

namespace messenger.Mapping;

public static class UserMapping
{
    public static User ToEntity(this CreateUserDto user)
    {
        return new User()
        {
            Id = user.Id,
            Name = user.Name,
            Password = user.Password,
            FriendList = user.FriendList
        };
    }
    public static User ToEntity(this UpdateUserDto user, int id)
    {
        return new User()
        {
            Id = id,
            Name = user.Name,
            Password = user.Password
        };
    }
    public static UserDetailDto ToUserDetailsDto(this User user)
    {
        return new(
            user.Id,
            user.Name,
            user.FriendCode!,
            user.FriendList?.Select(f => new FriendDto(f.Id, f.Name)).ToList() ?? []
        // user.Password
        );
    }
}
