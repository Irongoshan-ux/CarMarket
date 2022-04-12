using Microsoft.EntityFrameworkCore.Migrations;

namespace CarMarket.Data.Migrations
{
    public partial class AddedPathInImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4204f184-4c39-436f-9de3-7ff41fcbcdac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a6ec0a9-045c-4f6b-80e0-fb68f17cd62c");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "qwe",
                column: "ConcurrencyStamp",
                value: "de8458b8-abdb-49f1-8c2c-9549b25610a6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d1aef14-c191-408f-906a-a4c73fa63b07", "8e483b90-e16e-44bb-923c-76b8a41092ba", "Guest", "GUEST" },
                    { "f8e4e3be-a01c-4bf8-9c7a-7b57abe02dd4", "af98abbf-c6b4-4da4-abe2-3a356cf55c88", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwe",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b1a1b2a6-9b34-471b-8f79-587863aae3bb", "a003a5cb-a227-4f7c-9329-56990dbb4f76" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwerty",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "63c56689-ab1c-47d1-b1f6-6be4820bff10", "24003e16-8fe0-46e7-a2c8-534aa083e079" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d1aef14-c191-408f-906a-a4c73fa63b07");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8e4e3be-a01c-4bf8-9c7a-7b57abe02dd4");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Images");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "qwe",
                column: "ConcurrencyStamp",
                value: "93fad31b-05d1-4b96-9388-93d27633c940");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7a6ec0a9-045c-4f6b-80e0-fb68f17cd62c", "b21bd87a-6060-4828-9209-84bf4cd8a710", "Guest", "GUEST" },
                    { "4204f184-4c39-436f-9de3-7ff41fcbcdac", "8bc2ff5e-3d08-435b-a516-abbc9ef905bd", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwe",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0374253d-302b-414b-814e-bb2fa09eb59d", "17dc0fcd-8e5d-425d-b078-e874486166a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwerty",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "69f7ec97-1657-472d-ae94-d9f0257b1d04", "5b1a1477-d86d-415a-96c5-f6ae3aaf309e" });
        }
    }
}
