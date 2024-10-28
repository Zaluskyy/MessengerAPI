using System;
using messenger.Data;
using Microsoft.EntityFrameworkCore;

namespace messenger.Generate;

public static class FriendCode
{
    public static async Task<string> GenerateUniqueFriendCode(MessagesContext dbcontext)
    {
        string code;
        Random random = new();

        do
        {
            // Generate a random 8-character alphanumeric string
            code = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());

        } while (await dbcontext.Users.AnyAsync(u => u.FriendCode == code)); // Ensure it's unique

        return code;
    }
}
