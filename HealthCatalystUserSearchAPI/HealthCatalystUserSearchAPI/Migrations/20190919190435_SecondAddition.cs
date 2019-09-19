using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthCatalystUserSearchAPI.Migrations
{
    public partial class SecondAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "State", "Street1", "Street2", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("9e2a29f7-9fa7-4cfa-b90c-6aacc2019729"), "ThatCity", "TheCountry", "ThatState", "1 Tree Place", null, "010123" },
                    { new Guid("1869874b-3a8a-4800-a247-21187815c407"), "TheCity", "ThatCountry", "TheState", "45 Pike Street", null, "010321" }
                });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "InterestName", "InterestType" },
                values: new object[,]
                {
                    { new Guid("09bd236a-673c-4d06-a259-549008f22b03"), "Soccer", "Sport" },
                    { new Guid("e13dc427-55ec-4f13-932c-ab8d5b1d08f4"), "Cooking", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "MyAddressId" },
                values: new object[] { new Guid("e04a1983-be52-428c-bd3c-e6004e3a5992"), "John", "James", new Guid("9e2a29f7-9fa7-4cfa-b90c-6aacc2019729") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "MyAddressId" },
                values: new object[] { new Guid("2517d71a-0152-48b1-bf16-2ab75be91a6f"), "Chris", "Handle", new Guid("1869874b-3a8a-4800-a247-21187815c407") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "MyAddressId" },
                values: new object[] { new Guid("feaf87e5-5bf7-4b7a-a8fc-22b0b4c06aeb"), "Mukesh", "Mukesh", new Guid("1869874b-3a8a-4800-a247-21187815c407") });

            migrationBuilder.InsertData(
                table: "UserToInterest",
                columns: new[] { "UserId", "InterestId" },
                values: new object[] { new Guid("e04a1983-be52-428c-bd3c-e6004e3a5992"), new Guid("e13dc427-55ec-4f13-932c-ab8d5b1d08f4") });

            migrationBuilder.InsertData(
                table: "UserToInterest",
                columns: new[] { "UserId", "InterestId" },
                values: new object[] { new Guid("2517d71a-0152-48b1-bf16-2ab75be91a6f"), new Guid("09bd236a-673c-4d06-a259-549008f22b03") });

            migrationBuilder.InsertData(
                table: "UserToInterest",
                columns: new[] { "UserId", "InterestId" },
                values: new object[] { new Guid("feaf87e5-5bf7-4b7a-a8fc-22b0b4c06aeb"), new Guid("e13dc427-55ec-4f13-932c-ab8d5b1d08f4") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserToInterest",
                keyColumns: new[] { "UserId", "InterestId" },
                keyValues: new object[] { new Guid("2517d71a-0152-48b1-bf16-2ab75be91a6f"), new Guid("09bd236a-673c-4d06-a259-549008f22b03") });

            migrationBuilder.DeleteData(
                table: "UserToInterest",
                keyColumns: new[] { "UserId", "InterestId" },
                keyValues: new object[] { new Guid("e04a1983-be52-428c-bd3c-e6004e3a5992"), new Guid("e13dc427-55ec-4f13-932c-ab8d5b1d08f4") });

            migrationBuilder.DeleteData(
                table: "UserToInterest",
                keyColumns: new[] { "UserId", "InterestId" },
                keyValues: new object[] { new Guid("feaf87e5-5bf7-4b7a-a8fc-22b0b4c06aeb"), new Guid("e13dc427-55ec-4f13-932c-ab8d5b1d08f4") });

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: new Guid("09bd236a-673c-4d06-a259-549008f22b03"));

            migrationBuilder.DeleteData(
                table: "Interests",
                keyColumn: "Id",
                keyValue: new Guid("e13dc427-55ec-4f13-932c-ab8d5b1d08f4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2517d71a-0152-48b1-bf16-2ab75be91a6f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e04a1983-be52-428c-bd3c-e6004e3a5992"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("feaf87e5-5bf7-4b7a-a8fc-22b0b4c06aeb"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("1869874b-3a8a-4800-a247-21187815c407"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("9e2a29f7-9fa7-4cfa-b90c-6aacc2019729"));
        }
    }
}
