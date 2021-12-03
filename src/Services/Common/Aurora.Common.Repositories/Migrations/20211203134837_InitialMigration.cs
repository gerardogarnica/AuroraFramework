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
                name: "Atributo",
                schema: "COM",
                columns: table => new
                {
                    IdAtributo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(40)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    TipoAmbito = table.Column<string>(type: "varchar(20)", nullable: false),
                    TipoDato = table.Column<string>(type: "varchar(10)", nullable: false),
                    Configuracion = table.Column<string>(type: "xml", nullable: false),
                    EsVisible = table.Column<bool>(type: "bit", nullable: false),
                    EsEditable = table.Column<bool>(type: "bit", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atributo", x => x.IdAtributo);
                });

            migrationBuilder.CreateTable(
                name: "Catalogo",
                schema: "COM",
                columns: table => new
                {
                    IdCatalogo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(40)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    EsVisible = table.Column<bool>(type: "bit", nullable: false),
                    EsEditable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogo", x => x.IdCatalogo);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                schema: "COM",
                columns: table => new
                {
                    IdPais = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    NombreOficial = table.Column<string>(type: "varchar(100)", nullable: false),
                    CodigoDosLetras = table.Column<string>(type: "char(2)", nullable: false),
                    CodigoTresLetras = table.Column<string>(type: "char(3)", nullable: false),
                    CodigoTresDigitos = table.Column<string>(type: "char(3)", nullable: false),
                    PrefijoInternet = table.Column<string>(type: "char(3)", nullable: true),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.IdPais);
                });

            migrationBuilder.CreateTable(
                name: "ValorAtributo",
                schema: "COM",
                columns: table => new
                {
                    IdAtributo = table.Column<int>(type: "int", nullable: false),
                    IdRelacion = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<string>(type: "xml", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValorAtributo", x => x.IdAtributo);
                    table.ForeignKey(
                        name: "FK_ValorAtributo_Atributo",
                        column: x => x.IdAtributo,
                        principalSchema: "COM",
                        principalTable: "Atributo",
                        principalColumn: "IdAtributo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogoItem",
                schema: "COM",
                columns: table => new
                {
                    IdCatalogoItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCatalogo = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(40)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    EsEditable = table.Column<bool>(type: "bit", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogoItem", x => x.IdCatalogoItem);
                    table.ForeignKey(
                        name: "FK_CatalogoItem_Catalogo",
                        column: x => x.IdCatalogo,
                        principalSchema: "COM",
                        principalTable: "Catalogo",
                        principalColumn: "IdCatalogo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaisDivision",
                schema: "COM",
                columns: table => new
                {
                    IdPaisDivision = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPais = table.Column<short>(type: "smallint", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    EsNivelCiudad = table.Column<bool>(type: "bit", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaisDivision", x => x.IdPaisDivision);
                    table.ForeignKey(
                        name: "FK_PaisDivision_Pais",
                        column: x => x.IdPais,
                        principalSchema: "COM",
                        principalTable: "Pais",
                        principalColumn: "IdPais",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localidad",
                schema: "COM",
                columns: table => new
                {
                    IdLocalidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaisDivision = table.Column<short>(type: "smallint", nullable: false),
                    IdLocalidadPadre = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(5)", nullable: true),
                    CodigoAlterno = table.Column<string>(type: "varchar(10)", nullable: true),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidad", x => x.IdLocalidad);
                    table.ForeignKey(
                        name: "FK_Localidad_PaisDivision",
                        column: x => x.IdPaisDivision,
                        principalSchema: "COM",
                        principalTable: "PaisDivision",
                        principalColumn: "IdPaisDivision",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UK_Atributo",
                schema: "COM",
                table: "Atributo",
                columns: new[] { "Codigo", "TipoAmbito" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Catalogo",
                schema: "COM",
                table: "Catalogo",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_CatalogoItem",
                schema: "COM",
                table: "CatalogoItem",
                columns: new[] { "IdCatalogo", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Localidad_IdPaisDivision",
                schema: "COM",
                table: "Localidad",
                column: "IdPaisDivision");

            migrationBuilder.CreateIndex(
                name: "UK_Pais",
                schema: "COM",
                table: "Pais",
                column: "CodigoTresLetras",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaisDivision_IdPais",
                schema: "COM",
                table: "PaisDivision",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "UK_ValorAtributo",
                schema: "COM",
                table: "ValorAtributo",
                columns: new[] { "IdAtributo", "IdRelacion" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogoItem",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "Localidad",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "ValorAtributo",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "Catalogo",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "PaisDivision",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "Atributo",
                schema: "COM");

            migrationBuilder.DropTable(
                name: "Pais",
                schema: "COM");
        }
    }
}
