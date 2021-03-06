﻿// <auto-generated />
using Hospital.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Hospital.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180311214532_initdatabase")]
    partial class initdatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hospital.API.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Birthdate");

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("FamilyName");

                    b.Property<string>("Login");

                    b.Property<string>("MiddleName");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<int?>("PositionId");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Hospital.API.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateTime");

                    b.Property<int>("Duration");

                    b.Property<string>("Note");

                    b.Property<int?>("PatientId");

                    b.Property<int?>("StaffId");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("StaffId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("Hospital.API.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Hospital.API.Models.Disease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("Hospital.API.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Birthdate");

                    b.Property<string>("FamilyName");

                    b.Property<string>("Login");

                    b.Property<string>("MiddleName");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Hospital.API.Models.PatientDisease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Cured");

                    b.Property<DateTimeOffset?>("Diagnosed");

                    b.Property<int?>("DiseaseId");

                    b.Property<string>("Note");

                    b.Property<int?>("PatientId");

                    b.HasKey("Id");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientDiseases");
                });

            modelBuilder.Entity("Hospital.API.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Responsibility");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Hospital.API.Models.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppointmentDuration");

                    b.Property<DateTimeOffset>("Birthdate");

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("FamilyName");

                    b.Property<string>("Login");

                    b.Property<string>("MiddleName");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<int?>("PositionId");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("Hospital.API.Models.Admin", b =>
                {
                    b.HasOne("Hospital.API.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Hospital.API.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("Hospital.API.Models.Appointment", b =>
                {
                    b.HasOne("Hospital.API.Models.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId");

                    b.HasOne("Hospital.API.Models.Staff", "Staff")
                        .WithMany("Appointments")
                        .HasForeignKey("StaffId");
                });

            modelBuilder.Entity("Hospital.API.Models.PatientDisease", b =>
                {
                    b.HasOne("Hospital.API.Models.Disease", "Disease")
                        .WithMany("PatientDiseases")
                        .HasForeignKey("DiseaseId");

                    b.HasOne("Hospital.API.Models.Patient", "Patient")
                        .WithMany("PatientDiseases")
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("Hospital.API.Models.Staff", b =>
                {
                    b.HasOne("Hospital.API.Models.Department", "Department")
                        .WithMany("Staffs")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Hospital.API.Models.Position", "Position")
                        .WithMany("Staffs")
                        .HasForeignKey("PositionId");
                });
#pragma warning restore 612, 618
        }
    }
}
