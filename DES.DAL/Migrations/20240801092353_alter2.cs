using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DES.DAL.Migrations
{
    /// <inheritdoc />
    public partial class alter2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePages_DesRoles_DesRoleId",
                table: "RolePages");

            migrationBuilder.DropIndex(
                name: "IX_RolePages_DesRoleId",
                table: "RolePages");

            migrationBuilder.DropColumn(
                name: "DesRoleId",
                table: "RolePages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DesRoleId",
                table: "RolePages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RolePages_DesRoleId",
                table: "RolePages",
                column: "DesRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePages_DesRoles_DesRoleId",
                table: "RolePages",
                column: "DesRoleId",
                principalTable: "DesRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
