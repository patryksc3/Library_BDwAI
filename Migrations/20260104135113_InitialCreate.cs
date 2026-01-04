using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library_BDwAI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    ISBN = table.Column<string>(type: "TEXT", nullable: false),
                    PublishedYear = table.Column<int>(type: "INTEGER", nullable: false),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    CopiesAvailable = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LoanDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fantasy" },
                    { 2, "Science Fiction" },
                    { 3, "Romans" },
                    { 4, "Biografia" },
                    { 5, "Naukowy" },
                    { 6, "Kryminał" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsAdmin", "LastName", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "a@example.com", "Admin", true, "Admin", "AdminPass123", "" },
                    { 2, "john24@example.com", "John", false, "Smith", "UserPass456", "" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CopiesAvailable", "GenreId", "ISBN", "PublishedYear", "Title" },
                values: new object[,]
                {
                    { 1, "J.R.R. Tolkien", 5, 1, "412351", 1954, "Władca Pierścieni" },
                    { 2, "George Orwell", 3, 2, "465351", 1949, "1984" },
                    { 3, "Jane Austen", 3, 3, "413142", 1813, "Duma i uprzedzenie" },
                    { 4, "Walter Isaacson", 4, 4, "422355", 2011, "Steve Jobs" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserId",
                table: "Loans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
