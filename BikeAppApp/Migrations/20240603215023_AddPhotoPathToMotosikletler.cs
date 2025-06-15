using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeAppApp.Migrations
{
    public partial class AddPhotoPathToMotosikletler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoğrafYolu",
                table: "Motosikletler",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoğrafYolu",
                table: "Motosikletler");
        }
    }
}
