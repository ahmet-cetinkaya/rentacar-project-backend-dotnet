using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddAdditionalServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 16, 11, 37, 51, 868, DateTimeKind.Local).AddTicks(7818),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 12, 17, 39, 34, 100, DateTimeKind.Local).AddTicks(9555));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 12, 17, 39, 34, 100, DateTimeKind.Local).AddTicks(9555),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 16, 11, 37, 51, 868, DateTimeKind.Local).AddTicks(7818));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 14, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 14, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 2, 14, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 2, 14, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
