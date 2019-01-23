using Microsoft.EntityFrameworkCore.Migrations;

namespace PuSocialNetwork.Data.Migrations
{
    public partial class Play2048ScoresAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Play2048Score_Users_UserId",
                table: "Play2048Score");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Play2048Score",
                table: "Play2048Score");

            migrationBuilder.RenameTable(
                name: "Play2048Score",
                newName: "Play2048Scores");

            migrationBuilder.RenameIndex(
                name: "IX_Play2048Score_UserId",
                table: "Play2048Scores",
                newName: "IX_Play2048Scores_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Play2048Scores",
                table: "Play2048Scores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Play2048Scores_Users_UserId",
                table: "Play2048Scores",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Play2048Scores_Users_UserId",
                table: "Play2048Scores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Play2048Scores",
                table: "Play2048Scores");

            migrationBuilder.RenameTable(
                name: "Play2048Scores",
                newName: "Play2048Score");

            migrationBuilder.RenameIndex(
                name: "IX_Play2048Scores_UserId",
                table: "Play2048Score",
                newName: "IX_Play2048Score_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Play2048Score",
                table: "Play2048Score",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Play2048Score_Users_UserId",
                table: "Play2048Score",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
