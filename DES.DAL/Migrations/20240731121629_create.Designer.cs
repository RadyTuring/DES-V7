﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DES.DAL.Migrations
{
    [DbContext(typeof(DesAppDbContext))]
    [Migration("20240731121629_create")]
    partial class create
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.CaseStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CaseStatuss");
                });

            modelBuilder.Entity("Entities.Diagnosis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiagnosisName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiagnosisType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Diagnosiss");
                });

            modelBuilder.Entity("Entities.Dist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GovId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GovId");

                    b.ToTable("Dists");
                });

            modelBuilder.Entity("Entities.Gov", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GovName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Govs");
                });

            modelBuilder.Entity("Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("BirtDate")
                        .HasColumnType("date");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DistId")
                        .HasColumnType("int");

                    b.Property<string>("NID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DistId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Entities.PatientVisit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CaseStatusId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("FinalDiagnosisId")
                        .HasColumnType("int");

                    b.Property<int>("InitialDiagnosisId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("VisitDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CaseStatusId");

                    b.HasIndex("InitialDiagnosisId");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientVisits");
                });

            modelBuilder.Entity("Entities.ReportSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DistId")
                        .HasColumnType("int");

                    b.Property<string>("ReportSourceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DistId");

                    b.ToTable("ReportSources");
                });

            modelBuilder.Entity("Entities.ReportSourceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ReportSourceId")
                        .HasColumnType("int");

                    b.Property<string>("ReportSourceTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ReportSourceId");

                    b.ToTable("ReportSourceTypes");
                });

            modelBuilder.Entity("Entities.Dist", b =>
                {
                    b.HasOne("Entities.Gov", "Gov")
                        .WithMany()
                        .HasForeignKey("GovId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gov");
                });

            modelBuilder.Entity("Entities.Patient", b =>
                {
                    b.HasOne("Entities.Dist", "Dist")
                        .WithMany()
                        .HasForeignKey("DistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dist");
                });

            modelBuilder.Entity("Entities.PatientVisit", b =>
                {
                    b.HasOne("Entities.CaseStatus", "CaseStatus")
                        .WithMany()
                        .HasForeignKey("CaseStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Diagnosis", "InitialDiagnosis")
                        .WithMany()
                        .HasForeignKey("InitialDiagnosisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CaseStatus");

                    b.Navigation("InitialDiagnosis");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Entities.ReportSource", b =>
                {
                    b.HasOne("Entities.Dist", "Dist")
                        .WithMany()
                        .HasForeignKey("DistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dist");
                });

            modelBuilder.Entity("Entities.ReportSourceType", b =>
                {
                    b.HasOne("Entities.ReportSource", "ReportSource")
                        .WithMany()
                        .HasForeignKey("ReportSourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReportSource");
                });
#pragma warning restore 612, 618
        }
    }
}
