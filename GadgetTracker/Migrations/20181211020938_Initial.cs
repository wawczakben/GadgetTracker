using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GadgetTracker.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GadgetLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GadgetLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Households",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Households", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roommates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    f_name = table.Column<string>(nullable: true),
                    l_name = table.Column<string>(nullable: true),
                    age = table.Column<int>(nullable: false),
                    HouseholdId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roommates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roommates_Households_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Households",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gadgets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    GadgetLocationId = table.Column<int>(nullable: true),
                    RoommateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gadgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gadgets_GadgetLocations_GadgetLocationId",
                        column: x => x.GadgetLocationId,
                        principalTable: "GadgetLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gadgets_Roommates_RoommateId",
                        column: x => x.RoommateId,
                        principalTable: "Roommates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gadgets_GadgetLocationId",
                table: "Gadgets",
                column: "GadgetLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Gadgets_RoommateId",
                table: "Gadgets",
                column: "RoommateId");

            migrationBuilder.CreateIndex(
                name: "IX_Roommates_HouseholdId",
                table: "Roommates",
                column: "HouseholdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gadgets");

            migrationBuilder.DropTable(
                name: "GadgetLocations");

            migrationBuilder.DropTable(
                name: "Roommates");

            migrationBuilder.DropTable(
                name: "Households");
        }
    }
}
