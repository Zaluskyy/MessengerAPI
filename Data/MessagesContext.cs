using System;
using messenger.Entities;
using Microsoft.EntityFrameworkCore;

namespace messenger.Data
{
    public class MessagesContext : DbContext
    {
        public MessagesContext(DbContextOptions<MessagesContext> options) : base(options) { }

        public DbSet<Message> Messages => Set<Message>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new { Id = 1, Name = "Adolf", Password = "1234" },
                new { Id = 2, Name = "Janusz", Password = "2137" }
            );
            modelBuilder.Entity<Message>().HasData(
                new { Id = 1, SenderId = 1, ReceiverId = 2, Text = "Rucham ci mamę", Time = new DateTime(2001, 5, 9) },
                new { Id = 2, SenderId = 2, ReceiverId = 1, Text = "Twoja matka kurwa", Time = new DateTime(2001, 5, 10) }
            );
        }
    }
}

