using System;
using System.IO;
using Nightingale;
using NUnit.Framework;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace NightingaleUnitTests
{
    [TestFixture]
    [Category("FeatherLogger")]
    public class GivenAFeatherLogger
    {
        private string _testFolder;
        private string _loggerFileExtension;

        private const string TEST_ERROR_STRING = "652752256 error";
        private const string TEST_WARN_STRING = "1652478764 warn";
        private const string TEST_SQL_STRING = "435464354 sql";
        private const string TEST_INFO_STRING = "9876546 info";
        private const string TEST_EXTREME_STRING = "32423643 extreme";

        [SetUp]
        public void Setup()
        {
            _testFolder =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            _loggerFileExtension = UnitTestHelpers.TEST_LOGGER_EXTENSION;

            UnitTestHelpers.DeleteTestFiles(_testFolder);
        }

        [Test]
        public void GivenAFeatherLogger_IfAndOnlyIfCreatingALoggerWithLogAsYouGo_ThenAFolderAndAFileIsCreated(
            [Values(FeatherLoggerLogMode.LogAsYouGo, FeatherLoggerLogMode.LogDump)] FeatherLoggerLogMode logMode)
        {
            var testName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            var logger = new FeatherLogger (
                logMode: logMode,
                traceLevel: FeatherLoggerTraceLevel.Error,
                folderName: _testFolder + @"\LogMode" + "_" + logMode,
                filename: "log",
                hasTimestampInFilename: true,
                extension: _loggerFileExtension);

            var directoryExists = Directory.Exists(logger.FolderName);
            var fileExists = File.Exists(logger.FullPath);

            if (logMode == FeatherLoggerLogMode.LogAsYouGo)
            {
                Assert.IsTrue(directoryExists);
                Assert.IsTrue(fileExists);
            }
            else
            {
                Assert.IsFalse(directoryExists);
                Assert.IsFalse(fileExists);
            }
        }

        [Test]
        public void GivenAFeatherLogger_IfFolderNameIsNotIncluded_ThenFileIsPutInsideExeFolder()
        {
            var logger = new FeatherLogger(
                logMode: FeatherLoggerLogMode.LogAsYouGo,
                traceLevel: FeatherLoggerTraceLevel.Error,
                folderName: null, // defaults to the value of _testFolder
                filename: "log",
                hasTimestampInFilename: false,
                extension: _loggerFileExtension);

            var directoryExists = Directory.Exists(logger.FolderName);
            var fileExists = File.Exists(logger.FullPath);

            Assert.IsTrue(File.Exists(_testFolder + @"\log." + _loggerFileExtension));
        }

        [Test]
        public void GivenAFeatherLogger_IfTimestampIsRequested_ThenFilenameContainsYYYYMMDDHHMMSSTimestamp()
        {
            var logger = new FeatherLogger(
                logMode: FeatherLoggerLogMode.LogAsYouGo,
                traceLevel: FeatherLoggerTraceLevel.Error,
                folderName: null,
                filename: "log",
                hasTimestampInFilename: true,
                extension: _loggerFileExtension);

            Assert.IsTrue(Regex.IsMatch(logger.FileName, @"log\d{14}\.unittest\.xml$"));
        }

        [Test]
        public void GivenAFeatherLogger_IfTraceLevelIsNothing_ThenNothingIsLogged()
        {
            var logger = GetFeatherLoggerForTestingLogLevels(FeatherLoggerTraceLevel.Nothing);
            AttemptToLogAllTypes(logger);
            Assert.IsFalse(File.Exists(_testFolder + @"\log." + _loggerFileExtension));
        }

        [Test]
        public void GivenAFeatherLogger_IfTraceLevelIsError_ThenOnlyErrorIsLogged()
        {
            var logger = GetFeatherLoggerForTestingLogLevels(FeatherLoggerTraceLevel.Error);
            AttemptToLogAllTypes(logger);

            Assert.IsTrue(File.Exists(_testFolder + @"\log." + _loggerFileExtension), "File should exist");
            var loggedText = File.ReadAllText(_testFolder + @"\log." + _loggerFileExtension);

            Assert.IsTrue(loggedText.Contains(TEST_ERROR_STRING), "Error should be logged");
            Assert.IsFalse(loggedText.Contains(TEST_WARN_STRING), "Warn should not be logged");
            Assert.IsFalse(loggedText.Contains(TEST_SQL_STRING), "Sql should not be logged");
            Assert.IsFalse(loggedText.Contains(TEST_INFO_STRING), "Info should not be logged");
            Assert.IsFalse(loggedText.Contains(TEST_EXTREME_STRING), "Extreme should not be logged");
        }

        [Test]
        public void GivenAFeatherLogger_IfAttemptingToLogNullString_ThenStringNullWithinParenthesesIsLogged()
        {
            var logger = GetFeatherLoggerForTestingLogLevels(FeatherLoggerTraceLevel.Extreme);
            logger.Error((string)null);
            logger.Warn((string)null);
            logger.Sql((string)null);
            logger.Info((string)null);
            logger.Extreme((string)null);

            Assert.IsTrue(File.Exists(_testFolder + @"\log." + _loggerFileExtension), "File should exist");
            var loggedText = File.ReadAllText(_testFolder + @"\log." + _loggerFileExtension);

            string[] logLevels = { "ERROR:", "WARN :", "S Q L:", "INFO :", "EXTRM:" };
            foreach(string oneLevel in logLevels)
            {
                var stringToFind = oneLevel + " (null)";
                Assert.IsTrue(loggedText.Contains(stringToFind), 
                    "Log file should contain the string '" + stringToFind + "'");
            }
            
        }

        [Test]
        public void GivenAFeatherLogger_IfTraceLevelIsWarn_ThenOnlyErrorAndWarnAreLogged()
        {
            var logger = GetFeatherLoggerForTestingLogLevels(FeatherLoggerTraceLevel.Warn);
            AttemptToLogAllTypes(logger);

            Assert.IsTrue(File.Exists(_testFolder + @"\log." + _loggerFileExtension), "File should exist");
            var loggedText = File.ReadAllText(_testFolder + @"\log." + _loggerFileExtension);

            Assert.IsTrue(loggedText.Contains(TEST_ERROR_STRING), "Error should be logged");
            Assert.IsTrue(loggedText.Contains(TEST_WARN_STRING), "Warn should be logged");
            Assert.IsFalse(loggedText.Contains(TEST_SQL_STRING), "Sql should not be logged");
            Assert.IsFalse(loggedText.Contains(TEST_INFO_STRING), "Info should not be logged");
            Assert.IsFalse(loggedText.Contains(TEST_EXTREME_STRING), "Extreme should not be logged");
        }

        [Test]
        public void GivenAFeatherLogger_IfTraceLevelIsSql_ThenOnlyErrorAndWarnAndSqlAreLogged()
        {
            var logger = GetFeatherLoggerForTestingLogLevels(FeatherLoggerTraceLevel.Sql);
            AttemptToLogAllTypes(logger);

            Assert.IsTrue(File.Exists(_testFolder + @"\log." + _loggerFileExtension), "File should exist");
            var loggedText = File.ReadAllText(_testFolder + @"\log." + _loggerFileExtension);

            Assert.IsTrue(loggedText.Contains(TEST_ERROR_STRING), "Error should be logged");
            Assert.IsTrue(loggedText.Contains(TEST_WARN_STRING), "Warn should be logged");
            Assert.IsTrue(loggedText.Contains(TEST_SQL_STRING), "Sql should be logged");
            Assert.IsFalse(loggedText.Contains(TEST_INFO_STRING), "Info should not be logged");
            Assert.IsFalse(loggedText.Contains(TEST_EXTREME_STRING), "Extreme should not be logged");
        }

        [Test]
        public void GivenAFeatherLogger_IfTraceLevelIsInfo_ThenOnlyErrorAndWarnAndSqlAndInfoAreLogged()
        {
            var logger = GetFeatherLoggerForTestingLogLevels(FeatherLoggerTraceLevel.Info);
            AttemptToLogAllTypes(logger);

            Assert.IsTrue(File.Exists(_testFolder + @"\log." + _loggerFileExtension), "File should exist");
            var loggedText = File.ReadAllText(_testFolder + @"\log." + _loggerFileExtension);

            Assert.IsTrue(loggedText.Contains(TEST_ERROR_STRING), "Error should be logged");
            Assert.IsTrue(loggedText.Contains(TEST_WARN_STRING), "Warn should be logged");
            Assert.IsTrue(loggedText.Contains(TEST_SQL_STRING), "Sql should be logged");
            Assert.IsTrue(loggedText.Contains(TEST_INFO_STRING), "Info should be logged");
            Assert.IsFalse(loggedText.Contains(TEST_EXTREME_STRING), "Extreme should not be logged");
        }

        [Test]
        public void GivenAFeatherLogger_IfTraceLevelIsExtreme_ThenEverythingLogged()
        {
            var logger = GetFeatherLoggerForTestingLogLevels(FeatherLoggerTraceLevel.Extreme);
            AttemptToLogAllTypes(logger);

            Assert.IsTrue(File.Exists(_testFolder + @"\log." + _loggerFileExtension), "File should exist");
            var loggedText = File.ReadAllText(_testFolder + @"\log." + _loggerFileExtension);

            Assert.IsTrue(loggedText.Contains(TEST_ERROR_STRING), "Error should be logged");
            Assert.IsTrue(loggedText.Contains(TEST_WARN_STRING), "Warn should be logged");
            Assert.IsTrue(loggedText.Contains(TEST_SQL_STRING), "Sql should be logged");
            Assert.IsTrue(loggedText.Contains(TEST_INFO_STRING), "Info should be logged");
            Assert.IsTrue(loggedText.Contains(TEST_EXTREME_STRING), "Extreme should be logged");
        }

        [Test]
        public void GivenAFeatherLogger_IfLogModeIsLogDump_ThenFileIsCreatedOnlyAfterDump()
        {
            var logger = new FeatherLogger(
                logMode: FeatherLoggerLogMode.LogDump,
                traceLevel: FeatherLoggerTraceLevel.Error,
                folderName: null,
                filename: "log",
                hasTimestampInFilename: false,
                extension: _loggerFileExtension);

            logger.Error("123456");

            Assert.IsFalse(File.Exists(_testFolder + @"\log." + _loggerFileExtension), "File shouldn't exist before dump");
            logger.FinishLogging();

            Assert.IsTrue(File.Exists(_testFolder + @"\log." + _loggerFileExtension), "File should exist after dump");
            var loggedText = File.ReadAllText(_testFolder + @"\log." + _loggerFileExtension);
            Assert.IsTrue(loggedText.Contains("123456"));
        }





        private FeatherLogger GetFeatherLoggerForTestingLogLevels(FeatherLoggerTraceLevel traceLevel)
        {
            var logger = new FeatherLogger(
                logMode: FeatherLoggerLogMode.LogAsYouGo,
                traceLevel: traceLevel,
                folderName: null,
                filename: "log",
                hasTimestampInFilename: false,
                extension: _loggerFileExtension);

            return logger;
        }

        private void AttemptToLogAllTypes(FeatherLogger logger)
        {
            logger.Error(TEST_ERROR_STRING);
            logger.Warn(TEST_WARN_STRING);
            logger.Sql(TEST_SQL_STRING);
            logger.Info(TEST_INFO_STRING);
            logger.Extreme(TEST_EXTREME_STRING);
        }

    }
}
