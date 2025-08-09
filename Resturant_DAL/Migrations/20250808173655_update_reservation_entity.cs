using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resturant_DAL.Migrations
{
    /// <inheritdoc />
    public partial class update_reservation_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reservation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
