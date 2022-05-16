using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarMarket.Data.Migrations
{
    public partial class AddedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25c4bd5e-aec1-436e-8b6f-6567b29d8735");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bea7e54-0451-4d55-8c21-ca84757dc1a3");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "qwe", "qwe" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "qwe");

            migrationBuilder.DropColumn(
                name: "CarType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cars");

            migrationBuilder.AddColumn<long>(
                name: "ModelId",
                table: "Cars",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Year",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "admin", "d6403fdd-14e4-4769-91b3-d215b5962b13", "Admin", "ADMIN" },
                    { "user", "f196df7e-55fc-4c84-bde2-92d34ca66931", "User", "USER" },
                    { "c4a2c6b2-66f3-4ace-a74a-2f9f9e5515cc", "c9eb21a0-fc94-4395-9471-c03650b9f128", "Guest", "GUEST" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwe",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "63327caf-01ad-488b-8ff1-48e861607228", "0a7ba7a6-531d-4e81-bfa1-06c2f9819e14" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwerty",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "021f2728-45f4-47ce-bb93-18f909cf0614", "7c62e502-cb4e-4965-b8e1-a717c884ea40" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 32L, "Mini" },
                    { 31L, "Renault" },
                    { 30L, "Peugeot" },
                    { 29L, "Mitsubishi" },
                    { 20L, "Mazda" },
                    { 27L, "Volvo" },
                    { 26L, "Mercedes-Benz" },
                    { 25L, "Volkswagen" },
                    { 24L, "Lexus" },
                    { 23L, "Bugatti" },
                    { 22L, "Alfa Romeo" },
                    { 21L, "Nissan" },
                    { 33L, "Citroën" },
                    { 28L, "Kia" },
                    { 19L, "Jaguar" },
                    { 17L, "Dodge" },
                    { 2L, "BMW" },
                    { 3L, "Ferrari" },
                    { 4L, "Ford" },
                    { 5L, "Porsche" },
                    { 6L, "Honda" },
                    { 7L, "Lamborghini" },
                    { 8L, "Toyota" },
                    { 9L, "Bentley" },
                    { 10L, "Maserati" },
                    { 11L, "Audi" },
                    { 12L, "Subaru" },
                    { 13L, "Caillac" },
                    { 14L, "Jeep" },
                    { 15L, "Chrysler" },
                    { 16L, "Chevrolet" },
                    { 18L, "Hyundai" },
                    { 1L, "Tesla" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "user", "qwerty" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "admin", "qwe" });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ModelId",
                table: "Cars",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ModelId",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4a2c6b2-66f3-4ace-a74a-2f9f9e5515cc");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "admin", "qwe" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "user", "qwerty" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "user");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "CarType",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25c4bd5e-aec1-436e-8b6f-6567b29d8735", "7b845de4-4810-41d3-b7a2-5ae6ffc19adf", "Guest", "GUEST" },
                    { "7bea7e54-0451-4d55-8c21-ca84757dc1a3", "a73241cc-ced1-41fb-a5fe-49d648e003ff", "User", "USER" },
                    { "qwe", "993e7cef-7f9d-46e5-8373-c23efffb3fd5", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwe",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "50de5f36-60d4-4e6d-815f-641887f8e58a", "ad5594a8-d87e-4c5b-8ddc-a71a0c98676c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "qwerty",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bc25c25a-981c-4e59-881a-8e2d6dc7d609", "e4740963-a40b-4bd3-a301-c29d98fdb97f" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "qwe", "qwe" });
        }
    }
}
