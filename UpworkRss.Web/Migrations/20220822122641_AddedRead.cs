using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace upwork_rss.Migrations
{
    public partial class AddedRead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "RssItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Read",
                table: "RssItems");
        }
    }
}
