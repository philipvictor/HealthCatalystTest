using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthCatalystUserSearchAPI.Migrations
{
    public partial class ThreeAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "InterestName", "InterestType" },
                values: new object[] { new Guid("538afe79-3653-4061-b597-3011d79c3630"), "Rugby", "Sport" });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "InterestName", "InterestType" },
                values: new object[] { new Guid("98d60074-3ad1-4803-807f-6922982665ae"), "Baking", null });

            migrationBuilder.InsertData(
                table: "UserToInterest",
                columns: new[] { "UserId", "InterestId" },
                values: new object[] { new Guid("2517d71a-0152-48b1-bf16-2ab75be91a6f"), new Guid("538afe79-3653-4061-b597-3011d79c3630") });

            migrationBuilder.InsertData(
                table: "UserToInterest",
                columns: new[] { "UserId", "InterestId" },
                values: new object[] { new Guid("feaf87e5-5bf7-4b7a-a8fc-22b0b4c06aeb"), new Guid("98d60074-3ad1-4803-807f-6922982665ae") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserToInterest",
                keyColumns: new[] { "UserId", "InterestId" },
                keyValues: new object[] { new Guid("2517d71a-0152-48b1-bf16-2ab75be91a6f"), new Guid("538afe79-3653-4061-b597-3011d79c3630") });

            migrationBuilder.DeleteData(
                table: "UserToInterest",
                keyColumns: new[] { "UserId", "InterestId" },
                keyValues: new object[] { new Guid("feaf87e5-5bf7-4b7a-a8fc-22b0b4c06aeb"), new Guid("98d60074-3ad1-4803-807f-6922982665ae") });

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: new Guid("538afe79-3653-4061-b597-3011d79c3630"));

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: new Guid("98d60074-3ad1-4803-807f-6922982665ae"));
        }
    }
}
