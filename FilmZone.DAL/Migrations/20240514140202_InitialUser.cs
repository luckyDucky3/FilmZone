using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FilmZone.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Film",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Name = table.Column<string>(type: "text", nullable: false),
            //        Description = table.Column<string>(type: "text", nullable: false),
            //        PathToImage = table.Column<string>(type: "text", nullable: false),
            //        FilmOrSerial = table.Column<int>(type: "integer", nullable: false),
            //        Type = table.Column<int>(type: "integer", nullable: false),
            //        ReleaseFilmDate = table.Column<int>(type: "integer", nullable: false),
            //        Director = table.Column<string>(type: "text", nullable: false),
            //        Preview = table.Column<string>(type: "text", nullable: false),
            //        Links = table.Column<List<string>>(type: "text[]", nullable: false),
            //        Price = table.Column<List<string>>(type: "text[]", nullable: false),
            //        Advertisement = table.Column<List<string>>(type: "text[]", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Film", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    LoginName = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
