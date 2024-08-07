using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmZone.DAL.Migrations
{
    /// <inheritdoc />
    public partial class valueInFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "value",
                table: "film_feedback",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "film_feedback");
        }
    }
}
