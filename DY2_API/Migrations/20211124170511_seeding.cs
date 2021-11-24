using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DY2_API.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_RentedToId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "RentedToId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { -2, "Haruki", "Murakami" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { -1, "Umberto", "Eco" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[] { -1, "jsmith@example.com", "John", "Smith" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Name", "Publisher", "RentedToId" },
                values: new object[] { -3, -2, "Kafka on the Shore", "Arctic Editions", null });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Name", "Publisher", "RentedToId" },
                values: new object[] { -2, -1, "The Limits of Interpretation", "Fixed House", null });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Name", "Publisher", "RentedToId" },
                values: new object[] { -1, -1, "The Name of the Rose", "Fixed House", -1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Members_RentedToId",
                table: "Books",
                column: "RentedToId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_RentedToId",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "RentedToId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Members_RentedToId",
                table: "Books",
                column: "RentedToId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
