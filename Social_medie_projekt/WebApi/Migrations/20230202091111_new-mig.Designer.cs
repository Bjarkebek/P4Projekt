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
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230202091111_new-mig.Designer.cs
    [Migration("20230202091111_new-mig")]
    partial class newmig
========
    [Migration("20230206092013_initial")]
    partial class initial
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230206092013_initial.Designer.cs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi.Database.Entities.Liked", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LikedTime")
                        .HasColumnType("datetime");

                    b.HasKey("UserId", "PostId");

                    b.ToTable("Liked");

                    b.HasData(
                        new
                        {
                            UserId = 2,
                            PostId = 1,
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230202091111_new-mig.Designer.cs
                            LikedTime = new DateTime(2023, 2, 2, 10, 11, 11, 388, DateTimeKind.Local).AddTicks(4536)
========
                            LikedTime = new DateTime(2023, 2, 6, 10, 20, 13, 660, DateTimeKind.Local).AddTicks(4129)
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230206092013_initial.Designer.cs
                        });
                });

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

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("LoginId");

                    b.ToTable("Login");

                    b.HasData(
                        new
                        {
                            LoginId = 1,
                            Email = "Test1@mail.dk",
                            Password = "password",
                            Type = 0
                        },
                        new
                        {
                            LoginId = 2,
                            Email = "Test2@mail.dk",
                            Password = "password",
                            Type = 1
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Posts", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            PostId = 1,
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230202091111_new-mig.Designer.cs
                            Date = new DateTime(2023, 2, 2, 10, 11, 11, 388, DateTimeKind.Local).AddTicks(4522),
========
                            Date = new DateTime(2023, 2, 6, 10, 20, 13, 660, DateTimeKind.Local).AddTicks(4117),
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230206092013_initial.Designer.cs
                            Desc = "tadnawdnada",
                            Likes = 1,
                            Title = "testestestest",
                            UserId = 1
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
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230202091111_new-mig.Designer.cs
                            Created = new DateTime(2023, 2, 2, 10, 11, 11, 388, DateTimeKind.Local).AddTicks(4502),
========
                            Created = new DateTime(2023, 2, 6, 10, 20, 13, 660, DateTimeKind.Local).AddTicks(4098),
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230206092013_initial.Designer.cs
                            FirstName = "test",
                            LastName = "1",
                            LoginId = 1
                        },
                        new
                        {
                            UserId = 2,
                            Address = "testvej 2",
<<<<<<<< HEAD:Social_medie_projekt/WebApi/Migrations/20230202091111_new-mig.Designer.cs
                            Created = new DateTime(2023, 2, 2, 10, 11, 11, 388, DateTimeKind.Local).AddTicks(4507),
========
                            Created = new DateTime(2023, 2, 6, 10, 20, 13, 660, DateTimeKind.Local).AddTicks(4102),
>>>>>>>> origin/dev:Social_medie_projekt/WebApi/Migrations/20230206092013_initial.Designer.cs
                            FirstName = "test",
                            LastName = "2",
                            LoginId = 2
                        });
                });

            modelBuilder.Entity("WebApi.Database.Entities.Posts", b =>
                {
                    b.HasOne("WebApi.Database.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("WebApi.Database.Entities.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
