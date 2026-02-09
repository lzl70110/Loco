using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loco.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locomotives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    LocomotiveType = table.Column<int>(type: "int", nullable: false),
                    MeasuringUnit = table.Column<int>(type: "int", nullable: false, comment: " Mh or Km"),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locomotives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fuels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocoId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    InitialFuel = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    FinalFuel = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    Consumption = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    Refueled = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fuels_Locomotives_LocoId",
                        column: x => x.LocoId,
                        principalTable: "Locomotives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShiftWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocoId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Shift = table.Column<int>(type: "int", nullable: false),
                    InitialValue = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    FinalValue = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftWorks_Locomotives_LocoId",
                        column: x => x.LocoId,
                        principalTable: "Locomotives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fuels_LocoId",
                table: "Fuels",
                column: "LocoId");

            migrationBuilder.CreateIndex(
                name: "IX_Locomotives_Number",
                table: "Locomotives",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftWork_Loco_Date_Shift",
                table: "ShiftWorks",
                columns: new[] { "LocoId", "Date", "Shift" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fuels");

            migrationBuilder.DropTable(
                name: "ShiftWorks");

            migrationBuilder.DropTable(
                name: "Locomotives");
        }
    }
}
