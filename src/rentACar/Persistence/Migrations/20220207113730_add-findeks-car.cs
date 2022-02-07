using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addfindekscar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "MinFindeksCreditRate",
                table: "Cars",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "MinFindeksCreditRate",
                value: (short)500);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "MinFindeksCreditRate",
                value: (short)1100);

            migrationBuilder.UpdateData(
                table: "FindeksCreditRates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Score",
                value: (short)1900);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinFindeksCreditRate",
                table: "Cars");

            migrationBuilder.UpdateData(
                table: "FindeksCreditRates",
                keyColumn: "Id",
                keyValue: 2,
                column: "Score",
                value: (short)5000);
        }
    }
}
