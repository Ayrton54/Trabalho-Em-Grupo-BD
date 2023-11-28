using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrabalhoEmGrupoBD.Migrations
{
    /// <inheritdoc />
    public partial class criacaotabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "editoras",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_editoras", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "emails",
                columns: table => new
                {
                    Emails = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emails", x => x.Emails);
                });

            migrationBuilder.CreateTable(
                name: "livrosEmprestimos",
                columns: table => new
                {
                    ISBN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Edicao = table.Column<int>(type: "int", nullable: false),
                    Custo = table.Column<double>(type: "float", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livrosEmprestimos", x => x.ISBN);
                });

            migrationBuilder.CreateTable(
                name: "autors",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emails = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autors", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_autors_emails_Emails",
                        column: x => x.Emails,
                        principalTable: "emails",
                        principalColumn: "Emails",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emails = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_clientes_emails_Emails",
                        column: x => x.Emails,
                        principalTable: "emails",
                        principalColumn: "Emails",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "livros",
                columns: table => new
                {
                    ISBN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Edicao = table.Column<int>(type: "int", nullable: false),
                    Custo = table.Column<double>(type: "float", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    autorCodigo = table.Column<int>(type: "int", nullable: false),
                    editoraCodigo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livros", x => x.ISBN);
                    table.ForeignKey(
                        name: "FK_livros_autors_autorCodigo",
                        column: x => x.autorCodigo,
                        principalTable: "autors",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_livros_editoras_editoraCodigo",
                        column: x => x.editoraCodigo,
                        principalTable: "editoras",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emprestimos",
                columns: table => new
                {
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clienteCodigo = table.Column<int>(type: "int", nullable: false),
                    livrosEmprestimoISBN = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimos", x => x.Data);
                    table.ForeignKey(
                        name: "FK_Emprestimos_clientes_clienteCodigo",
                        column: x => x.clienteCodigo,
                        principalTable: "clientes",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emprestimos_livrosEmprestimos_livrosEmprestimoISBN",
                        column: x => x.livrosEmprestimoISBN,
                        principalTable: "livrosEmprestimos",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exemplares",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataAquisicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LivroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exemplares", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_exemplares_livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "livros",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_autors_Emails",
                table: "autors",
                column: "Emails");

            migrationBuilder.CreateIndex(
                name: "IX_clientes_Emails",
                table: "clientes",
                column: "Emails");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_clienteCodigo",
                table: "Emprestimos",
                column: "clienteCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_livrosEmprestimoISBN",
                table: "Emprestimos",
                column: "livrosEmprestimoISBN");

            migrationBuilder.CreateIndex(
                name: "IX_exemplares_LivroId",
                table: "exemplares",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_livros_autorCodigo",
                table: "livros",
                column: "autorCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_livros_editoraCodigo",
                table: "livros",
                column: "editoraCodigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emprestimos");

            migrationBuilder.DropTable(
                name: "exemplares");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "livrosEmprestimos");

            migrationBuilder.DropTable(
                name: "livros");

            migrationBuilder.DropTable(
                name: "autors");

            migrationBuilder.DropTable(
                name: "editoras");

            migrationBuilder.DropTable(
                name: "emails");
        }
    }
}
