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

        private int STARTING_NUMBER_OF_WORDS_LOADED = 200;
        private int LINK_ADD_ON_UP = 3;
        private int LINK_REMOVE_ON_DOWN = 1;

        private FeatherLogger _logger;

        //private bool _changesOccurred = false;

        private string _location;

        private int _numberOfTotalWords;
        private int _numberOfMasteredWords;
        private int _numberOfCurrentWords;

        private List<Domain.Word> _wordsToStudy;

        //private Domain.Link CurrentLink;
        //private Step CurrentStep;
        //private AorB AorBMode = AorB.A_Mode;

        private SQLiteConnection _dbConnection;
        private NHibernate.ISession _dbSession;

        public JapaneseStudyForm(string databasePath)
        {
            _logger = GlobalObjects.Logger;

            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            _logger.Info("Creating new DB connection");
            _dbConnection = new SQLiteConnection("Data Source = " + databasePath);
            _logger.Info("Connection created");

            InitializeComponent();
            LoadAllWords();
            UpdateLabelsOneTimeOnly();

            _numberOfCurrentWords = Math.Min(_numberOfTotalWords, STARTING_NUMBER_OF_WORDS_LOADED);

            //NextStep();
        }

        private void UpdateLabelsOneTimeOnly()
        {
            this.lbTotalWords.Text = "Total links: " + _numberOfTotalWords;
            this.lbNumberOfMastered.Text = "Mastered links: " + _numberOfMasteredWords;
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

        private void LoadAllWords()
        {
            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            _wordsToStudy = new List<Domain.Word>();

            _dbConnection.Open();
            _dbSession = NHibernateHelper.GetCustomSession(_dbConnection);

            var allWords = _dbSession.Query<Domain.Word>().ToList();

            foreach (var oneWord in allWords)
            {
                if (oneWord.Disabled == 0 && !oneWord.IsMastered)
                {
                    _numberOfTotalWords++;
                    if (!oneWord.IsMastered)
                    {
                        _wordsToStudy.Add(oneWord);
                    }
                    else
                    {
                        _numberOfMasteredWords++;
                    }
                }
            }
            _logger.CloseSection(_location);
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
