using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resturant_DAL.Migrations
{
    /// <inheritdoc />
    public partial class udbdate_reviewModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Review",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserID",
                table: "Review",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_UserID",
                table: "Review",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_UserID",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserID",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Review");
        }
    }
}
