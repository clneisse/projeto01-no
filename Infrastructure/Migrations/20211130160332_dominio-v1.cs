using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UStart.Infrastructure.Migrations
{
    public partial class dominiov1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "formas_pagamentos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: true),
                    dias = table.Column<ICollection<string>>(type: "jsonb", nullable: true),
                    desconto = table.Column<decimal>(type: "numeric", nullable: false),
                    codigo_externo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_formas_pagamentos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "caixas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_caixa = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    forma_pagamento_id = table.Column<Guid>(type: "uuid", nullable: false),
                    observacao = table.Column<string>(type: "text", nullable: true),
                    quantidade_de_itens = table.Column<decimal>(type: "numeric", nullable: false),
                    total_itens = table.Column<decimal>(type: "numeric", nullable: false),
                    total_desconto = table.Column<decimal>(type: "numeric", nullable: false),
                    total_produtos = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_caixas", x => x.id);
                    table.ForeignKey(
                        name: "fk_caixas_formas_pagamentos_forma_pagamento_id",
                        column: x => x.forma_pagamento_id,
                        principalTable: "formas_pagamentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_caixas_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_pedido = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    forma_pagamento_id = table.Column<Guid>(type: "uuid", nullable: false),
                    observacao = table.Column<string>(type: "text", nullable: true),
                    quantidade_de_itens = table.Column<decimal>(type: "numeric", nullable: false),
                    preco_unitario = table.Column<decimal>(type: "numeric", nullable: false),
                    total_itens = table.Column<decimal>(type: "numeric", nullable: false),
                    total_desconto = table.Column<decimal>(type: "numeric", nullable: false),
                    total_produtos = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pedidos", x => x.id);
                    table.ForeignKey(
                        name: "fk_pedidos_formas_pagamentos_forma_pagamento_id",
                        column: x => x.forma_pagamento_id,
                        principalTable: "formas_pagamentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pedidos_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "caixas_itens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    caixa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produto_id = table.Column<Guid>(type: "uuid", nullable: false),
                    observacao = table.Column<string>(type: "text", nullable: true),
                    quantidade = table.Column<decimal>(type: "numeric", nullable: false),
                    preco_unitario = table.Column<decimal>(type: "numeric", nullable: false),
                    desconto = table.Column<decimal>(type: "numeric", nullable: false),
                    total_unitario = table.Column<decimal>(type: "numeric", nullable: false),
                    total_item = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_caixas_itens", x => x.id);
                    table.ForeignKey(
                        name: "fk_caixas_itens_caixas_caixa_id",
                        column: x => x.caixa_id,
                        principalTable: "caixas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_caixas_itens_produtos_produto_id",
                        column: x => x.produto_id,
                        principalTable: "produtos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedidos_itens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    pedido_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produto_id = table.Column<Guid>(type: "uuid", nullable: false),
                    observacao = table.Column<string>(type: "text", nullable: true),
                    quantidade = table.Column<decimal>(type: "numeric", nullable: false),
                    preco_unitario = table.Column<decimal>(type: "numeric", nullable: false),
                    desconto = table.Column<decimal>(type: "numeric", nullable: false),
                    total_unitario = table.Column<decimal>(type: "numeric", nullable: false),
                    total_item = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pedidos_itens", x => x.id);
                    table.ForeignKey(
                        name: "fk_pedidos_itens_pedidos_pedido_id",
                        column: x => x.pedido_id,
                        principalTable: "pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pedidos_itens_produtos_produto_id",
                        column: x => x.produto_id,
                        principalTable: "produtos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_caixas_forma_pagamento_id",
                table: "caixas",
                column: "forma_pagamento_id");

            migrationBuilder.CreateIndex(
                name: "ix_caixas_usuario_id",
                table: "caixas",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_caixas_itens_caixa_id",
                table: "caixas_itens",
                column: "caixa_id");

            migrationBuilder.CreateIndex(
                name: "ix_caixas_itens_produto_id",
                table: "caixas_itens",
                column: "produto_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedidos_forma_pagamento_id",
                table: "pedidos",
                column: "forma_pagamento_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedidos_usuario_id",
                table: "pedidos",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedidos_itens_pedido_id",
                table: "pedidos_itens",
                column: "pedido_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedidos_itens_produto_id",
                table: "pedidos_itens",
                column: "produto_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "caixas_itens");

            migrationBuilder.DropTable(
                name: "pedidos_itens");

            migrationBuilder.DropTable(
                name: "caixas");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "formas_pagamentos");
        }
    }
}
