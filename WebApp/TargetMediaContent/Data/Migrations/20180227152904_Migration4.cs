using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MediaContentHSE.Data.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaContents",
                columns: table => new
                {
                    MediaContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaContents", x => x.MediaContentId);
                });

            migrationBuilder.CreateTable(
                name: "TargetGroups",
                columns: table => new
                {
                    TargetGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndAge = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartAge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetGroups", x => x.TargetGroupId);
                });

            migrationBuilder.CreateTable(
                name: "TargetMediaContentInterfaces",
                columns: table => new
                {
                    TargetMediaContentInterfaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Button = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetMediaContentInterfaces", x => x.TargetMediaContentInterfaceId);
                });

            migrationBuilder.CreateTable(
                name: "TargetMediaContents",
                columns: table => new
                {
                    TargetMediaContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MediaContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TargetGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetMediaContentInterfaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetMediaContents", x => x.TargetMediaContentId);
                    table.ForeignKey(
                        name: "FK_TargetMediaContents_MediaContents_MediaContentId",
                        column: x => x.MediaContentId,
                        principalTable: "MediaContents",
                        principalColumn: "MediaContentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetMediaContents_TargetGroups_TargetGroupId",
                        column: x => x.TargetGroupId,
                        principalTable: "TargetGroups",
                        principalColumn: "TargetGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetMediaContents_TargetMediaContentInterfaces_TargetMediaContentInterfaceId",
                        column: x => x.TargetMediaContentInterfaceId,
                        principalTable: "TargetMediaContentInterfaces",
                        principalColumn: "TargetMediaContentInterfaceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TargetMediaContents_MediaContentId",
                table: "TargetMediaContents",
                column: "MediaContentId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetMediaContents_TargetGroupId",
                table: "TargetMediaContents",
                column: "TargetGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetMediaContents_TargetMediaContentInterfaceId",
                table: "TargetMediaContents",
                column: "TargetMediaContentInterfaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TargetMediaContents");

            migrationBuilder.DropTable(
                name: "MediaContents");

            migrationBuilder.DropTable(
                name: "TargetGroups");

            migrationBuilder.DropTable(
                name: "TargetMediaContentInterfaces");
        }
    }
}
