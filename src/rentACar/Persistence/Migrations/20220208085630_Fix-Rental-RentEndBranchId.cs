using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class FixRentalRentEndBranchId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RentEndRentalBrandId",
                table: "Rentals",
                newName: "RentEndRentalBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentEndRentalBranchId",
                table: "Rentals",
                column: "RentEndRentalBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentStartRentalBranchId",
                table: "Rentals",
                column: "RentStartRentalBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_RentalBranches_RentEndRentalBranchId",
                table: "Rentals",
                column: "RentEndRentalBranchId",
                principalTable: "RentalBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_RentalBranches_RentStartRentalBranchId",
                table: "Rentals",
                column: "RentStartRentalBranchId",
                principalTable: "RentalBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_RentalBranches_RentEndRentalBranchId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_RentalBranches_RentStartRentalBranchId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_RentEndRentalBranchId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_RentStartRentalBranchId",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "RentEndRentalBranchId",
                table: "Rentals",
                newName: "RentEndRentalBrandId");
        }
    }
}
