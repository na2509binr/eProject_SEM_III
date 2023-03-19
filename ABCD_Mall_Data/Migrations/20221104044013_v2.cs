using Microsoft.EntityFrameworkCore.Migrations;

namespace ABCD_Mall_Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddUniqueConstraint(
               name: "UNQ_CategoriesName",
               table: "Categories",
               column: "Name",
               schema: null
               );

            migrationBuilder.AddUniqueConstraint(
                name: "UNQ_CustomerEmail",
                table: "Customers",
                column: "Email",
                schema: null
                );

            migrationBuilder.AddUniqueConstraint(
                name: "UNQ_StoresName",
                table: "Stores",
                column: "Name",
                schema: null
                );

            migrationBuilder.AddUniqueConstraint(
                name: "UNQ_GenreName",
                table: "Genres",
                column: "Name",
                schema: null
                );

            migrationBuilder.AddUniqueConstraint(
                name: "UNQ_FilmTitle",
                table: "Films",
                column: "Title",
                schema: null
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150);
        }
    }
}
