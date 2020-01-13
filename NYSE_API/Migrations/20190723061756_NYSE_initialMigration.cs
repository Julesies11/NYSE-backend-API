using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NYSE.API.Migrations
{
    public partial class NYSE_initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyPrice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(nullable: false),
                    stock_symbol = table.Column<string>(maxLength: 10, nullable: false),
                    stock_price_open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock_price_close = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock_price_low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock_price_high = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock_price_adj_close = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock_volume = table.Column<int>(nullable: false),
                    stock_exchange = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPrice", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyPrice");
        }
    }
}
