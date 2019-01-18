﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaxCalculatorApp.API.Data;

namespace TaxCalculatorApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaxCalculatorApp.API.models.PostalCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<int?>("TaxCalculationTypeId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TaxCalculationTypeId");

                    b.ToTable("PostalCodes");

                    b.HasData(
                        new { Id = 1, Code = "7441", TaxCalculationTypeId = 1 },
                        new { Id = 2, Code = "A100", TaxCalculationTypeId = 2 },
                        new { Id = 3, Code = "7000", TaxCalculationTypeId = 3 },
                        new { Id = 4, Code = "1000", TaxCalculationTypeId = 1 }
                    );
                });

            modelBuilder.Entity("TaxCalculatorApp.API.models.TaxCalculatedValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AnnualIncome");

                    b.Property<DateTime>("DateCalculated");

                    b.Property<string>("PostalCode");

                    b.Property<decimal>("ValueCalculated");

                    b.HasKey("Id");

                    b.ToTable("TaxCalculatedValues");
                });

            modelBuilder.Entity("TaxCalculatorApp.API.models.TaxCalculationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("TaxCalculationTypes");

                    b.HasData(
                        new { Id = 1, Type = "Progressive" },
                        new { Id = 2, Type = "Flat Value" },
                        new { Id = 3, Type = "Flat Rate" }
                    );
                });

            modelBuilder.Entity("TaxCalculatorApp.API.models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaxCalculatorApp.API.models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("TaxCalculatorApp.API.models.PostalCode", b =>
                {
                    b.HasOne("TaxCalculatorApp.API.models.TaxCalculationType", "TaxCalculationType")
                        .WithMany("PostalCodes")
                        .HasForeignKey("TaxCalculationTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
