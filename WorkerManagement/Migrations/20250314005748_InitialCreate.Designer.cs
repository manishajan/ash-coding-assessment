﻿// <auto-generated />
using WorkerManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WorkerManagement.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250314005748_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WorkerManagement.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"));

                    b.Property<decimal>("PayPerHour")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("WorkerID")
                        .HasColumnType("int");

                    b.HasKey("EmployeeID");

                    b.HasIndex("WorkerID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WorkerManagement.Models.Manager", b =>
                {
                    b.Property<int>("ManagerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManagerID"));

                    b.Property<decimal>("AnnualSalary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MaxExpenseAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("WorkerID")
                        .HasColumnType("int");

                    b.HasKey("ManagerID");

                    b.HasIndex("WorkerID");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("WorkerManagement.Models.Supervisor", b =>
                {
                    b.Property<int>("SupervisorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupervisorID"));

                    b.Property<decimal>("AnnualSalary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("WorkerID")
                        .HasColumnType("int");

                    b.HasKey("SupervisorID");

                    b.HasIndex("WorkerID");

                    b.ToTable("Supervisors");
                });

            modelBuilder.Entity("WorkerManagement.Models.Worker", b =>
                {
                    b.Property<int>("WorkerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkerID"));

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkerID");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("WorkerManagement.Models.Employee", b =>
                {
                    b.HasOne("WorkerManagement.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("WorkerManagement.Models.Manager", b =>
                {
                    b.HasOne("WorkerManagement.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("WorkerManagement.Models.Supervisor", b =>
                {
                    b.HasOne("WorkerManagement.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Worker");
                });
#pragma warning restore 612, 618
        }
    }
}
