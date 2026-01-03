namespace Library_BDwAI;

using Microsoft.Data.Sqlite;
using System.Diagnostics;

public static class DatabaseHelper
{
    public static void InitializeDatabase(string contentRootPath)
    {
        var dbPath = Path.GetFullPath(Path.Combine(contentRootPath, "Databases", "library.db"));
        var connectionString = $"Data Source={dbPath}";

        Debug.WriteLine($"[DB] DbPath='{dbPath}'");
        Debug.WriteLine($"[DB] ContentRootPath='{contentRootPath}'");

        var directory = Path.GetDirectoryName(dbPath);
        if (string.IsNullOrWhiteSpace(directory))
        {
            throw new InvalidOperationException($"Invalid DbPath '{dbPath}'.");
        }

        Directory.CreateDirectory(directory);

        if (!File.Exists(dbPath))
        {
            using (File.Create(dbPath))
            {
            }
        }

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var pragmaCmd = new SqliteCommand("PRAGMA foreign_keys = ON;", connection))
            {
                pragmaCmd.ExecuteNonQuery();
            }

            const string createTablesQuery = @"
                CREATE TABLE IF NOT EXISTS Genres (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FirstName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    Email TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL,
                    PhoneNumber TEXT,
                    IsAdmin INTEGER NOT NULL DEFAULT 0
                );

                CREATE TABLE IF NOT EXISTS Books (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Author TEXT NOT NULL,
                    ISBN TEXT NOT NULL,
                    ReleaseYear INTEGER NOT NULL,
                    GenreId INTEGER NOT NULL,
                    FOREIGN KEY(GenreId) REFERENCES Genres(Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Loans (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    LoanDate TEXT NOT NULL,
                    ReturnDate TEXT,
                    BookId INTEGER NOT NULL,
                    UserId INTEGER NOT NULL,
                    FOREIGN KEY(BookId) REFERENCES Books(Id) ON DELETE CASCADE,
                    FOREIGN KEY(UserId) REFERENCES Users(Id) ON DELETE CASCADE
                );
            ";

            using (var command = new SqliteCommand(createTablesQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}