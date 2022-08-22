using Microsoft.EntityFrameworkCore.Migrations;
using Optmni.Utilities.Constants;
using System;

namespace Optmni.DAL.Migrations
{
    public partial class insertSeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "AspNetRoles"
               , columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" }
               , values: new object[] { Guid.NewGuid().ToString() ,
                                         OptmniConstants.CustomerRole ,
                                         OptmniConstants.CustomerRole.ToUpper(),
                                         Guid.NewGuid().ToString() });

            migrationBuilder.InsertData(table: "AspNetRoles"
               , columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" }
               , values: new object[] { Guid.NewGuid().ToString() ,
                                         OptmniConstants.GrowersRole ,
                                         OptmniConstants.GrowersRole.ToUpper(),
                                         Guid.NewGuid().ToString() });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from AspNetRoles ");
        }
    }
}
