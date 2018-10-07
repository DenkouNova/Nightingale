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


        public JapaneseStudyForm(string databasePath)
        {
            InitializeComponent();
        }

        private void KillExistingPaintWindowAndStartANewOne()
        {
            bool changedPaintBrush = false;
            int maxNumberOfTimes = 10;
            int currentNumberOfTimes = 0;

            foreach (Process proc in Process.GetProcessesByName("mspaint"))
            {
                proc.Kill();
            }
            System.Diagnostics.Process.Start("mspaint.exe");

            Thread.Sleep(300);

            string windowName = GlobalObjects.Language == WindowsLanguage.FR ? "Sans titre - Paint" : "Untitled - Paint";
            while (!changedPaintBrush && currentNumberOfTimes < maxNumberOfTimes)
            {
                Thread.Sleep(200);
                
                IntPtr handle = FindWindow("mspaintapp", windowName);
                changedPaintBrush = ChangePaintBrush(handle);
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

        private void JapaneseStudyForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        private void btnPaint_Click(object sender, EventArgs e)
        {
            KillExistingPaintWindowAndStartANewOne();
        }

        


        private void btnMasteryUp_Click(object sender, EventArgs e)
        {

        }

        private void btnMasteryDown_Click(object sender, EventArgs e)
        {

        }

        private void UpdateEveryStepLabels()
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }
    }
}
