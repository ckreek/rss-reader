using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightRssReader.BusinessLayer.Migrations
{
    public partial class UrlUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RssItems_Url",
                table: "RssItems");

            migrationBuilder.CreateIndex(
                name: "IX_RssItems_Url",
                table: "RssItems",
                column: "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RssItems_Url",
                table: "RssItems");

            migrationBuilder.CreateIndex(
                name: "IX_RssItems_Url",
                table: "RssItems",
                column: "Url");
        }
    }
}
