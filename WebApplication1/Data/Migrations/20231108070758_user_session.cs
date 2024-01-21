using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    public partial class user_session : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Session",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Session_UserId",
                table: "Session",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_AspNetUsers_UserId",
                table: "Session",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_AspNetUsers_UserId",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_UserId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Session");
        }
    }
}
