using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddOtpAuthenticator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 3, 3, 15, 13, 28, 351, DateTimeKind.Local).AddTicks(8879),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 18, 14, 27, 44, 53, DateTimeKind.Local).AddTicks(4208));

            migrationBuilder.CreateTable(
                name: "OtpAuthenticators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SecretKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpAuthenticators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtpAuthenticators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 5, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 5, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "RentalBranches",
                keyColumn: "Id",
                keyValue: 1,
                column: "City",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 3, 5, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 3, 5, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_OtpAuthenticators_UserId",
                table: "OtpAuthenticators",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtpAuthenticators");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 18, 14, 27, 44, 53, DateTimeKind.Local).AddTicks(4208),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 3, 3, 15, 13, 28, 351, DateTimeKind.Local).AddTicks(8879));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "RentalBranches",
                keyColumn: "Id",
                keyValue: 1,
                column: "City",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
