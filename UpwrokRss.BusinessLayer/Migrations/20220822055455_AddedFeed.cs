using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpwrokRss.BusinessLayer.Migrations
{
    public partial class AddedFeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM RssItems");

            migrationBuilder.AddColumn<long>(
                name: "FeedId",
                table: "RssItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RssItems_FeedId",
                table: "RssItems",
                column: "FeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_RssItems_Feeds_FeedId",
                table: "RssItems",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RssItems_Feeds_FeedId",
                table: "RssItems");

            migrationBuilder.DropTable(
                name: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_RssItems_FeedId",
                table: "RssItems");

            migrationBuilder.DropColumn(
                name: "FeedId",
                table: "RssItems");
        }
    }
}
