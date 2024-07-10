using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Client.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "houseCleaningLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractorsName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationCoordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    WeatherCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkCompletionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DescriptionOfWorkPerformed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifficultiesOrObstaclesEncountered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralCommentsOrObservations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_houseCleaningLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "landscapingLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractorsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubContractorsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationCoordinates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    WeatherCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkCompletionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DescriptionOfWorkPerformed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DifficultiesOrObstaclesEncountered = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuppliesUsed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralCommentsOrObservations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreWorkPhotosPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostWorkPhotosPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_landscapingLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "houseCleaningLogs");

            migrationBuilder.DropTable(
                name: "landscapingLogs");
        }
    }
}
