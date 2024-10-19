using System;

namespace messenger.Entities;

public class Message
{
    public int Id { get; set; }
    public required string SenderId { get; set; }
    public required string ReceiverId { get; set; }
    public required string Tex { get; set; }
}
