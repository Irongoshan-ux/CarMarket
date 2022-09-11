using Microsoft.EntityFrameworkCore.Migrations;

namespace CarMarket.Data.Migrations
{
    public partial class DeletedPathFromImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: "877b4805-3d05-45bf-a38e-830519415cce");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "becb5db9-fefc-4f2a-90cb-365122a1c4cd", "dc2c9f5b-efac-432e-ae60-7199dd717f81", "Guest", "GUEST" },
                    { "e4ce1704-3265-40bf-83ce-a80827dea991", "4d03af8a-45fe-497b-b1ce-2d129f0bac6b", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwe",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3e23c42e-f3eb-43dc-a9f1-a99eb6be51d8", "665df38e-3e57-4e69-9a39-0133cdbb4f2d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwerty",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "38281328-bf83-4aef-a77c-ba5963b16656", "453cef84-531c-4475-b4bb-f3661f7b03b0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "becb5db9-fefc-4f2a-90cb-365122a1c4cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4ce1704-3265-40bf-83ce-a80827dea991");

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
    }
}
