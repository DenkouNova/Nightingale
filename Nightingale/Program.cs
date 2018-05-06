using System;
using System.IO;
using System.Linq;
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
                if (key == "LogMode")
                {
                    GlobalObjects.FeatherLoggerMode = (FeatherLoggerLogMode)
                        Enum.Parse(typeof(FeatherLoggerLogMode), value);
                }
                else if (key == "TraceLevel")
                {
                    GlobalObjects.FeatherLoggerTraceLevel = (FeatherLoggerTraceLevel)
                        Enum.Parse(typeof(FeatherLoggerTraceLevel), value);
                }
                else if (key == "FolderName")
                {
                    GlobalObjects.FolderName = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Invalid key '" + key + "' found in ini file");
                }
            }
        }

        
    }
}
