using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZombieParty.Migrations
{
    /// <inheritdoc />
    public partial class SeedMonWeapon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "WeaponId", "CreatedDate", "Description", "Force", "Image", "Name", "Price", "Qty", "QtyBought" },
                values: new object[] { 3, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "A sacred relic designed to smite the undead with holy fire.", 350m, "https://example.com", "The Exorcist", 750m, 5, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "WeaponId",
                keyValue: 3);
        }
    }
}
