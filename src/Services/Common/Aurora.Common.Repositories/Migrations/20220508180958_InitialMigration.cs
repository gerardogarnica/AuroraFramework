using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aurora.Common.Repositories.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "COM");

            migrationBuilder.CreateTable(
                name: "AttributeSetting",
                schema: "COM",
                columns: table => new
                {
                    AttributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(40)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ScopeType = table.Column<string>(type: "varchar(20)", nullable: false),
                    DataType = table.Column<string>(type: "varchar(10)", nullable: false),
                    Configuration = table.Column<string>(type: "xml", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeSetting", x => x.AttributeId);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                schema: "COM",
                columns: table => new
                {
                    CatalogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(40)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.CatalogId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "COM",
                columns: table => new
                {
                    CountryId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    OfficialName = table.Column<string>(type: "varchar(100)", nullable: false),
                    TwoLettersCode = table.Column<string>(type: "char(2)", nullable: false),
                    ThreeLettersCode = table.Column<string>(type: "char(3)", nullable: false),
                    ThreeDigitsCode = table.Column<string>(type: "char(3)", nullable: false),
                    InternetPrefix = table.Column<string>(type: "char(3)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "AttributeValue",
                schema: "COM",
                columns: table => new
                {
                    AttributeId = table.Column<int>(type: "int", nullable: false),
                    RelationshipId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "xml", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValue", x => x.AttributeId);
                    table.ForeignKey(
                        name: "FK_AttributeValue_AttributeSetting",
                        column: x => x.AttributeId,
                        principalSchema: "COM",
                        principalTable: "AttributeSetting",
                        principalColumn: "AttributeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItem",
                schema: "COM",
                columns: table => new
                {
                    CatalogItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "varchar(40)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsEditable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItem", x => x.CatalogItemId);
                    table.ForeignKey(
                        name: "FK_CatalogItem_Catalog",
                        column: x => x.CatalogId,
                        principalSchema: "COM",
                        principalTable: "Catalog",
                        principalColumn: "CatalogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryDivision",
                schema: "COM",
                columns: table => new
                {
                    DivisionId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    LevelNumber = table.Column<int>(type: "int", nullable: false),
                    IsCityLevel = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDivision", x => x.DivisionId);
                    table.ForeignKey(
                        name: "FK_CountryDivision_Country",
                        column: x => x.CountryId,
                        principalSchema: "COM",
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                schema: "COM",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DivisionId = table.Column<short>(type: "smallint", nullable: false),
                    ParentLocationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Code = table.Column<string>(type: "varchar(5)", nullable: true),
                    AlternativeCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(35)", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Location_CountryDivision",
                        column: x => x.DivisionId,
                        principalSchema: "COM",
                        principalTable: "CountryDivision",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UK_AttributeSetting",
                schema: "COM",
                table: "AttributeSetting",
                columns: new[] { "Code", "ScopeType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_AttributeValue",
                schema: "COM",
                table: "AttributeValue",
                columns: new[] { "AttributeId", "RelationshipId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Catalog",
                schema: "COM",
                table: "Catalog",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_CatalogItem",
                schema: "COM",
                table: "CatalogItem",
                columns: new[] { "CatalogId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Country",
                schema: "COM",
                table: "Country",
                column: "ThreeLettersCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryDivision_CountryId",
                schema: "COM",
                table: "CountryDivision",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_DivisionId",
                schema: "COM",
                table: "Location",
                column: "DivisionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeValue",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "CatalogItem",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "Location",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "AttributeSetting",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "Catalog",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "CountryDivision",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "COM");
        }
    }
}
