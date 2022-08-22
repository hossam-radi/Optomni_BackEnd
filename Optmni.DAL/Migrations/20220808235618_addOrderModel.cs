using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Optmni.DAL.Migrations
{
    public partial class addOrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateOtpCodeTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OtpCodeReqCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OtpCodeReqTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OtpWrongTrials",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WrongTrialTime",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "OtpCode",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "GrowerId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "OtpCode");

            migrationBuilder.AlterColumn<Guid>(
                name: "GrowerId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOtpCodeTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OtpCodeReqCount",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpCodeReqTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OtpWrongTrials",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WrongTrialTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
