using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockWallet.Db.Mysql.Migrations
{
    /// <inheritdoc />
    public partial class SummaryControl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastProcessedId",
                table: "Summaries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastProcessedId",
                table: "Summaries");
        }
    }
}
