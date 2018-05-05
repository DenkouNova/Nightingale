using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace NightingaleUnitTests
{
    public class UnitTestHelpers
    {
        public static string TEST_LOGGER_EXTENSION = "unittest.txt";
        public static string TEST_DATABASE_FILE_NAME = "database.unittest.sqlite";

        public static void DeleteTestFiles(string folderPath)
        {
            string[] filters = {TEST_LOGGER_EXTENSION, TEST_DATABASE_FILE_NAME };

            foreach(var oneFilter in filters)
            {
                foreach (var oneFile in Directory.GetFiles(folderPath, "*." + oneFilter))
                {
                    File.Delete(oneFile);
                }
            }

            foreach (var oneDir in Directory.GetDirectories(folderPath))
            {
                Directory.Delete(oneDir, recursive: true);
            }
        }
    }
}
