using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class EndPropCanNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_RentalBranches_RentEndRentalBranchId",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "RentEndRentalBranchId",
                table: "Rentals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RentEndKilometer",
                table: "Rentals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_RentalBranches_RentEndRentalBranchId",
                table: "Rentals",
                column: "RentEndRentalBranchId",
                principalTable: "RentalBranches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_RentalBranches_RentEndRentalBranchId",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "RentEndRentalBranchId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RentEndKilometer",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_RentalBranches_RentEndRentalBranchId",
                table: "Rentals",
                column: "RentEndRentalBranchId",
                principalTable: "RentalBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
