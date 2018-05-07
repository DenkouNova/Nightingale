using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Nightingale.Extensions;

namespace Nightingale
{
    public enum FeatherLoggerTraceLevel
    {
        Extreme = 10,
        Info = 9,
        Sql = 6,
        Warn = 3,
        Error = 1,
        Nothing = -9999
    }

    public enum FeatherLoggerLogMode
    {
        LogAsYouGo,
        LogDump
    }

    public class FeatherLogger
    {
        private const string TAB_STRING = "  "; // 2-space tab

        private StringBuilder _allText;
        private int _currentTabLevel;

        public FeatherLoggerLogMode LogMode { get; set; }
        public FeatherLoggerTraceLevel TraceLevel { get; set; }

        public string FolderName { get; set; }
        public string FileName { get; set; }

        public string FullPath { get { return FolderName + @"\" + FileName; } }

        public string ErrorMessage { get; private set; } // Contains text when the logger was created in some wrong state

        public FeatherLogger(FeatherLoggerLogMode logMode, FeatherLoggerTraceLevel traceLevel, string folderName,
            string filename, bool hasTimestampInFilename, string extension)
        {
            ErrorMessage = String.Empty;
            TraceLevel = FeatherLoggerTraceLevel.Nothing;

            try
            {
                LogMode = logMode;
                TraceLevel = traceLevel;

                var blah = System.AppDomain.CurrentDomain.FriendlyName;
                var blah2 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                var blah3 = System.Reflection.Assembly.GetCallingAssembly();
                var blah4 = System.Reflection.Assembly.GetExecutingAssembly();

                FolderName = !String.IsNullOrEmpty(folderName) ? folderName :
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                

                FileName = filename + (hasTimestampInFilename ? DateTime.Now.ToString("yyyyMMddhhmmss") : "") +
                    (String.IsNullOrEmpty(extension) ? "" : "." + extension);

                ResetTabLevel();

                if (LogMode == FeatherLoggerLogMode.LogAsYouGo)
                {
                    CreateFolderForLogFileIfDoesntExist();
                }
                else if (LogMode == FeatherLoggerLogMode.LogDump)
                {
                    _allText = new StringBuilder();
                }
                else
                {
                    throw new NotImplementedException("FeatherLogger.ctor does not support LogMode " + LogMode);
                }

                WriteFirstLine();
            }
            catch (Exception ex)
            {
                this.ErrorMessage = "EXCEPTION: " + ex.Message +
                    ((ex.InnerException == null) ? "" : " / INNER EXCEPTION: " + ex.InnerException.Message);
                throw ex;
            }

        }

        private void WriteLastLine(bool error = false)
        {
            if (TraceLevel > FeatherLoggerTraceLevel.Nothing)
            {
                ResetTabLevel();
                if (error)
                {
                    WriteOneLine("<!------------------------------>");
                    WriteOneLine("<!-- Execution ended in ERROR -->");
                    WriteOneLine("<!------------------------------>");
                }
                else
                {
                    WriteOneLine("<!----------------------------------->");
                    WriteOneLine("<!-- Execution ended successfully. -->");
                    WriteOneLine("<!----------------------------------->");
                }
            }
        }

        public string Error(Exception ex)
        {
            string ExceptionMessage = "Exception: '" + ex.Message + "'" +
                ex.InnerException != null ? " Inner exception: '" + ex.InnerException.Message + "'" : "";
            if (this.TraceLevel >= FeatherLoggerTraceLevel.Error) WriteOneLine("ERROR: " + ExceptionMessage);
            return ExceptionMessage;
        }

        public string Error(string s)
        {
            if (this.TraceLevel >= FeatherLoggerTraceLevel.Error) WriteOneLine("ERROR: " + (s ?? "(null)"));
            return s;
        }

        public string Warn(string s)
        {
            if (this.TraceLevel >= FeatherLoggerTraceLevel.Warn) WriteOneLine("WARN : " + (s ?? "(null)"));
            return s;
        }

        public string Sql(string s)
        {
            if (this.TraceLevel >= FeatherLoggerTraceLevel.Sql) WriteOneLine("S Q L: " + (s ?? "(null)"));
            return s;
        }

        public string Info(string s)
        {
            if (this.TraceLevel >= FeatherLoggerTraceLevel.Info) WriteOneLine("INFO : " + (s ?? "(null)"));
            return s;
        }

        public string Extreme(string s)
        {
            if (this.TraceLevel >= FeatherLoggerTraceLevel.Extreme) WriteOneLine("EXTRM: " + (s ?? "(null)"));
            return s;
        }

        // TODO UNTESTED
        public string CloseSectionWithReturnInfo(string s, string location)
        {
            var returnString = WrapWithReturn(s);
            Info(returnString);
            CloseSection(location);
            return s;
        }

        // TODO UNTESTED
        public void OpenSection(string s)
        {
            WriteOneLine("<" + s + "> " + GenerateTimestamp());
            _currentTabLevel ++;
        }

        // TODO UNTESTED
        public void CloseSection(string s)
        {
            _currentTabLevel --;
            WriteOneLine("</" + s + "> " + GenerateTimestamp());
        }

        private void WriteOneLine(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _currentTabLevel; i++) sb.Append(TAB_STRING);
            s = s ?? "(null)";
            sb.Append(s);

            var loggedString = sb.ToString().ReplaceWhitespaceSpecialCharacters();

            if (LogMode == FeatherLoggerLogMode.LogAsYouGo)
            {
                using (StreamWriter sw = new StreamWriter(FolderName + @"\" + FileName, true, Encoding.UTF8))
                {
                    sw.WriteLine(loggedString);
                }
            }
            else if (LogMode == FeatherLoggerLogMode.LogDump)
            {
                _allText.AppendLine(loggedString);
            }
            else
            { 
                throw new NotImplementedException("FeatherLogger.WriteOneLine does not support LogMode " + LogMode);
            } 
        }

        public void FinishLogging(bool error = false)
        {
            WriteLastLine();

            if (LogMode == FeatherLoggerLogMode.LogDump)
            {
                CreateFolderForLogFileIfDoesntExist();
                File.WriteAllText(this.FullPath, _allText.ToString(), Encoding.UTF8);
            }
        }



        private string WrapWithReturn(string s)
        {
            return "Returns: '" + s + "'";
        }

        private string GenerateTimestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        private void CreateFolderForLogFileIfDoesntExist()
        {
            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }
        }

        private void ResetTabLevel()
        {
            _currentTabLevel = 0;
        }

        private void WriteFirstLine()
        {
            if (TraceLevel > FeatherLoggerTraceLevel.Nothing)
            {
                WriteOneLine("<!-- Logger starting -->");
            }
        }



    }
}
