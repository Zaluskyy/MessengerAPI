using System;

namespace messenger.Entities;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
    public string? FriendCode { get; set; }
    public List<User> FriendList { get; set; } = new List<User>();
    // public List<User> FriendList { get; set; } = [];
}
