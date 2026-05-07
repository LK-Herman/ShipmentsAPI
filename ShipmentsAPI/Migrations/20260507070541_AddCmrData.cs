using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipmentsAPI.Migrations
{
    public partial class AddCmrData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CmrData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsigneeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsigneeStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsigneeCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsigneeCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoadingPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsMarks1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsMarks2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsMarks3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsMarks4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsMarks5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsUN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsClassUN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsPGUN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDgd = table.Column<bool>(type: "bit", nullable: false),
                    IsLine3Active = table.Column<bool>(type: "bit", nullable: false),
                    IsLine4Active = table.Column<bool>(type: "bit", nullable: false),
                    IsLine5Active = table.Column<bool>(type: "bit", nullable: false),
                    IsAdrRegulated = table.Column<bool>(type: "bit", nullable: false),
                    IsOverpack = table.Column<bool>(type: "bit", nullable: false),
                    GoodsQty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsNet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoodsCBM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpedCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpedCarPlates = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmrData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmrData_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CmrData_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmrData_CustomerId",
                table: "CmrData",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CmrData_ShipmentId_CustomerId",
                table: "CmrData",
                columns: new[] { "ShipmentId", "CustomerId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmrData");
        }
    }
}
