using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nightingale
{
    public class GlobalObjects
    {
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
                    FeatherLoggerLogMode.LogAsYouGo,
                    FeatherLoggerTraceLevel.Info,
                    folderName: null,
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
