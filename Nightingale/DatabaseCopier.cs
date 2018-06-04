using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nightingale
{
    public class DatabaseCopier
    {
        public static string GetCopyFileName(string databasePath)
        {
            return databasePath + ".importing";
        }
        
        private readonly FeatherLogger _logger;

        public string OriginalDatabasePath { get; private set; }
        public string CopyDatabasePath { get; private set; }

        public DatabaseCopier(FeatherLogger logger)
        {
            _logger = logger;
        }

        public string CopyDatabaseForImport(string databasePath)
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            OriginalDatabasePath = databasePath;

            string copyDatabasePath = null;
            
            if (!File.Exists(databasePath))
            {
                // TODO UNTESTED
                _logger.Error("File '" + databasePath + "' does not exist!");
                _logger.CloseSectionWithReturnInfo(copyDatabasePath, location);
                return copyDatabasePath;
            }  

            try
            {
                copyDatabasePath = GetCopyFileName(databasePath);

                if (File.Exists(copyDatabasePath))
                {
                    // TODO UNTESTED
                    _logger.Info("Copy file '" + copyDatabasePath + "' already exists. Deleting...");
                    File.Delete(copyDatabasePath);
                    _logger.Info("Deleted.");
                }

                _logger.Info("Copying '" + databasePath + "' to '" + copyDatabasePath + "'...");
                File.Copy(databasePath, copyDatabasePath);
                _logger.Info("Complete.");
            }
            catch (Exception ex)
            {
                // TODO UNTESTED (error during delete)
                // TODO UNTESTED (error during backup)
                _logger.Error(ex);
                copyDatabasePath = null;
            }

            CopyDatabasePath = copyDatabasePath;

            _logger.CloseSectionWithReturnInfo(copyDatabasePath, location);
            return copyDatabasePath;
        }

        public bool RestoreFile()
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _logger.Info("Will restore file '" + CopyDatabasePath + "' into file '" + OriginalDatabasePath);

            bool success = false;

            try
            {
                File.Copy(CopyDatabasePath, OriginalDatabasePath, overwrite: true);
                _logger.Info("Restored.");
                success = true;
            }
            catch (Exception ex)
            {
                // TODO UNTESTED (error during restore)
                success = false;
                _logger.Error(ex);
            }

            try
            {
                _logger.Info("Deleting copy file...");
                File.Delete(CopyDatabasePath);
                _logger.Info("Deleted.");
            }
            catch (Exception ex)
            {
                // TODO UNTESTED (error during delete)
                // Not being able to delete the file does not mean the restore process is not successful.
                _logger.Warn(ex);
                _logger.Warn("Could not delete file. Oh well");
            }

            _logger.CloseSectionWithReturnInfo(success.ToString(), location);
            return success;
        }
    }
}
