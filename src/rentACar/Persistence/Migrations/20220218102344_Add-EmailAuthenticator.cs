using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddEmailAuthenticator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthenticatorType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 18, 13, 23, 42, 625, DateTimeKind.Local).AddTicks(5408),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 16, 22, 46, 47, 552, DateTimeKind.Local).AddTicks(3327));

            migrationBuilder.CreateTable(
                name: "EmailAuthenticators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ActivationKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAuthenticators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAuthenticators_Users_UserId",
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
                values: new object[] { new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "RentalEndDate", "RentalStartDate" },
                values: new object[] { new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Local) });

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

            migrationBuilder.CreateIndex(
                name: "IX_EmailAuthenticators_UserId",
                table: "EmailAuthenticators",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailAuthenticators");

            migrationBuilder.DropColumn(
                name: "AuthenticatorType",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 16, 22, 46, 47, 552, DateTimeKind.Local).AddTicks(3327),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 18, 13, 23, 42, 625, DateTimeKind.Local).AddTicks(5408));

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
    }
}
