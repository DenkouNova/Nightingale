using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
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

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You did it!");
        }

        private void btnCreateStudyDictionary_Click(object sender, EventArgs e)
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
                return;
            }

            _logger.Info("User chose folder '" + folderPath + "'.");
            if (!Directory.Exists(folderPath))
            {
                message = _logger.Info("Please enter the path of an existing folder.");
                MessageBox.Show(message, "Folder does not exist",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            _logger.CloseSection("location");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _logger.CloseSection("MainForm");
            _logger.FinishLogging();
        }
    }
}
