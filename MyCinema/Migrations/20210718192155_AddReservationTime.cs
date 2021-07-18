using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCinema.Migrations
{
    public partial class AddReservationTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservatioTime",
                table: "Reservations",
                newName: "ReservationTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservationTime",
                table: "Reservations",
                newName: "ReservatioTime");
        }
    }
}
