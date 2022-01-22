using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaccineRegistration.Migrations
{
    public partial class AddToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NIK = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaccineType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaccineDose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaccineDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "Questionaire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    isAllergies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isAutoimmune = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isMedication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isImmunosuppressant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isHeartdisease = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDiabetes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isHypertension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isCovid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionaire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionaire_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questionaire_PatientId",
                table: "Questionaire",
                column: "PatientId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questionaire");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
