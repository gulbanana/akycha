using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Akycha.Migrations
{
    /// <inheritdoc />
    public partial class ImproveFacilityEditing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Input_Facility_ToId",
                table: "Input");

            migrationBuilder.AddColumn<int>(
                name: "PowerShards",
                table: "Process",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ToId",
                table: "Input",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Input_Facility_ToId",
                table: "Input",
                column: "ToId",
                principalTable: "Facility",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Input_Facility_ToId",
                table: "Input");

            migrationBuilder.DropColumn(
                name: "PowerShards",
                table: "Process");

            migrationBuilder.AlterColumn<int>(
                name: "ToId",
                table: "Input",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Input_Facility_ToId",
                table: "Input",
                column: "ToId",
                principalTable: "Facility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
