using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class branchandkilometer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentEndKilometer",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RentEndRentalBrandId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RentStartKilometer",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RentStartRentalBranchId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kilometer",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RentalBranchId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RentalBranches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalBranches", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RentalBranches",
                columns: new[] { "Id", "City" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 7 }
                });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RentEndKilometer", "RentEndRentalBrandId", "RentStartKilometer", "RentStartRentalBranchId" },
                values: new object[] { 1200, 2, 1000, 1 });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RentEndKilometer", "RentEndRentalBrandId", "RentStartKilometer", "RentStartRentalBranchId" },
                values: new object[] { 1200, 1, 1000, 2 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Kilometer", "RentalBranchId" },
                values: new object[] { 1000, 1 });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Kilometer", "RentalBranchId" },
                values: new object[] { 1000, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RentalBranchId",
                table: "Cars",
                column: "RentalBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_RentalBranches_RentalBranchId",
                table: "Cars",
                column: "RentalBranchId",
                principalTable: "RentalBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_RentalBranches_RentalBranchId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "RentalBranches");

            migrationBuilder.DropIndex(
                name: "IX_Cars_RentalBranchId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RentEndKilometer",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentEndRentalBrandId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentStartKilometer",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentStartRentalBranchId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Kilometer",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RentalBranchId",
                table: "Cars");
        }
    }
}
