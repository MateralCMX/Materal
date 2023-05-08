﻿// <auto-generated />
using System;
using Materal.Oscillator.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Materal.Oscillator.SqliteEFRepository.Migrations
{
    [DbContext(typeof(OscillatorDBContext))]
    [Migration("20230505090235_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Materal.Oscillator.Abstractions.Domain.Answer", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AnswerData")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<string>("AnswerType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(400)
                        .HasColumnType("TEXT");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ScheduleID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkEvent")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("Materal.Oscillator.Abstractions.Domain.Plan", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(400)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PlanTriggerData")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<string>("PlanTriggerType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ScheduleID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Plan");
                });

            modelBuilder.Entity("Materal.Oscillator.Abstractions.Domain.Schedule", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(400)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Territory")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Schedule");
                });

            modelBuilder.Entity("Materal.Oscillator.Abstractions.Domain.ScheduleWork", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("FailEvent")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("Index")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ScheduleID")
                        .HasColumnType("TEXT");

                    b.Property<string>("SuccessEvent")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("WorkID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("ScheduleWork");
                });

            modelBuilder.Entity("Materal.Oscillator.Abstractions.Domain.Work", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(400)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkData")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Work");
                });
#pragma warning restore 612, 618
        }
    }
}