using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Nightingale
{
    static class Program
    {
        private const string INI_FILE = "Nightingale.ini";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var localFolder = 
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var fullPath = localFolder + @"\" + INI_FILE;
            var iniReader = new IniFileReader(fullPath);
            var keyValuePairs = iniReader.ReadIniFile();
            InitializeValuesFromIni(keyValuePairs);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        // TODO UNTESTED
        private static void InitializeValuesFromIni(Dictionary<string, string> keyValuePairs)
        {
            foreach(var oneKeyValuePair in keyValuePairs)
            {
                var key = oneKeyValuePair.Key;
                var value = oneKeyValuePair.Value;

                if (key == "GoodAnswerPoints")
                {
                    GlobalObjects.GoodAnswerPoints = Convert.ToInt32(value);
                }
                else if (key == "GoodAnswerPrct")
                {
                    GlobalObjects.GoodAnswerPrct = double.Parse(value, CultureInfo.InvariantCulture);
                }
                else if (key == "BadAnswerPoints")
                {
                    GlobalObjects.BadAnswerPoints = Convert.ToInt32(value);
                }
                else if (key == "BadAnswerPrct")
                {
                    GlobalObjects.BadAnswerPrct = double.Parse(value, CultureInfo.InvariantCulture);
                }
                else if (key == "GoodAnswerPoints")
                {
                    GlobalObjects.GoodAnswerPoints = Convert.ToInt32(value);
                }
                else if (key == "BadAnswerPoints")
                {
                    GlobalObjects.BadAnswerPoints = Convert.ToInt32(value);
                }
                else if (key == "GoodAnswerPrct")
                {
                    GlobalObjects.GoodAnswerPrct = double.Parse(value, CultureInfo.InvariantCulture);
                }
                else if (key == "BadAnswerPrct")
                {
                    GlobalObjects.BadAnswerPrct = double.Parse(value, CultureInfo.InvariantCulture);
                }
                else if (key == "TraceLevel")
                {
                    FeatherLoggerTraceLevel ParsedValue = (FeatherLoggerTraceLevel)
                        Enum.Parse(typeof(FeatherLoggerTraceLevel), value, true);
                    GlobalObjects.FeatherLoggerTraceLevel = ParsedValue;
                }
                else if (key == "LogMode")
                {
                    FeatherLoggerLogMode ParsedValue = (FeatherLoggerLogMode)
                        Enum.Parse(typeof(FeatherLoggerLogMode), value, true);
                    GlobalObjects.FeatherLoggerMode = ParsedValue;
                }
                else if (key == "FolderName")
                {
                    GlobalObjects.FolderName = value;
                }
                else if (key == "Language")
                {
                    WindowsLanguage ParsedValue = (WindowsLanguage)
                        Enum.Parse(typeof(WindowsLanguage), value, true);
                    GlobalObjects.Language = ParsedValue;
                }
                else if (key == "FreePointsOnNextLevel")
                {
                    GlobalObjects.FreePointsOnNextLevel = Convert.ToInt32(value);
                }
                else if (key == "LevelDownOnPoints")
                {
                    GlobalObjects.LevelDownOnPoints = Convert.ToInt32(value);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Invalid key '" + key + "' found in ini file");
                }


            }
        }

        
    }
}
