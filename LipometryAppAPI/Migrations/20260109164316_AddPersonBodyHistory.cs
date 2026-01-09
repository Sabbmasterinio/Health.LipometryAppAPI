using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LipometryAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonBodyHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BodyMeasurementMeasurementId",
                table: "People",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonBodyMeasurements",
                columns: table => new
                {
                    MeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeightInKg = table.Column<double>(type: "float", nullable: true),
                    HeightInCm = table.Column<double>(type: "float", nullable: true),
                    WaistInCm = table.Column<double>(type: "float", nullable: true),
                    HipInCm = table.Column<double>(type: "float", nullable: true),
                    NeckInCm = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonBodyMeasurements", x => x.MeasurementId);
                    table.ForeignKey(
                        name: "FK_PersonBodyMeasurements_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_BodyMeasurementMeasurementId",
                table: "People",
                column: "BodyMeasurementMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBodyMeasurements_PersonId",
                table: "PersonBodyMeasurements",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_PersonBodyMeasurements_BodyMeasurementMeasurementId",
                table: "People",
                column: "BodyMeasurementMeasurementId",
                principalTable: "PersonBodyMeasurements",
                principalColumn: "MeasurementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_PersonBodyMeasurements_BodyMeasurementMeasurementId",
                table: "People");

            migrationBuilder.DropTable(
                name: "PersonBodyMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_People_BodyMeasurementMeasurementId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "BodyMeasurementMeasurementId",
                table: "People");
        }
    }
}
