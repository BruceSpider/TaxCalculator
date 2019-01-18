using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxCalculatorApp.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxCalculatedValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ValueCalculated = table.Column<decimal>(nullable: false),
                    DateCalculated = table.Column<DateTime>(nullable: false),
                    AnnualIncome = table.Column<decimal>(nullable: false),
                    PostalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculatedValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostalCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    TaxCalculationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostalCodes_TaxCalculationTypes_TaxCalculationTypeId",
                        column: x => x.TaxCalculationTypeId,
                        principalTable: "TaxCalculationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TaxCalculationTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "Progressive" });

            migrationBuilder.InsertData(
                table: "TaxCalculationTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "Flat Value" });

            migrationBuilder.InsertData(
                table: "TaxCalculationTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 3, "Flat Rate" });

            migrationBuilder.InsertData(
                table: "PostalCodes",
                columns: new[] { "Id", "Code", "TaxCalculationTypeId" },
                values: new object[,]
                {
                    { 1, "7441", 1 },
                    { 4, "1000", 1 },
                    { 2, "A100", 2 },
                    { 3, "7000", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodes_TaxCalculationTypeId",
                table: "PostalCodes",
                column: "TaxCalculationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostalCodes");

            migrationBuilder.DropTable(
                name: "TaxCalculatedValues");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "TaxCalculationTypes");
        }
    }
}
