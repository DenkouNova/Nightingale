using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nightingale
{
    public class GlobalObjects
    {
        public static FeatherLoggerLogMode FeatherLoggerMode { get; set; }
        public static FeatherLoggerTraceLevel FeatherLoggerTraceLevel { get; set; }
        public static string FolderName { get; set; }

        private static FeatherLogger _logger = null;
        public static FeatherLogger Logger
            { get { return _logger ?? (_logger = CreateFeatherLogger()); } }

        private static DatabaseCreator _databaseCreator = null;
        public static DatabaseCreator DatabaseCreator
        { get { return _databaseCreator ?? (_databaseCreator = CreateDatabaseCreator()); } }

        private static FeatherLogger CreateFeatherLogger()
        {
            try
            {
                var returnLogger = new FeatherLogger(
                    logMode: FeatherLoggerMode,
                    traceLevel: FeatherLoggerTraceLevel,
                    folderName: FolderName,
                    filename: "Nightingale",
                    hasTimestampInFilename: true,
                    extension: "xml");
                return returnLogger;
            }
            catch (Exception ex)
            {
                _logger.Error(ex); // not Logger! You wouldn't want recursion
                throw;
            }
        }

        private static DatabaseCreator CreateDatabaseCreator()
        {
            DatabaseCreator dbCreator = null;
            try
            {
                dbCreator = new DatabaseCreator(Logger);
            } catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return dbCreator;            
        }
    }
}
