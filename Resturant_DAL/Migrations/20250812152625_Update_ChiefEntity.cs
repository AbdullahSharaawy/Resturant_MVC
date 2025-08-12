using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resturant_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Update_ChiefEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Chief",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Chief");
        }
    }
}
