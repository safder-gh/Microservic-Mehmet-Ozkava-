using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ComeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedAtAt",
                table: "Products",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAtAt",
                table: "Orders",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAtAt",
                table: "OrderItems",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAtAt",
                table: "Customers",
                newName: "LastModifiedAt");

            migrationBuilder.AlterColumn<int>(
                name: "Payment_PaymentMethod",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Products",
                newName: "LastModifiedAtAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Orders",
                newName: "LastModifiedAtAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "OrderItems",
                newName: "LastModifiedAtAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Customers",
                newName: "LastModifiedAtAt");

            migrationBuilder.AlterColumn<string>(
                name: "Payment_PaymentMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
