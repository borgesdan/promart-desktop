﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Promart.Database.Context;

#nullable disable

namespace Promart.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230126194432_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Promart.Database.Entities.FamilyRelationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Income")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Occupation")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("RegistryStatus")
                        .HasColumnType("int");

                    b.Property<byte>("Relationship")
                        .HasColumnType("tinyint");

                    b.Property<string>("Schooling")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("FamilyRelationships");
                });

            modelBuilder.Entity("Promart.Database.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressCEP")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("AddressCity")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AddressComplement")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AddressDistrict")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AddressNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("AddressReferencePoint")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("AddressState")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("AddressStreet")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CPF")
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Certidao")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte>("Dwelling")
                        .HasColumnType("tinyint");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<byte>("Gender")
                        .HasColumnType("tinyint");

                    b.Property<bool?>("IsGovernmentBeneficiary")
                        .HasColumnType("bit");

                    b.Property<byte>("MonthlyIncome")
                        .HasColumnType("tinyint");

                    b.Property<string>("Observations")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ProjectRegistry")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("ProjectRegistryDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("ProjectShift")
                        .HasColumnType("tinyint");

                    b.Property<byte>("ProjectStatus")
                        .HasColumnType("tinyint");

                    b.Property<string>("RG")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RegistryStatus")
                        .HasColumnType("int");

                    b.Property<byte>("Relationship")
                        .HasColumnType("tinyint");

                    b.Property<string>("ResponsibleName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ResponsiblePhone")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("SchoolName")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<byte>("SchoolShift")
                        .HasColumnType("tinyint");

                    b.Property<byte>("SchoolYear")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Promart.Database.Entities.Workshop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RegistryStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Workshops");
                });

            modelBuilder.Entity("StudentWorkshop", b =>
                {
                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.Property<int>("WorkshopsId")
                        .HasColumnType("int");

                    b.HasKey("StudentsId", "WorkshopsId");

                    b.HasIndex("WorkshopsId");

                    b.ToTable("StudentWorkshop");
                });

            modelBuilder.Entity("Promart.Database.Entities.FamilyRelationship", b =>
                {
                    b.HasOne("Promart.Database.Entities.Student", "Student")
                        .WithMany("FamilyRelationships")
                        .HasForeignKey("StudentId");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentWorkshop", b =>
                {
                    b.HasOne("Promart.Database.Entities.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Promart.Database.Entities.Workshop", null)
                        .WithMany()
                        .HasForeignKey("WorkshopsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Promart.Database.Entities.Student", b =>
                {
                    b.Navigation("FamilyRelationships");
                });
#pragma warning restore 612, 618
        }
    }
}
