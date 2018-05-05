using System;
using System.IO;
using System.Linq;
using System.Text;
using Nightingale;
using NUnit.Framework;
using System.Collections.Generic;

namespace NightingaleUnitTests
{

    [TestFixture]
    [Category("Database")]
    public class GivenADatabaseCreator
    {
        private const string TEST_DATABASE_FILE_NAME = "database.unittest.sqlite";

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

            dbCreator.CreateDatabase(_testFolder, TEST_DATABASE_FILE_NAME);

            Assert.IsTrue(File.Exists(_testFolder + @"\" + TEST_DATABASE_FILE_NAME));
        }
    }
}
