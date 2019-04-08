using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TaxFormGeneratorApi.Migrations
{
    public partial class AddUserSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PersonalOib = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: false),
                    StreetNumber = table.Column<string>(nullable: false),
                    Postcode = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    CityIban = table.Column<string>(nullable: false),
                    CityCode = table.Column<string>(nullable: false),
                    CitySurtax = table.Column<double>(nullable: false),
                    CompanyOib = table.Column<int>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    CompanyEmail = table.Column<string>(nullable: false),
                    CompanyStreet = table.Column<string>(nullable: false),
                    CompanyCity = table.Column<string>(nullable: false),
                    SalaryAmount = table.Column<decimal>(nullable: false),
                    SalaryCurrency = table.Column<string>(nullable: false),
                    SalaryNonTaxableAmount = table.Column<decimal>(nullable: false),
                    Salary_SalaryTax = table.Column<double>(nullable: false),
                    SalaryHealthInsuranceContribution = table.Column<double>(nullable: false),
                    SalaryWorkSafetyContribution = table.Column<double>(nullable: false),
                    SalaryEmploymentContribution = table.Column<double>(nullable: false),
                    SalaryPensionPillar1Contribution = table.Column<double>(nullable: false),
                    SalaryPensionPillar2Contribution = table.Column<double>(nullable: false),
                    DividendTax = table.Column<double>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_CompanyOib",
                table: "UserSettings",
                column: "CompanyOib",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_PersonalOib",
                table: "UserSettings",
                column: "PersonalOib",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
