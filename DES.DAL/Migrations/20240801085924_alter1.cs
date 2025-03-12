using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DES.DAL.Migrations
{
    /// <inheritdoc />
    public partial class alter1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Dists_DistId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientVisits_CaseStatuss_CaseStatusId",
                table: "PatientVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientVisits_Diagnosiss_InitialDiagnosisId",
                table: "PatientVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientVisits_Patients_PatientId",
                table: "PatientVisits");

            migrationBuilder.DropTable(
                name: "ReportSourceTypes");

            migrationBuilder.DropTable(
                name: "ReportSources");

            migrationBuilder.DropTable(
                name: "Dists");

            migrationBuilder.DropTable(
                name: "Govs");

            migrationBuilder.DropIndex(
                name: "IX_PatientVisits_CaseStatusId",
                table: "PatientVisits");

            migrationBuilder.DropIndex(
                name: "IX_PatientVisits_InitialDiagnosisId",
                table: "PatientVisits");

            migrationBuilder.DropIndex(
                name: "IX_PatientVisits_PatientId",
                table: "PatientVisits");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DistId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "DistId",
                table: "Patients",
                newName: "AreaId");

            migrationBuilder.AddColumn<string>(
                name: "ReportSource",
                table: "PatientVisits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ltype = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PgTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PgImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PgHref = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PgParentId = table.Column<int>(type: "int", nullable: true),
                    PgORder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiseaseIds = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    DesRoleId = table.Column<int>(type: "int", nullable: false),
                    DesPageId = table.Column<int>(type: "int", nullable: false),
                    IsAdd = table.Column<bool>(type: "bit", nullable: false),
                    IsUpdate = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsView = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePages_DesRoles_DesRoleId",
                        column: x => x.DesRoleId,
                        principalTable: "DesRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePages_DesRoleId",
                table: "RolePages",
                column: "DesRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "DesAccounts");

            migrationBuilder.DropTable(
                name: "DesPages");

            migrationBuilder.DropTable(
                name: "RolePages");

            migrationBuilder.DropTable(
                name: "DesRoles");

            migrationBuilder.DropColumn(
                name: "ReportSource",
                table: "PatientVisits");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Patients",
                newName: "DistId");

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
                    GovId = table.Column<int>(type: "int", nullable: false),
                    DistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
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
                name: "ReportSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistId = table.Column<int>(type: "int", nullable: false),
                    ReportSourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
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
                name: "ReportSourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportSourceId = table.Column<int>(type: "int", nullable: false),
                    ReportSourceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_Patients_DistId",
                table: "Patients",
                column: "DistId");

            migrationBuilder.CreateIndex(
                name: "IX_Dists_GovId",
                table: "Dists",
                column: "GovId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSources_DistId",
                table: "ReportSources",
                column: "DistId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportSourceTypes_ReportSourceId",
                table: "ReportSourceTypes",
                column: "ReportSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Dists_DistId",
                table: "Patients",
                column: "DistId",
                principalTable: "Dists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientVisits_CaseStatuss_CaseStatusId",
                table: "PatientVisits",
                column: "CaseStatusId",
                principalTable: "CaseStatuss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientVisits_Diagnosiss_InitialDiagnosisId",
                table: "PatientVisits",
                column: "InitialDiagnosisId",
                principalTable: "Diagnosiss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientVisits_Patients_PatientId",
                table: "PatientVisits",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
