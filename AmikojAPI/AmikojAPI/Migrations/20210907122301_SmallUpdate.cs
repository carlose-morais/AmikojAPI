using Microsoft.EntityFrameworkCore.Migrations;

namespace AmikojApi.Migrations
{
    public partial class SmallUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastCompleteClassId",
                table: "UsersProgress",
                newName: "LastCompleteClassNumber");

            migrationBuilder.RenameColumn(
                name: "ChapterId",
                table: "UsersProgress",
                newName: "ChapterNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastCompleteClassNumber",
                table: "UsersProgress",
                newName: "LastCompleteClassId");

            migrationBuilder.RenameColumn(
                name: "ChapterNumber",
                table: "UsersProgress",
                newName: "ChapterId");
        }
    }
}
