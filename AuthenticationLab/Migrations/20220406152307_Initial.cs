using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthenticationLab.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mytable",
                columns: table => new
                {
                    CRASH_ID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CRASH_DATETIME = table.Column<string>(nullable: true),
                    MAIN_ROAD_NAME = table.Column<string>(nullable: true),
                    CITY = table.Column<string>(nullable: true),
                    COUNTY_NAME = table.Column<string>(nullable: true),
                    ROUTE = table.Column<string>(nullable: true),
                    MILEPOINT = table.Column<double>(nullable: false),
                    LAT_UTM_Y = table.Column<double>(nullable: false),
                    LONG_UTM_X = table.Column<double>(nullable: false),
                    WORK_ZONE_RELATED = table.Column<string>(nullable: true),
                    PEDESTRIAN_INVOLVED = table.Column<string>(nullable: true),
                    BICYCLIST_INVOLVED = table.Column<string>(nullable: true),
                    MOTORCYCLE_INVOLVED = table.Column<string>(nullable: true),
                    IMPROPER_RESTRAINT = table.Column<string>(nullable: true),
                    UNRESTRAINED = table.Column<string>(nullable: true),
                    DUI = table.Column<string>(nullable: true),
                    INTERSECTION_RELATED = table.Column<string>(nullable: true),
                    WILD_ANIMAL_RELATED = table.Column<string>(nullable: true),
                    DOMESTIC_ANIMAL_RELATED = table.Column<string>(nullable: true),
                    OVERTURN_ROLLOVER = table.Column<string>(nullable: true),
                    COMMERCIAL_MOTOR_VEH_INVOLVED = table.Column<string>(nullable: true),
                    TEENAGE_DRIVER_INVOLVED = table.Column<string>(nullable: true),
                    OLDER_DRIVER_INVOLVED = table.Column<string>(nullable: true),
                    NIGHT_DARK_CONDITION = table.Column<string>(nullable: true),
                    SINGLE_VEHICLE = table.Column<string>(nullable: true),
                    DISTRACTED_DRIVING = table.Column<string>(nullable: true),
                    DROWSY_DRIVING = table.Column<string>(nullable: true),
                    ROADWAY_DEPARTURE = table.Column<string>(nullable: true),
                    CRASH_SEVERITY_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mytable", x => x.CRASH_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mytable");
        }
    }
}
