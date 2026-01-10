using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LipometryAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBodyMeasurement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_PersonBodyMeasurements_BodyMeasurementMeasurementId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_BodyMeasurementMeasurementId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "BodyMeasurementMeasurementId",
                table: "People");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BodyMeasurementMeasurementId",
                table: "People",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_BodyMeasurementMeasurementId",
                table: "People",
                column: "BodyMeasurementMeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_PersonBodyMeasurements_BodyMeasurementMeasurementId",
                table: "People",
                column: "BodyMeasurementMeasurementId",
                principalTable: "PersonBodyMeasurements",
                principalColumn: "MeasurementId");
        }
    }
}
