using Microsoft.EntityFrameworkCore.Migrations;

namespace TaxFormGeneratorApi.Migrations
{
    public partial class OibsToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PersonalOib",
                table: "UserSettings",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CompanyOib",
                table: "UserSettings",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PersonalOib",
                table: "UserSettings",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "CompanyOib",
                table: "UserSettings",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
