﻿// <auto-generated />
using System;
using Materal.TTA.Demo.MySqlEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Materal.TTA.Demo.MySqlEFRepository.Migrations
{
    [DbContext(typeof(TTADemoDBContext))]
    [Migration("20231128070239_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Materal.TTA.Demo.Domain.TestDomain", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte>("ByteType")
                        .HasColumnType("tinyint unsigned");

                    b.Property<DateTime>("DateTimeType")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("DecimalType")
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte>("EnumType")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("IntType")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<string>("StringType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("ID");

                    b.ToTable("TestDomain");
                });
#pragma warning restore 612, 618
        }
    }
}