﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using messenger.Data;

#nullable disable

namespace messenger.Migrations
{
    [DbContext(typeof(MessagesContext))]
    partial class MessagesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("messenger.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tex")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("messenger.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Adolf"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Janusz"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
