using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DES.DAL.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaseStatuss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStatuss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosiss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagnosisName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosisType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosiss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Govs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GovName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Govs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    GovId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dists_Govs_GovId",
                        column: x => x.GovId,
                        principalTable: "Govs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirtDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DistId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Dists_DistId",
                        column: x => x.DistId,
                        principalTable: "Dists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportSourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportSources_Dists_DistId",
                        column: x => x.DistId,
                        principalTable: "Dists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientVisits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitDate = table.Column<DateOnly>(type: "date", nullable: false),
                    InitialDiagnosisId = table.Column<int>(type: "int", nullable: false),
                    FinalDiagnosisId = table.Column<int>(type: "int", nullable: false),
                    CaseStatusId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientVisits_CaseStatuss_CaseStatusId",
                        column: x => x.CaseStatusId,
                        principalTable: "CaseStatuss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientVisits_Diagnosiss_InitialDiagnosisId",
                        column: x => x.InitialDiagnosisId,
                        principalTable: "Diagnosiss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientVisits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportSourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportSourceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ReportSourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSourceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportSourceTypes_ReportSources_ReportSourceId",
                        column: x => x.ReportSourceId,
                        principalTable: "ReportSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dists_GovId",
                table: "Dists",
                column: "GovId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DistId",
                table: "Patients",
                column: "DistId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVisits_CaseStatusId",
                table: "PatientVisits",
                column: "CaseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVisits_InitialDiagnosisId",
                table: "PatientVisits",
                column: "InitialDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVisits_PatientId",
                table: "PatientVisits",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSources_DistId",
                table: "ReportSources",
                column: "DistId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSourceTypes_ReportSourceId",
                table: "ReportSourceTypes",
                column: "ReportSourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientVisits");

            migrationBuilder.DropTable(
                name: "ReportSourceTypes");

            migrationBuilder.DropTable(
                name: "CaseStatuss");

            migrationBuilder.DropTable(
                name: "Diagnosiss");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "ReportSources");

            migrationBuilder.DropTable(
                name: "Dists");

            migrationBuilder.DropTable(
                name: "Govs");
        }
    }
}
