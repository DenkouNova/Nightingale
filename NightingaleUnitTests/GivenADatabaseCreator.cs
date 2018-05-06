using System;
using System.IO;
using System.Linq;
using System.Text;
using Nightingale;
using NUnit.Framework;
using System.Threading;
using System.Data.SQLite;
using System.Collections.Generic;

namespace NightingaleUnitTests
{

    [TestFixture]
    [Category("Database")]
    public class GivenADatabaseCreator
    {
        // This is a pattern I haven't really explored, but.
        // This is a global variable indicating whether or not the test being run is the
        // prerequisite for another test.
        // If the test fails, it should give the default error message, but if a prerequisite
        // test fails, the error message should be to fix that prerequisite test first.
        private string _runningPrerequisiteErrorMessage;

        private string _databasePath;

        private string _testFolder;

        [SetUp]
        public void Setup()
        {
            _testFolder =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            UnitTestHelpers.DeleteTestFiles(_testFolder);
        }

        [Test]
        public void GivenADatabaseCreator_WhenICreateADatabase_ThenTheDatabaseIsCreated()
        {
            var dbCreator = GlobalObjects.DatabaseCreator;

            var dbFilename = "database" + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + UnitTestHelpers.TEST_DATABASE_EXTENSION;
            dbCreator.CreateDatabase(_testFolder, dbFilename);

            _databasePath = _testFolder + @"\" + dbFilename;

            Assert.IsTrue(File.Exists(_databasePath), 
                _runningPrerequisiteErrorMessage ?? "Database should be created");
        }

        [Test]
        public void GivenADatabaseCreator_WhenICreateADatabase_ThenTheDatabaseIsPopulated()
        {
            TestPrerequisite(GivenADatabaseCreator_WhenICreateADatabase_ThenTheDatabaseIsCreated);

            using (var connection = new SQLiteConnection("Data Source=" + _databasePath + ";Version=3;"))
            {
                connection.Open();
                string[] tablesToVerify = { "Category", "SubCategory", "Source", "SubSource", "Link" };

                foreach(string oneTable in tablesToVerify)
                {
                    string query = "SELECT 1 FROM " + oneTable;
                    using (var command = new System.Data.SQLite.SQLiteCommand(query, connection))
                    {
                        // the real test here is Assert.DoesNotThrow()
                        command.ExecuteReader();
                    }
                }
                connection.Close();
            }
        }



        private delegate void PrerequisiteTest();

        private void TestPrerequisite(PrerequisiteTest test)
        {
            _runningPrerequisiteErrorMessage = "Prerequisite '" + test.Method.Name + "' not met";
            test.Invoke();
            _runningPrerequisiteErrorMessage = null;
        }
    }
}
