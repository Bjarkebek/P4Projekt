﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Database;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230116090025_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi.Database.Entities.Login", b =>
                {
                    b.Property<int>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("LoginId");

                    b.ToTable("Login");

                    b.HasData(
                        new
                        {
                            LoginId = 1,
                            Email = "Test1@mail.dk",
                            Password = "password"
                        },
                        new
                        {
                            LoginId = 2,
                            Email = "Test2@mail.dk",
                            Password = "password"
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("LoginId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("LoginId")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Address = "testvej 1",
                            Created = new DateTime(2023, 1, 16, 10, 0, 24, 982, DateTimeKind.Local).AddTicks(182),
                            FirstName = "test",
                            LastName = "1",
                            LoginId = 1
                        },
                        new
                        {
                            UserId = 2,
                            Address = "testvej 2",
                            Created = new DateTime(2023, 1, 16, 10, 0, 24, 982, DateTimeKind.Local).AddTicks(186),
                            FirstName = "test",
                            LastName = "2",
                            LoginId = 2
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.User", b =>
                {
                    b.HasOne("WebApi.Database.Entities.Login", "Login")
                        .WithOne("User")
                        .HasForeignKey("WebApi.Database.Entities.User", "LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("WebApi.Database.Entities.Login", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}