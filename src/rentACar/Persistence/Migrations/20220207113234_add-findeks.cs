using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addfindeks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FindeksCreditRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindeksCreditRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FindeksCreditRates_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FindeksCreditRates",
                columns: new[] { "Id", "CustomerId", "Score" },
                values: new object[] { 1, 1, (short)1000 });

            migrationBuilder.InsertData(
                table: "FindeksCreditRates",
                columns: new[] { "Id", "CustomerId", "Score" },
                values: new object[] { 2, 2, (short)5000 });

            migrationBuilder.CreateIndex(
                name: "IX_FindeksCreditRates_CustomerId",
                table: "FindeksCreditRates",
                column: "CustomerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FindeksCreditRates");
        }
    }
}
