﻿// <auto-generated />
using System;
using FileDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FileDemo.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20210318012208_attachment_table")]
    partial class attachment_table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FileDemo.Models.Attachment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("attachment")
                        .HasColumnType("varbinary(MAX)");

                    b.HasKey("id");

                    b.ToTable("attachments");
                });
#pragma warning restore 612, 618
        }
    }
}
