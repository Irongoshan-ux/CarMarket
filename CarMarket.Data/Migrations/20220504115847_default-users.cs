using Microsoft.EntityFrameworkCore.Migrations;

namespace CarMarket.Data.Migrations
{
    public partial class defaultusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "becb5db9-fefc-4f2a-90cb-365122a1c4cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4ce1704-3265-40bf-83ce-a80827dea991");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "qwe",
                column: "ConcurrencyStamp",
                value: "993e7cef-7f9d-46e5-8373-c23efffb3fd5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25c4bd5e-aec1-436e-8b6f-6567b29d8735", "7b845de4-4810-41d3-b7a2-5ae6ffc19adf", "Guest", "GUEST" },
                    { "7bea7e54-0451-4d55-8c21-ca84757dc1a3", "a73241cc-ced1-41fb-a5fe-49d648e003ff", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwe",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "SecurityStamp" },
                values: new object[] { "50de5f36-60d4-4e6d-815f-641887f8e58a", "Admin", "User", "ad5594a8-d87e-4c5b-8ddc-a71a0c98676c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwerty",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "SecurityStamp" },
                values: new object[] { "bc25c25a-981c-4e59-881a-8e2d6dc7d609", "Basic", "User", "e4740963-a40b-4bd3-a301-c29d98fdb97f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25c4bd5e-aec1-436e-8b6f-6567b29d8735");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bea7e54-0451-4d55-8c21-ca84757dc1a3");

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
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "SecurityStamp" },
                values: new object[] { "3e23c42e-f3eb-43dc-a9f1-a99eb6be51d8", null, null, "665df38e-3e57-4e69-9a39-0133cdbb4f2d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwerty",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "SecurityStamp" },
                values: new object[] { "38281328-bf83-4aef-a77c-ba5963b16656", null, null, "453cef84-531c-4475-b4bb-f3661f7b03b0" });
        }
    }
}
