using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularLogin.Migrations
{
    public partial class AddCustomerAndRep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Lots",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedDate",
                table: "Lots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RepId1",
                table: "Lots",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoldDate",
                table: "Lots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Reps",
                columns: table => new
                {
                    RepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepPhone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reps", x => x.RepId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lots_CustomerId",
                table: "Lots",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_RepId1",
                table: "Lots",
                column: "RepId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Customers_CustomerId",
                table: "Lots",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Customers_LotId",
                table: "Lots",
                column: "LotId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Reps_LotId",
                table: "Lots",
                column: "LotId",
                principalTable: "Reps",
                principalColumn: "RepId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_Reps_RepId1",
                table: "Lots",
                column: "RepId1",
                principalTable: "Reps",
                principalColumn: "RepId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Customers_CustomerId",
                table: "Lots");

            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Customers_LotId",
                table: "Lots");

            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Reps_LotId",
                table: "Lots");

            migrationBuilder.DropForeignKey(
                name: "FK_Lots_Reps_RepId1",
                table: "Lots");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Reps");

            migrationBuilder.DropIndex(
                name: "IX_Lots_CustomerId",
                table: "Lots");

            migrationBuilder.DropIndex(
                name: "IX_Lots_RepId1",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "ReceivedDate",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "RepId1",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "SoldDate",
                table: "Lots");
        }
    }
}
