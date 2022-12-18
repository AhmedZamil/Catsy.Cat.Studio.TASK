using Microsoft.EntityFrameworkCore.Migrations;

namespace CatsyCatStudio.Migrations
{
    public partial class AddStarRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "StarRating",
                table: "Pies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarRating",
                table: "Pies");
        }
    }
}
