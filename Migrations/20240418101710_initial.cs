using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoApp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            /*
            migrationBuilder.CreateTable(
                name: "felhasznalok",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nev = table.Column<string>(type: "NVARCHAR2(32)", maxLength: 32, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    jelszo = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: false),
                    szuletes_datuma = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    role = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_felhasznalok", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "kategoriak",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nev = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kategoriak", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orszagok",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nev = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    rovidites = table.Column<string>(type: "NVARCHAR2(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orszagok", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "albumok",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    cim = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    felhasz_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albumok", x => x.id);
                    table.ForeignKey(
                        name: "FK_albumok_felhasznalok_felhasz_id",
                        column: x => x.felhasz_id,
                        principalTable: "felhasznalok",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "palyazatok",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nev = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    hatarido = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    leiras = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    nyertes = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    kategoria_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_palyazatok", x => x.id);
                    table.ForeignKey(
                        name: "FK_palyazatok_kategoriak_kategoria_id",
                        column: x => x.kategoria_id,
                        principalTable: "kategoriak",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kepek",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    cim = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    orszag_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ertekeles = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    album_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    felhasz_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    fenykeputvonal = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kepek", x => x.id);
                    table.ForeignKey(
                        name: "FK_kepek_albumok_album_id",
                        column: x => x.album_id,
                        principalTable: "albumok",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kepek_felhasznalok_felhasz_id",
                        column: x => x.felhasz_id,
                        principalTable: "felhasznalok",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kepek_orszagok_orszag_id",
                        column: x => x.orszag_id,
                        principalTable: "orszagok",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KepKategoria",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    kategoria_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    kep_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KepKategoria", x => x.id);
                    table.ForeignKey(
                        name: "FK_KepKategoria_kategoriak_kategoria_id",
                        column: x => x.kategoria_id,
                        principalTable: "kategoriak",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KepKategoria_kepek_kep_id",
                        column: x => x.kep_id,
                        principalTable: "kepek",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kommentek",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    felhasz_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    kep_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    megjegyzes = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kommentek", x => x.id);
                    table.ForeignKey(
                        name: "FK_kommentek_felhasznalok_felhasz_id",
                        column: x => x.felhasz_id,
                        principalTable: "felhasznalok",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_kommentek_kepek_kep_id",
                        column: x => x.kep_id,
                        principalTable: "kepek",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_albumok_felhasz_id",
                table: "albumok",
                column: "felhasz_id");

            migrationBuilder.CreateIndex(
                name: "IX_kepek_album_id",
                table: "kepek",
                column: "album_id");

            migrationBuilder.CreateIndex(
                name: "IX_kepek_felhasz_id",
                table: "kepek",
                column: "felhasz_id");

            migrationBuilder.CreateIndex(
                name: "IX_kepek_orszag_id",
                table: "kepek",
                column: "orszag_id");

            migrationBuilder.CreateIndex(
                name: "IX_KepKategoria_kategoria_id",
                table: "KepKategoria",
                column: "kategoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_KepKategoria_kep_id",
                table: "KepKategoria",
                column: "kep_id");

            migrationBuilder.CreateIndex(
                name: "IX_kommentek_felhasz_id",
                table: "kommentek",
                column: "felhasz_id");

            migrationBuilder.CreateIndex(
                name: "IX_kommentek_kep_id",
                table: "kommentek",
                column: "kep_id");

            migrationBuilder.CreateIndex(
                name: "IX_palyazatok_kategoria_id",
                table: "palyazatok",
                column: "kategoria_id");
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropTable(
                name: "KepKategoria");

            migrationBuilder.DropTable(
                name: "kommentek");

            migrationBuilder.DropTable(
                name: "palyazatok");

            migrationBuilder.DropTable(
                name: "kepek");

            migrationBuilder.DropTable(
                name: "kategoriak");

            migrationBuilder.DropTable(
                name: "albumok");

            migrationBuilder.DropTable(
                name: "orszagok");

            migrationBuilder.DropTable(
                name: "felhasznalok");
            */
        }
    }
}
