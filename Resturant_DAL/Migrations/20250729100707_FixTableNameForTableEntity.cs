using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resturant_DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixTableNameForTableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chief_Resturant_RestaurantID",
                table: "Chief");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Resturant_RestaurantID",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_EventType_EventTypeID",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Payment_PaymentID",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedTable_Reservation_ReservationID",
                table: "ReservedTable");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationID",
                table: "ReservedTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentID",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventTypeID",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantID",
                table: "Location",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantID",
                table: "Chief",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Chief_Resturant_RestaurantID",
                table: "Chief",
                column: "RestaurantID",
                principalTable: "Resturant",
                principalColumn: "RestaurantID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Resturant_RestaurantID",
                table: "Location",
                column: "RestaurantID",
                principalTable: "Resturant",
                principalColumn: "RestaurantID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_EventType_EventTypeID",
                table: "Reservation",
                column: "EventTypeID",
                principalTable: "EventType",
                principalColumn: "EventTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Payment_PaymentID",
                table: "Reservation",
                column: "PaymentID",
                principalTable: "Payment",
                principalColumn: "PaymentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedTable_Reservation_ReservationID",
                table: "ReservedTable",
                column: "ReservationID",
                principalTable: "Reservation",
                principalColumn: "ReservationID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chief_Resturant_RestaurantID",
                table: "Chief");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Resturant_RestaurantID",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_EventType_EventTypeID",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Payment_PaymentID",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservedTable_Reservation_ReservationID",
                table: "ReservedTable");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationID",
                table: "ReservedTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentID",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EventTypeID",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantID",
                table: "Location",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantID",
                table: "Chief",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Chief_Resturant_RestaurantID",
                table: "Chief",
                column: "RestaurantID",
                principalTable: "Resturant",
                principalColumn: "RestaurantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Resturant_RestaurantID",
                table: "Location",
                column: "RestaurantID",
                principalTable: "Resturant",
                principalColumn: "RestaurantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_EventType_EventTypeID",
                table: "Reservation",
                column: "EventTypeID",
                principalTable: "EventType",
                principalColumn: "EventTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Payment_PaymentID",
                table: "Reservation",
                column: "PaymentID",
                principalTable: "Payment",
                principalColumn: "PaymentID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservedTable_Reservation_ReservationID",
                table: "ReservedTable",
                column: "ReservationID",
                principalTable: "Reservation",
                principalColumn: "ReservationID");
        }
    }
}
