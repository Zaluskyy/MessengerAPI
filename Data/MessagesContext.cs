using System;
using messenger.Entities;
using messenger.Generate;
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
            modelBuilder.Entity<User>()
                .HasMany(u => u.FriendList)
                .WithMany()
                .UsingEntity(j => j.ToTable("Friends"));
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<User>().HasData(
        //         new { Id = 1, Name = "Adolf", Password = "1234", FriendCode = "12345" },
        //         new { Id = 2, Name = "Janusz", Password = "2137", FriendCode = "123456" }
        //     );
        //     modelBuilder.Entity<Message>().HasData(
        //         new { Id = 1, SenderId = 1, ReceiverId = 2, Text = "Siema", Time = new DateTime(2001, 5, 9) },
        //         new { Id = 2, SenderId = 2, ReceiverId = 1, Text = "Elo", Time = new DateTime(2001, 5, 10) }
        //     );
        // }
    }
}

