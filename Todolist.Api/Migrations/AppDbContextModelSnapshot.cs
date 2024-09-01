﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Todolist.Api.Models.Data;

#nullable disable

namespace Todolist.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Todolist.Api.Models.Domain.Todos", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("is_active")
                        .HasColumnType("boolean");

                    b.Property<string>("todo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.ToTable("t_todos");
                });

            modelBuilder.Entity("Todolist.Api.Models.Domain.Users", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("is_active")
                        .HasColumnType("boolean");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("t_users");
                });

            modelBuilder.Entity("Todolist.Api.Models.Domain.Todos", b =>
                {
                    b.HasOne("Todolist.Api.Models.Domain.Users", "user")
                        .WithMany("todos")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Todolist.Api.Models.Domain.Users", b =>
                {
                    b.Navigation("todos");
                });
#pragma warning restore 612, 618
        }
    }
}
