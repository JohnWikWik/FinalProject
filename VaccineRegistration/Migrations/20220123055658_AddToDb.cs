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
                    isAllergies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isAutoimmune = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isMedication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isImmunosuppressant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isHeartdisease = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDiabetes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isHypertension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isCovid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    VaccineRegistreeModelPatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionaire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionaire_Patient_VaccineRegistreeModelPatientId",
                        column: x => x.VaccineRegistreeModelPatientId,
                        principalTable: "Patient",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questionaire_VaccineRegistreeModelPatientId",
                table: "Questionaire",
                column: "VaccineRegistreeModelPatientId");
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
