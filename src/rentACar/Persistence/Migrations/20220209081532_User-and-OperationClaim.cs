using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class UserandOperationClaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 9, 11, 15, 31, 584, DateTimeKind.Local).AddTicks(9712),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 8, 13, 15, 56, 33, DateTimeKind.Local).AddTicks(4855));

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 2, 9, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 9, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 2, 9, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 9, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 2, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 9, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 2, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 9, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 8, 13, 15, 56, 33, DateTimeKind.Local).AddTicks(4855),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 9, 11, 15, 31, 584, DateTimeKind.Local).AddTicks(9712));

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 2, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 2, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Rentals",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "RentEndDate", "RentStartDate" },
                values: new object[] { new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 8, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
