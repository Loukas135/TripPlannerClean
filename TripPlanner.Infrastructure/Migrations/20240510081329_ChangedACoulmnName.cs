using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedACoulmnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Services",
                newName: "OverallRating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OverallRating",
                table: "Services",
                newName: "Rating");
        }
    }
}
