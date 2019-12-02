using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonDirectory.Data.Migrations
{
    public partial class Related_People_Recreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelatedPeople",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationType = table.Column<byte>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    RelativePersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedPeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedPeople_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatedPeople_People_RelativePersonId",
                        column: x => x.RelativePersonId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPeople_PersonId",
                table: "RelatedPeople",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPeople_RelativePersonId",
                table: "RelatedPeople",
                column: "RelativePersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedPeople");
        }
    }
}
