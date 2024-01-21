using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    public partial class updatebazy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ExerciseType",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseType_UserId",
                table: "ExerciseType",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseType_AspNetUsers_UserId",
                table: "ExerciseType",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseType_AspNetUsers_UserId",
                table: "ExerciseType");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseType_UserId",
                table: "ExerciseType");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExerciseType");
        }
    }
}
