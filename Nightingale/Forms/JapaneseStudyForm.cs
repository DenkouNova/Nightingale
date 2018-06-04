using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Data.SQLite;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Nightingale.Forms
{
    public partial class JapaneseStudyForm : Form
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lp1, string lp2);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);



        private FeatherLogger _logger;

        private SQLiteConnection _dbConnection;

        private string _location;

        public JapaneseStudyForm(string databasePath)
        {
            _logger = GlobalObjects.Logger;

            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            _logger.Info("Creating new DB connection");
            _dbConnection = new SQLiteConnection("Data Source = " + databasePath);
            _logger.Info("Connection created");

            InitializeComponent();
        }

        private void JapaneseStudyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _logger.CloseSection(_location);
        }




        private void btnPaint_Click(object sender, EventArgs e)
        {
            bool changedPaintBrush = false;
            int maxNumberOfTimes = 10;
            int currentNumberOfTimes = 0;

            // TODO: this is only in kill-new mode
            /*
            foreach (Process proc in Process.GetProcessesByName("mspaint"))
            {
                proc.Kill();
            }
            System.Diagnostics.Process.Start("mspaint.exe");
            */

            // new window in existing mspaint
            if (Process.GetProcessesByName("mspaint").Count() == 0)
            {
                System.Diagnostics.Process.Start("mspaint.exe");
            }

            Thread.Sleep(300);

            while (!changedPaintBrush && currentNumberOfTimes < maxNumberOfTimes)
            {
                // TODO: add time in ini
                Thread.Sleep(200);
                // TODO: add in ini English or French
                // IntPtr handle = FindWindow("mspaintapp", "Untitled - Paint");
                IntPtr handle = FindWindow("mspaintapp", "Sans titre - Paint");
                // TODO: in ini, choose mode
                // either Kill-create new mspaint instance, or
                // use new window on existing instance
                changedPaintBrush = NewPaintBrush(handle); // new window
                //changedPaintBrush = ChangePaintBrush(handle); // kill-create

                currentNumberOfTimes++;
            }

        }

        private bool ChangePaintBrush(IntPtr handle)
        {
            if (SetForegroundWindow(handle))
            {
                SendKeys.SendWait("%");
                SendKeys.SendWait("h");
                SendKeys.SendWait("b");
                SendKeys.SendWait("{DOWN}");
                SendKeys.SendWait("{ENTER}");

                if (cbSmallBrush.Checked)
                {
                    SendKeys.SendWait("%");
                    SendKeys.SendWait("h");
                    SendKeys.SendWait("s");
                    SendKeys.SendWait("z");
                    SendKeys.SendWait("{DOWN}");
                    SendKeys.SendWait("{ENTER}");
                }

                // alt = %
                // ctrl = ^
                // shift = +
                //"Alt,H,B,↓,Enter"

                return true;
            }
            return false;
        }

        private bool NewPaintBrush(IntPtr handle)
        {
            if (SetForegroundWindow(handle))
            {
                SendKeys.SendWait("^n");
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait("{ENTER}");
                return true;
            }
            return false;
        }




    }
}
