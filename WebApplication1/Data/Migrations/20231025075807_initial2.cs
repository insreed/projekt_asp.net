using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Session_SessionId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Exercise");

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "Exercise",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Session_SessionId",
                table: "Exercise",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Session_SessionId",
                table: "Exercise");

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "Exercise",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Exercise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Session_SessionId",
                table: "Exercise",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id");
        }
    }
}
