using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Akycha.Migrations
{
    /// <inheritdoc />
    public partial class SomersloopFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAmplified",
                table: "Process",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAmplified",
                table: "Process");
        }
    }
}
