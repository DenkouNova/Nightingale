using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Nightingale
{
    public class DatabaseCreator
    {
        private readonly FeatherLogger _logger;

        public DatabaseCreator(FeatherLogger logger)
        {
            _logger = logger;
        }

        public string CreateDatabase(string folderPath, string filename)
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            string databasePath = folderPath + @"\" + filename;
            _logger.Info("Creating database in path '" + databasePath + "'...");
            SQLiteConnection.CreateFile(databasePath);

            if (File.Exists(databasePath))
            {
                _logger.Info("File created.");
                PopulateDatabase(databasePath);
            }
            else
            {
                _logger.Error("File '" + databasePath + "' does not exist!");
            }

            _logger.CloseSectionWithReturnInfo(databasePath, location);
            return databasePath;
        }

        private void PopulateDatabase(string databasePath)
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _logger.Info("Opening database connection...");

            using (var connection = new SQLiteConnection("Data Source=" + databasePath + ";Version=3;"))
            {
                connection.Open();

                string[] createTableStatements = {
                    CREATE_TABLE_QUOTES,
                    CREATE_TABLE_WORDS,
                    CREATE_TABLE_SOURCES };

                foreach (var createOneTable in createTableStatements)
                {
                    CreateTable(createOneTable, connection);
                }

                CreateTable(ALL_DATA_VIEW, connection, "view");

                connection.Close();
            }

            _logger.CloseSection(location);
        }

        private void CreateTable(string query, SQLiteConnection connection, string objectType = "table")
        {
            _logger.Info("Creating " + objectType + " with the following query:");
            foreach (string oneLine in Regex.Split(query, "\r\n|\r|\n"))
            {
                _logger.Info(oneLine);
            }

            try
            {
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
                _logger.Info("Created " + objectType + ".");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }
            
        }

        // Don't have NOT NULL on the FK's.
        // For some reason with my mapping Source ID is updated after Quote is inserted

        private const string CREATE_TABLE_SOURCES = @"
CREATE TABLE Sources (
  Id INTEGER NOT NULL,
  Text TEXT NOT NULL,
  PRIMARY KEY('Id') 
);";

        private const string CREATE_TABLE_QUOTES = @"
CREATE TABLE Quotes (
  Id INTEGER NOT NULL,
  Character TEXT,
  Text TEXT NOT NULL,
  Source_Id INTEGER,
  PRIMARY KEY('Id') 
);";

        private const string CREATE_TABLE_WORDS = @"
CREATE TABLE Words (
  Id INTEGER NOT NULL,
  Kanji TEXT,
  Kana TEXT NOT NULL,
  Translation TEXT NOT NULL,
  ReadingMastery INTEGER NOT NULL DEFAULT '0',
  TranslationMastery INTEGER NOT NULL DEFAULT '0',
  KanjiMastery INTEGER NOT NULL DEFAULT '0',
  Stars INTEGER NOT NULL DEFAULT '0',
  LastStudied TEXT,
  Quote_ID INTEGER,
  Disabled INTEGER NOT NULL DEFAULT '0',
  PRIMARY KEY('Id')
);";




        private const string ALL_DATA_VIEW = @"
CREATE VIEW all_data
AS
SELECT
  s.Id AS Source_Id, s.Text AS Source_Text,
  q.Id AS Quote_Id, q.Character, q.Text AS Quote_Text,
  w.Id AS Word_Id, w.Kanji, w.Kana, w.Translation
FROM Sources s
  JOIN Quotes q ON q.Source_Id = s.Id
  JOIN Words w ON w.Quote_Id = q.id
;";
        
    }
}
