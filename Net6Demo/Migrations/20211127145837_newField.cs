using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Net6Demo.Migrations
{
    public partial class newField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YetAnotherField",
                table: "ToDoItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YetAnotherField",
                table: "ToDoItems");
        }
    }
}
