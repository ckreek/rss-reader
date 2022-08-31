using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightRssReader.BusinessLayer.Migrations
{
    public partial class RenameItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RssPosts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    PublishDate = table.Column<string>(type: "TEXT", nullable: false),
                    Hidden = table.Column<bool>(type: "INTEGER", nullable: false),
                    Read = table.Column<bool>(type: "INTEGER", nullable: false),
                    FeedId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RssPosts_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RssPosts_FeedId",
                table: "RssPosts",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_RssPosts_Url",
                table: "RssPosts",
                column: "Url",
                unique: true);

            migrationBuilder.Sql(@"
INSERT INTO RssPosts (Id, Title, Summary, Url, PublishDate, Hidden, Read, FeedId, CreatedOn, UpdatedOn)
SELECT Id, Title, Summary, Url, PublishDate, Hidden, Read, FeedId, CreatedOn, UpdatedOn
FROM RssItems;
            ");

            migrationBuilder.DropTable(
                name: "RssItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RssItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FeedId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Hidden = table.Column<bool>(type: "INTEGER", nullable: false),
                    PublishDate = table.Column<string>(type: "TEXT", nullable: false),
                    Read = table.Column<bool>(type: "INTEGER", nullable: false),
                    Summary = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RssItems_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RssItems_FeedId",
                table: "RssItems",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_RssItems_Url",
                table: "RssItems",
                column: "Url",
                unique: true);

            migrationBuilder.Sql(@"
INSERT INTO RssItems (Id, Title, Summary, Url, PublishDate, Hidden, Read, FeedId, CreatedOn, UpdatedOn)
SELECT Id, Title, Summary, Url, PublishDate, Hidden, Read, FeedId, CreatedOn, UpdatedOn
FROM RssPosts;
            ");

            // migrationBuilder.DropTable(
            //     name: "RssPosts");
        }
    }
}
