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
        }
    }
}

