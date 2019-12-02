using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonDirectory.Data.Migrations
{
    public partial class Cascade_Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_People_PersonId",
                table: "PhoneNumbers");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_People_PersonId",
                table: "PhoneNumbers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_People_PersonId",
                table: "PhoneNumbers");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_People_PersonId",
                table: "PhoneNumbers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
