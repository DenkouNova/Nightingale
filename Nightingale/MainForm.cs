﻿using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using Nightingale.Forms;
using Nightingale.Parsers;
using System.Configuration;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace Nightingale
{
    public partial class MainForm : Form
    {
        private readonly FeatherLogger _logger;

        public MainForm()
        {
            InitializeComponent();
            _logger = GlobalObjects.Logger;
            _logger.OpenSection("MainForm");
        }

        private void btnCreateDictionary_Click(object sender, EventArgs e)
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            var message = _logger.Info("Enter a path in which the dictionary will be created.");
            var folderPath = Microsoft.VisualBasic.Interaction.InputBox(
                message, "Dictionary path",
                Path.GetDirectoryName(Application.ExecutablePath),
                this.Location.X + ((this.Size.Width - 370) / 2), this.Location.Y + ((this.Size.Height - 160) / 2));

            if (String.IsNullOrEmpty(folderPath))
            {
                MessageBox.Show(_logger.Info("Cancelled by user."));
                _logger.CloseSection(location);
                return;
            }

            _logger.Info("User chose folder '" + folderPath + "'.");
            if (!Directory.Exists(folderPath))
            {
                message = _logger.Info("Please enter the path of an existing folder.");
                MessageBox.Show(message, "Folder does not exist",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _logger.CloseSection(location);
                return;
            }

            message = _logger.Info("Enter a filename for the dictionary.");
            var filename = Microsoft.VisualBasic.Interaction.InputBox(
                message, "Dictionary filename",
                "Dictionary" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".sqlite",
                this.Location.X + ((this.Size.Width - 370) / 2), this.Location.Y + ((this.Size.Height - 160) / 2));

            if (String.IsNullOrEmpty(filename))
            {
                MessageBox.Show(_logger.Info("Cancelled by user."));
                _logger.CloseSection(location);
                return;
            }
            
            var databaseCreator = GlobalObjects.DatabaseCreator.CreateDatabase(folderPath, filename);

            message = "Database created.";
            MessageBox.Show(_logger.Info(message));
            _logger.CloseSection(location);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _logger.CloseSection("MainForm");
            _logger.FinishLogging();
        }

        private void cbUseDatabaseSqlite_CheckedChanged(object sender, EventArgs e)
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);
            var message = "Not implemented.";
            MessageBox.Show(_logger.Info(message));
            _logger.CloseSection(location);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            var databasePath = GetFilePathFromOpenFileDialog(
                message: "Choose an sqlite database/dictionary",
                filter: "SQLite database|*.sqlite");
            if (String.IsNullOrEmpty(databasePath))
            {
                MessageBox.Show(_logger.Info("Cancelled by user."));
                _logger.CloseSection(location);
                return;
            }
            
            var importedFilePath = GetFilePathFromOpenFileDialog(
                message: "Choose a file to import",
                filter: "Text files|*.txt");
            if (String.IsNullOrEmpty(importedFilePath))
            {
                MessageBox.Show(_logger.Info("Cancelled by user."));
                _logger.CloseSection(location);
                return;
            }

            _logger.Info("Will copy database first");

            var dbCopier = GlobalObjects.DatabaseCopier;
            var copyDatabasePath = dbCopier.CopyDatabaseForImport(databasePath);

            if (String.IsNullOrEmpty(copyDatabasePath))
            {
                // TODO UNTESTED
                _logger.Error("Database copy failed.");
            }
            else
            {
                AbstractParser parser;
                using (StreamReader sReader = new StreamReader(importedFilePath))
                {
                    string wordStyleString = ":WordStyle=";
                    var firstLine = sReader.ReadLine();
                    if (firstLine.Substring(0, 11) != wordStyleString)
                    {
                        var errorMessage = "First line of file must start with ':WordStyle='.";
                        throw new Exception(_logger.Error(errorMessage));
                    }
                    var wordStyle = firstLine.Substring(wordStyleString.Length);
                    if (wordStyle == "Takoboto")
                    {
                        parser = new TakobotoParser();
                    }
                    else if (wordStyle == "JWPCE")
                    {
                        parser = new JwpceParser();
                    }
                    else
                    {
                        var errorMessage = "Word style '" + wordStyle + "' is invalid.";
                        throw new Exception(_logger.Error(errorMessage));
                    }
                }

                var importSuccess = parser.ImportFile(copyDatabasePath, importedFilePath);
                if (importSuccess)
                {
                    dbCopier.RestoreFile();
                    MessageBox.Show(_logger.Info("Success!"));
                }
                else
                {
                    var errorMessage = "Parsing failed. Some error happened, apparently";
                    MessageBox.Show(_logger.Error(errorMessage));
                }
            }

            _logger.CloseSection(location);
        }

        // "SQLite database|*.sqlite"
        private string GetFilePathFromOpenFileDialog(string message, string filter)
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _logger.Info(message);
            var openFileDialog = new OpenFileDialog()
            {
                Title = message,
                Filter = filter
            };

            string returnValue = null;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show(_logger.Info("Cancelled by user."));
            }
            else
            {
                returnValue = openFileDialog.FileName;
                _logger.Info("User chose '" + returnValue + "'");
            }

            _logger.CloseSectionWithReturnInfo(returnValue, location);
            return returnValue;
        }

        private void btnStudy_Click(object sender, EventArgs e)
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            var databasePath = GetFilePathFromOpenFileDialog(
                message: "Choose an sqlite database/dictionary",
                filter: "SQLite database|*.sqlite");
            if (String.IsNullOrEmpty(databasePath))
            {
                MessageBox.Show(_logger.Info("Cancelled by user."));
                _logger.CloseSection(location);
                return;
            }

            var form = new JapaneseStudyForm(databasePath);

            _logger.CloseSection(location);
            form.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
