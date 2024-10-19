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
            Name = user.Name
        };
    }
    public static User ToEntity(this UpdateUserDto user, int id)
    {
        return new User()
        {
            Id = id,
            Name = user.Name
        };
    }
    public static UserDetailDto ToUserDetailsDto(this User user)
    {
        return new(
            user.Id,
            user.Name
        );
    }
}
