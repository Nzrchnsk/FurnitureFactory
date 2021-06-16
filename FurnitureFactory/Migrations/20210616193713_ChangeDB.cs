using Microsoft.EntityFrameworkCore.Migrations;

namespace FurnitureFactory.Migrations
{
    public partial class ChangeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Kitchens_KitchenId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "KitchenFurnitureModules");

            migrationBuilder.DropTable(
                name: "Kitchens");

            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ClientPhone",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "KitchenId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Orders",
                newName: "CratedAt");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Orders",
                newName: "TotalCast");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_KitchenId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "FurnitureModules",
                newName: "Photo");

            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FurnitureModules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_FurnitureModules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "FurnitureModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ModuleId",
                table: "Orders",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ModuleId",
                table: "Sales",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_OrderId",
                table: "Sales",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_FurnitureModules_ModuleId",
                table: "Orders",
                column: "ModuleId",
                principalTable: "FurnitureModules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_FurnitureModules_ModuleId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ModuleId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FurnitureModules");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "KitchenId");

            migrationBuilder.RenameColumn(
                name: "TotalCast",
                table: "Orders",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "CratedAt",
                table: "Orders",
                newName: "Date");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_KitchenId");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "FurnitureModules",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientPhone",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Kitchens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitchens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KitchenFurnitureModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FurnitureModuleId = table.Column<int>(type: "int", nullable: false),
                    KitchenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitchenFurnitureModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KitchenFurnitureModules_FurnitureModules_FurnitureModuleId",
                        column: x => x.FurnitureModuleId,
                        principalTable: "FurnitureModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KitchenFurnitureModules_Kitchens_KitchenId",
                        column: x => x.KitchenId,
                        principalTable: "Kitchens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KitchenFurnitureModules_FurnitureModuleId",
                table: "KitchenFurnitureModules",
                column: "FurnitureModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_KitchenFurnitureModules_KitchenId",
                table: "KitchenFurnitureModules",
                column: "KitchenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Kitchens_KitchenId",
                table: "Orders",
                column: "KitchenId",
                principalTable: "Kitchens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
