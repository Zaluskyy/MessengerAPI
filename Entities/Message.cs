using System;

namespace messenger.Entities;

public class Message
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public User? Sender { get; set; }
    public User? Receiver { get; set; }
    public required string Text { get; set; }
    public DateTime Time { get; set; }
}
