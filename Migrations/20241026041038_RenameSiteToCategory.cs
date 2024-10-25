using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Akycha.Migrations
{
    /// <inheritdoc />
    public partial class RenameSiteToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Facility",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("update Facility set Category=coalesce(Site, 'Facilities');");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "Facility");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "Facility",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql("update Facility set Site=Category;");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Facility");
        }
    }
}
