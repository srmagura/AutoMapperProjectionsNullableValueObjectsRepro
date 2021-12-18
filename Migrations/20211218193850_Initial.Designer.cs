﻿// <auto-generated />
using System;
using AutoMapperProjectionsNullableValueObjectsRepro.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutoMapperProjectionsNullableValueObjectsRepro.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20211218193850_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AutoMapperProjectionsNullableValueObjectsRepro.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("AutoMapperProjectionsNullableValueObjectsRepro.Message", b =>
                {
                    b.OwnsOne("AutoMapperProjectionsNullableValueObjectsRepro.MessageMetadata", "Metadata", b1 =>
                        {
                            b1.Property<int>("MessageId")
                                .HasColumnType("int");

                            b1.HasKey("MessageId");

                            b1.ToTable("Messages");

                            b1.WithOwner()
                                .HasForeignKey("MessageId");

                            b1.OwnsOne("AutoMapperProjectionsNullableValueObjectsRepro.FileRef", "Attachment", b2 =>
                                {
                                    b2.Property<int>("MessageMetadataMessageId")
                                        .HasColumnType("int");

                                    b2.Property<Guid>("FileId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("FileType")
                                        .IsRequired()
                                        .HasMaxLength(32)
                                        .HasColumnType("nvarchar(32)");

                                    b2.HasKey("MessageMetadataMessageId");

                                    b2.ToTable("Messages");

                                    b2.WithOwner()
                                        .HasForeignKey("MessageMetadataMessageId");
                                });

                            b1.Navigation("Attachment");
                        });

                    b.Navigation("Metadata")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}