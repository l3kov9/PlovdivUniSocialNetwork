using Microsoft.EntityFrameworkCore.Migrations;

namespace PuSocialNetwork.Data.Migrations
{
    public partial class PollTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalClicks",
                table: "PollAnswer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalClicks",
                table: "PollAnswer");
        }
    }
}
