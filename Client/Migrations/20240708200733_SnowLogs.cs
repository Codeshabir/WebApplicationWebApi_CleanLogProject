using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Client.Migrations
{
    public partial class SnowLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "snowLogs",
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
                    ParkingArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParkingRamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sidewalk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StairwaysSteps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Driveways = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreWorkPhotosPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostWorkPhotosPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_snowLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "snowLogs");
        }
    }
}
