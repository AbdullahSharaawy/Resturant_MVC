using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resturant_DAL.Migrations
{
    /// <inheritdoc />
    public partial class remove_resturantID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantID",
                table: "Table");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantID",
                table: "Table",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
