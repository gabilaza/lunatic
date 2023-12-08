using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lunatic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class C : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "text", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
            //        FirstName = table.Column<string>(type: "text", nullable: true),
            //        LastName = table.Column<string>(type: "text", nullable: false),
            //        Role = table.Column<int>(type: "integer", nullable: false),
            //        UserName = table.Column<string>(type: "text", nullable: true),
            //        NormalizedUserName = table.Column<string>(type: "text", nullable: true),
            //        Email = table.Column<string>(type: "text", nullable: true),
            //        NormalizedEmail = table.Column<string>(type: "text", nullable: true),
            //        EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
            //        PasswordHash = table.Column<string>(type: "text", nullable: true),
            //        SecurityStamp = table.Column<string>(type: "text", nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "text", nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
            //        TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
            //        LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
            //        AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Tags = table.Column<int[]>(type: "integer[]", nullable: true),
                    CommentIds = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    UserAssignIds = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    StartedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Team",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uuid", nullable: false),
            //        Name = table.Column<string>(type: "text", nullable: false),
            //        CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        LastModifiedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
            //        LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Team", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "CommentEmote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Emote = table.Column<int>(type: "integer", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentEmote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentEmote_Comment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "Id");
                });

            //migrationBuilder.CreateTable(
            //    name: "Project",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uuid", nullable: false),
            //        Title = table.Column<string>(type: "text", nullable: false),
            //        Description = table.Column<string>(type: "text", nullable: false),
            //        TaskIds = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
            //        TeamId = table.Column<Guid>(type: "uuid", nullable: true),
            //        CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        LastModifiedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
            //        LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Project", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Project_Team_TeamId",
            //            column: x => x.TeamId,
            //            principalTable: "Team",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TeamUser",
            //    columns: table => new
            //    {
            //        MembersId = table.Column<string>(type: "text", nullable: false),
            //        TeamsId = table.Column<Guid>(type: "uuid", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TeamUser", x => new { x.MembersId, x.TeamsId });
            //        table.ForeignKey(
            //            name: "FK_TeamUser_AspNetUsers_MembersId",
            //            column: x => x.MembersId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TeamUser_Team_TeamsId",
            //            column: x => x.TeamsId,
            //            principalTable: "Team",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_CommentEmote_CommentId",
                table: "CommentEmote",
                column: "CommentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Project_TeamId",
            //    table: "Project",
            //    column: "TeamId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TeamUser_TeamsId",
            //    table: "TeamUser",
            //    column: "TeamsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentEmote");

            //migrationBuilder.DropTable(
            //    name: "Project");

            migrationBuilder.DropTable(
                name: "Task");

            //migrationBuilder.DropTable(
            //    name: "TeamUser");

            migrationBuilder.DropTable(
                name: "Comment");

            //migrationBuilder.DropTable(
            //    name: "AspNetUsers");

            //migrationBuilder.DropTable(
            //    name: "Team");
        }
    }
}
