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
                    CREATE_TABLE_CATEGORY,
                    CREATE_TABLE_SUBCATEGORY,
                    CREATE_TABLE_SOURCE,
                    CREATE_TABLE_SUBSOURCE,
                    CREATE_TABLE_LINK };

                foreach (var createOneTable in createTableStatements)
                {
                    CreateTable(createOneTable, connection);
                }
                connection.Close();
            }

            _logger.CloseSection(location);
        }

        private void CreateTable(string query, SQLiteConnection connection)
        {
            _logger.Info("Creating table with the following query:");
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
                _logger.Info("Table created.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }
            
        }

        private const string CREATE_TABLE_CATEGORY = @"
CREATE TABLE Category (
  Id INTEGER,
  Name TEXT,
  Disabled INTEGER  
);";

        private const string CREATE_TABLE_SUBCATEGORY = @"
CREATE TABLE SubCategory (
  Id INTEGER,
  Name TEXT,
  Disabled INTEGER,
  Category_Id INTEGER
);";

        private const string CREATE_TABLE_SOURCE = @"
CREATE TABLE Source (
  Id INTEGER,
  Name TEXT,
  Disabled INTEGER,
  SubCategory_Id INTEGER
);";

        private const string CREATE_TABLE_SUBSOURCE = @"
CREATE TABLE SubSource (
  Id INTEGER,
  Name TEXT,
  Disabled INTEGER,
  Source_Id INTEGER
);";

        private const string CREATE_TABLE_LINK = @"
CREATE TABLE Link (
  Id INTEGER,
  Name TEXT,
  Disabled INTEGER,
  SubSource_Id INTEGER,
  MasteryAToB INTEGER,
  MasteryBToA INTEGER,
  LastStudiedDate TEXT,
  Discriminant TEXT,
  DatumA_Varchar TEXT,
  DatumB_Varchar TEXT
);";
        
    }
}
