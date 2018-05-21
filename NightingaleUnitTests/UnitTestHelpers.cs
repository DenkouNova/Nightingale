using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NightingaleUnitTests
{
    public class UnitTestHelpers
    {
        public static string TEST_DATABASE_EXTENSION = "unittest.sqlite";
        public static string TEST_DATABASE_EXTENSION_IMPORTING = "unittest.sqlite.importing";

        public static string TEST_LOGGER_EXTENSION = "unittest.xml";

        public static string LOGGER_FILTER_TXT = "Nightingale*.txt";
        public static string LOGGER_FILTER_XML = "Nightingale*.xml";

        public static void DeleteTestFiles(string folderPath)
        {
            // Attempt to delete SQLite databases
            var databaseExtensions = new string[] { TEST_DATABASE_EXTENSION, TEST_DATABASE_EXTENSION_IMPORTING };
            foreach (var oneExtension in databaseExtensions)
            {
                foreach (var oneFile in Directory.GetFiles(folderPath, "*." + oneExtension))
                {
                    try
                    {
                        File.Delete(oneFile);
                    }
                    catch (IOException)
                    {
                        // Probably still in use, just do nothing.
                        // These pesky SQLite databases will be deletable eventually
                    }
                }
            }

            // Delete logger files, part 1
            // Files created in Logger tests
            foreach (var oneFile in Directory.GetFiles(folderPath, "*." + TEST_LOGGER_EXTENSION))
            {
                File.Delete(oneFile);
            }

            // Delete logger files, part 2
            // Files created in normal usage
            string[] fileFiltersToDelete = { LOGGER_FILTER_TXT, LOGGER_FILTER_XML };
            foreach (var oneFilter in fileFiltersToDelete)
            {
                foreach (var oneFile in Directory.GetFiles(folderPath, oneFilter))
                {
                    File.Delete(oneFile);
                }
            }

            // Delete directories
            foreach (var oneDir in Directory.GetDirectories(folderPath))
            {
                Directory.Delete(oneDir, recursive: true);
            }
        }
    }
}
