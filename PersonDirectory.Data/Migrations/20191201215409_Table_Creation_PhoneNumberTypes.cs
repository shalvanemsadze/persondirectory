using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonDirectory.Data.Migrations
{
    public partial class Table_Creation_PhoneNumberTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneNumberTypes",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Male" },
                    { (byte)2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "PhoneNumberTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Mobile" },
                    { (byte)2, "Office" },
                    { (byte)3, "Home" }
                });

            migrationBuilder.InsertData(
                table: "RelationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Colleague" },
                    { (byte)2, "Acquaintance" },
                    { (byte)3, "Relative" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_Type",
                table: "PhoneNumbers",
                column: "Type");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_PhoneNumberTypes_Type",
                table: "PhoneNumbers",
                column: "Type",
                principalTable: "PhoneNumberTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_PhoneNumberTypes_Type",
                table: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "PhoneNumberTypes");

            migrationBuilder.DropIndex(
                name: "IX_PhoneNumbers_Type",
                table: "PhoneNumbers");

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "RelationTypes",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "RelationTypes",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "RelationTypes",
                keyColumn: "Id",
                keyValue: (byte)3);
        }
    }
}
