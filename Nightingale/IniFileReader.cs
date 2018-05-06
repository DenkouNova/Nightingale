using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Nightingale
{
    public class IniFileReader
    {
        private readonly string _fileFullPath;

        public IniFileReader(string fileFullPath)
        {
            if (!File.Exists(fileFullPath))
            {
                var errorMessage = "File '" + fileFullPath + "' not found. Should be next to the exe file.";
                throw new FileNotFoundException(errorMessage);
            }
            _fileFullPath = fileFullPath;
        }

        // TODO UNTESTED
        public Dictionary<string, string> ReadIniFile()
        {
            var returnDictionary = new Dictionary<string, string>();

            try
            {
                string allText = File.ReadAllText(_fileFullPath);

                string[] lines = allText.Split(
                    new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.None
                );

                foreach (var oneLine in lines)
                {
                    if (oneLine.Length > 0 && oneLine.IndexOf("#") == -1 && oneLine.IndexOf("[") == -1)
                    {
                        var key = oneLine.Substring(0, oneLine.IndexOf("="));
                        var value = oneLine.Substring(oneLine.IndexOf("=") + 1);

                        if (returnDictionary.ContainsKey(key))
                        {
                            throw new Exception("Error: key '" + key + "' found twice in the file.");
                        }
                        returnDictionary.Add(key, value);
                    }
                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = "Exception message: '" + ex.Message + "'" +
                    ex.InnerException == null ? "" :
                    "Inner exception message: '" + ex.InnerException.Message + "'";
            }

            return returnDictionary;
        }
    }
}
