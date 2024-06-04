using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmZone.DAL.Migrations
{
    /// <inheritdoc />
    public partial class lowercase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Film",
                table: "Film");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "Film",
                newName: "film");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "user",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "user",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LoginName",
                table: "user",
                newName: "login_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "user",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "film",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "film",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Preview",
                table: "film",
                newName: "preview");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "film",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Links",
                table: "film",
                newName: "links");

            migrationBuilder.RenameColumn(
                name: "Director",
                table: "film",
                newName: "director");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "film",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Advertisement",
                table: "film",
                newName: "advertisement");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "film",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ReleaseFilmDate",
                table: "film",
                newName: "release_film_date");

            migrationBuilder.RenameColumn(
                name: "PathToImage",
                table: "film",
                newName: "path_to_image");

            migrationBuilder.RenameColumn(
                name: "FilmOrSerial",
                table: "film",
                newName: "film_or_serial");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_film",
                table: "film",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_film",
                table: "film");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "film",
                newName: "Film");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "User",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "User",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "login_name",
                table: "User",
                newName: "LoginName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "User",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Film",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Film",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "preview",
                table: "Film",
                newName: "Preview");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Film",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "links",
                table: "Film",
                newName: "Links");

            migrationBuilder.RenameColumn(
                name: "director",
                table: "Film",
                newName: "Director");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Film",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "advertisement",
                table: "Film",
                newName: "Advertisement");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Film",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "release_film_date",
                table: "Film",
                newName: "ReleaseFilmDate");

            migrationBuilder.RenameColumn(
                name: "path_to_image",
                table: "Film",
                newName: "PathToImage");

            migrationBuilder.RenameColumn(
                name: "film_or_serial",
                table: "Film",
                newName: "FilmOrSerial");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Film",
                table: "Film",
                column: "Id");
        }
    }
}
