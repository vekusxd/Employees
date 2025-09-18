using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Api.Migrations
{
    /// <inheritdoc />
    public partial class SoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeFile_Employees_EmployeeId",
                table: "EmployeeFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeFile",
                table: "EmployeeFile");

            migrationBuilder.RenameTable(
                name: "EmployeeFile",
                newName: "EmployeeFiles");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeFile_EmployeeId",
                table: "EmployeeFiles",
                newName: "IX_EmployeeFiles_EmployeeId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeFiles",
                table: "EmployeeFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeFiles_Employees_EmployeeId",
                table: "EmployeeFiles",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeFiles_Employees_EmployeeId",
                table: "EmployeeFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeFiles",
                table: "EmployeeFiles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EmployeeFiles");

            migrationBuilder.RenameTable(
                name: "EmployeeFiles",
                newName: "EmployeeFile");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeFiles_EmployeeId",
                table: "EmployeeFile",
                newName: "IX_EmployeeFile_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeFile",
                table: "EmployeeFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeFile_Employees_EmployeeId",
                table: "EmployeeFile",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
