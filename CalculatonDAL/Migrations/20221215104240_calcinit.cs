using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CalculatonDAL.Migrations
{
    /// <inheritdoc />
    public partial class calcinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "calcution_item",
                columns: table => new
                {
                    calculationid = table.Column<int>(name: "calculation_id", type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numberone = table.Column<int>(name: "number_one", type: "integer", nullable: false),
                    numbertwo = table.Column<int>(name: "number_two", type: "integer", nullable: false),
                    operationcode = table.Column<string>(name: "operation_code", type: "text", nullable: false),
                    resultvalue = table.Column<int>(name: "result_value", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calcution_item", x => x.calculationid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calcution_item");
        }
    }
}
