﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBoards.entities;

#nullable disable

namespace MyBoards.Migrations
{
    [DbContext(typeof(MyBoardContext))]
    [Migration("20220804132829_UserFullName")]
    partial class UserFullName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MyBoards.entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("MyBoards.entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkItemId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MyBoards.entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("MyBoards.entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyBoards.entities.WorkItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IterationPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("StateId");

                    b.ToTable("WorkItems");

                    b.HasDiscriminator<string>("Discriminator").HasValue("WorkItem");
                });

            modelBuilder.Entity("MyBoards.entities.WorkItemState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("WorkItemStates");
                });

            modelBuilder.Entity("MyBoards.entities.WorkItemTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("WorkItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublicationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("TagId", "WorkItemId");

                    b.HasIndex("WorkItemId");

                    b.ToTable("WorkItemTag");
                });

            modelBuilder.Entity("MyBoards.entities.Epic", b =>
                {
                    b.HasBaseType("MyBoards.entities.WorkItem");

                    b.Property<DateTime?>("EndDate")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Epic");
                });

            modelBuilder.Entity("MyBoards.entities.Issue", b =>
                {
                    b.HasBaseType("MyBoards.entities.WorkItem");

                    b.Property<decimal>("Effort")
                        .HasColumnType("decimal(5,2)");

                    b.HasDiscriminator().HasValue("Issue");
                });

            modelBuilder.Entity("MyBoards.entities.Task", b =>
                {
                    b.HasBaseType("MyBoards.entities.WorkItem");

                    b.Property<string>("Activity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RemainingWork")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.HasDiscriminator().HasValue("Task");
                });

            modelBuilder.Entity("MyBoards.entities.Address", b =>
                {
                    b.HasOne("MyBoards.entities.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("MyBoards.entities.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyBoards.entities.Comment", b =>
                {
                    b.HasOne("MyBoards.entities.WorkItem", "WorkItem")
                        .WithMany("Comments")
                        .HasForeignKey("WorkItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("MyBoards.entities.WorkItem", b =>
                {
                    b.HasOne("MyBoards.entities.User", "Author")
                        .WithMany("WorkItems")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyBoards.entities.WorkItemState", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("State");
                });

            modelBuilder.Entity("MyBoards.entities.WorkItemTag", b =>
                {
                    b.HasOne("MyBoards.entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyBoards.entities.WorkItem", "WorkItem")
                        .WithMany()
                        .HasForeignKey("WorkItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("MyBoards.entities.User", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("WorkItems");
                });

            modelBuilder.Entity("MyBoards.entities.WorkItem", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
