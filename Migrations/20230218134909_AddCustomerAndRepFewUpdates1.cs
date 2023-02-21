using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularLogin.Migrations
{
    public partial class AddCustomerAndRepFewUpdates1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Reps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Reps");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Customers");
        }
    }
}
