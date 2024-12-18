﻿// <auto-generated />
using System;
using Materal.TTA.Demo.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Materal.TTA.Demo.SqliteEFRepository.Migrations
{
    [DbContext(typeof(TTADemoDBContext))]
    partial class TTADemoDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Materal.TTA.Demo.Domain.TestDomain", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte>("ByteType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTimeType")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("DecimalType")
                        .HasColumnType("TEXT");

                    b.Property<byte>("EnumType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IntType")
                        .HasMaxLength(100)
                        .HasColumnType("INTEGER");

                    b.Property<string>("StringType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("TestDomain", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
