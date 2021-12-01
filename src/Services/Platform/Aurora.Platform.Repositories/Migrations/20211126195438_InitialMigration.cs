using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aurora.Platform.Repositories.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "APP");

            migrationBuilder.EnsureSchema(
                name: "SEG");

            migrationBuilder.CreateTable(
                name: "Aplicacion",
                schema: "APP",
                columns: table => new
                {
                    IdAplicacion = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(36)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(100)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplicacion", x => x.IdAplicacion);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                schema: "SEG",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRepositorio = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(100)", nullable: false),
                    EsPredeterminado = table.Column<bool>(type: "bit", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "SEG",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "varchar(35)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "varchar(100)", nullable: false),
                    EsPredeterminado = table.Column<bool>(type: "bit", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Componente",
                schema: "APP",
                columns: table => new
                {
                    IdComponente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicacion = table.Column<short>(type: "smallint", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(40)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(100)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componente", x => x.IdComponente);
                    table.ForeignKey(
                        name: "FK_Componente_Aplicacion",
                        column: x => x.IdAplicacion,
                        principalSchema: "APP",
                        principalTable: "Aplicacion",
                        principalColumn: "IdAplicacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repositorio",
                schema: "APP",
                columns: table => new
                {
                    IdRepositorio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAplicacion = table.Column<short>(type: "smallint", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(36)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(100)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositorio", x => x.IdRepositorio);
                    table.ForeignKey(
                        name: "FK_Repositorio_Aplicacion",
                        column: x => x.IdAplicacion,
                        principalSchema: "APP",
                        principalTable: "Aplicacion",
                        principalColumn: "IdAplicacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioCredencial",
                schema: "SEG",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Contrasena = table.Column<string>(type: "varchar(200)", nullable: false),
                    ContrasenaControl = table.Column<string>(type: "varchar(500)", nullable: false),
                    DebeCambiar = table.Column<bool>(type: "bit", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioCreacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCredencial", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_UsuarioCredencial_Usuario",
                        column: x => x.IdUsuario,
                        principalSchema: "SEG",
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPertenencia",
                schema: "SEG",
                columns: table => new
                {
                    IdPertenencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    EsPredeterminado = table.Column<bool>(type: "bit", nullable: false),
                    EsActivo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPertenencia", x => x.IdPertenencia);
                    table.ForeignKey(
                        name: "FK_UsuarioPertenencia_Rol",
                        column: x => x.IdRol,
                        principalSchema: "SEG",
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPertenencia_Usuario",
                        column: x => x.IdUsuario,
                        principalSchema: "SEG",
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepositorioDetalle",
                schema: "APP",
                columns: table => new
                {
                    IdRepositorioDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRepositorio = table.Column<int>(type: "int", nullable: false),
                    IdComponente = table.Column<int>(type: "int", nullable: false),
                    Cadena = table.Column<string>(type: "varchar(1000)", nullable: false),
                    UsuarioCreacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    UsuarioModificacion = table.Column<string>(type: "varchar(35)", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepositorioDetalle", x => x.IdRepositorioDetalle);
                    table.ForeignKey(
                        name: "FK_RepositorioDetalle_Repositorio",
                        column: x => x.IdRepositorio,
                        principalSchema: "APP",
                        principalTable: "Repositorio",
                        principalColumn: "IdRepositorio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioCredencialHistorial",
                schema: "SEG",
                columns: table => new
                {
                    IdHistorial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    NumeroCambio = table.Column<int>(type: "int", nullable: false),
                    Contrasena = table.Column<string>(type: "varchar(200)", nullable: false),
                    ContrasenaControl = table.Column<string>(type: "varchar(500)", nullable: false),
                    DebeCambiar = table.Column<bool>(type: "bit", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaFinalizacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCredencialHistorial", x => x.IdHistorial);
                    table.ForeignKey(
                        name: "FK_UsuarioCredencialHistorial_UsuarioCredencial",
                        column: x => x.IdUsuario,
                        principalSchema: "SEG",
                        principalTable: "UsuarioCredencial",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UK_Aplicacion",
                schema: "APP",
                table: "Aplicacion",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Componente",
                schema: "APP",
                table: "Componente",
                columns: new[] { "IdAplicacion", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Repositorio",
                schema: "APP",
                table: "Repositorio",
                columns: new[] { "IdAplicacion", "Codigo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_RepositorioDetalle",
                schema: "APP",
                table: "RepositorioDetalle",
                columns: new[] { "IdRepositorio", "IdComponente" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Rol",
                schema: "SEG",
                table: "Rol",
                columns: new[] { "IdRepositorio", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Usuario",
                schema: "SEG",
                table: "Usuario",
                column: "NombreUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCredencialHistorial_IdUsuario",
                schema: "SEG",
                table: "UsuarioCredencialHistorial",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPertenencia_IdRol",
                schema: "SEG",
                table: "UsuarioPertenencia",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "UK_UsuarioPertenencia",
                schema: "SEG",
                table: "UsuarioPertenencia",
                columns: new[] { "IdUsuario", "IdRol" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Componente",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "RepositorioDetalle",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "UsuarioCredencialHistorial",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "UsuarioPertenencia",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "Repositorio",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "UsuarioCredencial",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "Rol",
                schema: "SEG");

            migrationBuilder.DropTable(
                name: "Aplicacion",
                schema: "APP");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "SEG");
        }
    }
}
