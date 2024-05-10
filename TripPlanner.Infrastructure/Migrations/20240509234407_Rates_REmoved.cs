using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripPlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Rates_REmoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Ratings_RatingsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Cars_RoomId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Trips_RoomId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Ratings_RatingsId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_RatingsId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RatingsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RatingsId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RatingsId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "wallet",
                table: "AspNetUsers",
                newName: "Wallet");

            migrationBuilder.CreateTable(
                name: "RatingsService",
                columns: table => new
                {
                    RatingsId = table.Column<int>(type: "int", nullable: false),
                    ServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingsService", x => new { x.RatingsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_RatingsService_Ratings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingsService_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingsUser",
                columns: table => new
                {
                    RatingsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingsUser", x => new { x.RatingsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RatingsUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingsUser_Ratings_RatingsId",
                        column: x => x.RatingsId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CarId",
                table: "Reservation",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TripId",
                table: "Reservation",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingsService_ServicesId",
                table: "RatingsService",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingsUser_UsersId",
                table: "RatingsUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Cars_CarId",
                table: "Reservation",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Trips_TripId",
                table: "Reservation",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Cars_CarId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Trips_TripId",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "RatingsService");

            migrationBuilder.DropTable(
                name: "RatingsUser");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_CarId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_TripId",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "Wallet",
                table: "AspNetUsers",
                newName: "wallet");

            migrationBuilder.AddColumn<int>(
                name: "RatingsId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingsId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_RatingsId",
                table: "Services",
                column: "RatingsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RatingsId",
                table: "AspNetUsers",
                column: "RatingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Ratings_RatingsId",
                table: "AspNetUsers",
                column: "RatingsId",
                principalTable: "Ratings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Cars_RoomId",
                table: "Reservation",
                column: "RoomId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Trips_RoomId",
                table: "Reservation",
                column: "RoomId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Ratings_RatingsId",
                table: "Services",
                column: "RatingsId",
                principalTable: "Ratings",
                principalColumn: "Id");
        }
    }
}
