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
    [Category("Files")]
    public class GivenADatabaseCopier : BaseTestWithPrerequisites
    {
        private string _databasePath;
        private string _copyDatabasePath;

        private string _testFolder;

        [SetUp]
        public void Setup()
        {
            _testFolder =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            UnitTestHelpers.DeleteTestFiles(_testFolder);
        }

        [Test]
        public void GivenADatabaseCopier_WhenICopyADatabase_ThenObjectPathsShouldBeUpdated()
        {
            SetupForDatabaseCreationTests();

            var dbCopier = GlobalObjects.DatabaseCopier;
            dbCopier.CopyDatabaseForImport(_databasePath);

            Assert.AreEqual(dbCopier.OriginalDatabasePath, _databasePath,
                "After copy, the DatabaseCopier object should know the original filename");
            Assert.AreEqual(dbCopier.CopyDatabasePath, _copyDatabasePath,
                "After copy, the DatabaseCopier object should know the copy filename");
        }

        [Test]
        public void GivenADatabaseCopier_WhenICopyADatabase_ThenItIsCopied()
        {
            SetupForDatabaseCreationTests();

            GlobalObjects.DatabaseCopier.CopyDatabaseForImport(_databasePath);

            Assert.IsTrue(File.Exists(_copyDatabasePath),
                _runningPrerequisiteErrorMessage ?? "File should have been copied from '" + _databasePath +
                "' to '" + _copyDatabasePath + "'");
        }

        [Test]
        public void GivenADatabaseCopier_WhenIRestoreAFile_ThenItIsRestoredWithNewTextFromTheCopyFile()
        {
            TestPrerequisite(GivenADatabaseCopier_WhenICopyADatabase_ThenItIsCopied);

            string finalText = "This text should be in the final file";
            File.WriteAllText(_copyDatabasePath, finalText);

            GlobalObjects.DatabaseCopier.RestoreFile();

            var allTextOriginalPath = File.ReadAllText(_databasePath);
            Assert.AreEqual(finalText, allTextOriginalPath, "Text should be the new text");
        }

        [Test]
        public void GivenADatabaseCopier_WhenIRestoreAFile_ThenTheCopyFileShouldDisappear()
        {
            TestPrerequisite(GivenADatabaseCopier_WhenICopyADatabase_ThenItIsCopied);

            GlobalObjects.DatabaseCopier.RestoreFile();

            Assert.IsFalse(File.Exists(_copyDatabasePath), "Copy file should be deleted");
        }




        private void SetupForDatabaseCreationTests()
        {
            var dbFilename = "database" + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + UnitTestHelpers.TEST_DATABASE_EXTENSION;
            _databasePath = _testFolder + @"\" + dbFilename;
            File.WriteAllText(_databasePath, "abc");

            _copyDatabasePath = DatabaseCopier.GetCopyFileName(_databasePath);
        }

        

    }
}
