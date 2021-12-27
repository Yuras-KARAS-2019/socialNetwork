using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Faroosom.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedData = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.UniqueConstraint("AK_Users_Login", x => x.Login);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromId = table.Column<int>(type: "int", nullable: false),
                    ToId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedData = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_FromId",
                        column: x => x.FromId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Users_ToId",
                        column: x => x.ToId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriberId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    CreatedData = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.UniqueConstraint("AK_Subscriptions_SubscriberId_PublisherId", x => new { x.SubscriberId, x.PublisherId });
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "CreatedData", "LastName", "Login", "Name", "Password" },
                values: new object[] { 1, 0, new DateTime(2021, 12, 1, 0, 1, 8, 327, DateTimeKind.Local).AddTicks(9404), "Durda", "Sniper2001", "Roma", "QWERTY1234" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "CreatedData", "LastName", "Login", "Name", "Password" },
                values: new object[] { 2, 0, new DateTime(2021, 12, 1, 0, 1, 8, 329, DateTimeKind.Local).AddTicks(8572), "Durda", "Killer1995", "Nadia", "QWERTY1234" });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "CreatedData", "PublisherId", "SubscriberId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "CreatedData", "PublisherId", "SubscriberId" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromId",
                table: "Messages",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToId",
                table: "Messages",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PublisherId",
                table: "Subscriptions",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
