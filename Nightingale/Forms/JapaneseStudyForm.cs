﻿using System;
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

        //private Dictionary<int, Domain.Link> _masteryAtoBLinksToStudy;
        //private Dictionary<int, Domain.Link> _masteryBtoALinksToStudy;
        private List<Domain.Link> _masteryAtoBLinksToStudy;
        private List<Domain.Link> _masteryBtoALinksToStudy;

        private Domain.Link CurrentLink;
        private Step CurrentStep;
        private AorB AorBMode = AorB.A_Mode;

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

            NextStep();
        }

        private void JapaneseStudyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _logger.CloseSection(_location);
        }

        private void NextStep()
        {
            CurrentStep = DetermineNextStep();
            switch (CurrentStep)
            {
                case Step.Question:
                    CurrentLink = GetOneWord(CurrentLink);
                    DisplayOneQuestionWord(CurrentLink);
                    break;
                case Step.Answer:
                    DisplayOneAnswerWord(CurrentLink);
                    break;
                default:
                    var ex = new Exception("Invalid step '" + CurrentStep + "'. Aborting all the things");
                    _logger.Error(ex);
                    throw ex;
            }
        }

        private Step DetermineNextStep()
        {
            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            Step NextStep;
            switch (CurrentStep)
            {
                case Step.Initializing:
                case Step.Answer:
                    // either display the next word, or display the first word
                    NextStep = Step.Question;
                    break;
                case Step.Question:
                    NextStep = Step.Answer;
                    break;
                default:
                    NextStep = Step.Error;
                    break;
            }
            // TODO better logging
            _logger.CloseSection(_location);
            return NextStep;
        }


        private void DisplayOneQuestionWord(Domain.Link currentLink)
        {
            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            var subsource = currentLink.Subsource;
            var source = subsource.Source;

            this.lbSource.Text = "「" + source.Name + "」より";
            this.lbWord.Text = currentLink.DatumA;

            var quote = subsource.Name;
            string word;
            if (currentLink.Discriminant == "かな漢字")
            {
                var kanji = currentLink.DatumA;
                var kana = currentLink.DatumB;

                if (AorBMode == AorB.A_Mode)
                {
                    // Kanji to kana
                    // Given the kanji, find the kana.
                    word = kanji;
                    // Quote can retain the original kanji.
                    
                }
                else
                {
                    // Kana to kanji
                    // Given the kana, find the kanji.
                    word = kana;
                    // Naturally, the kanji must be removed from the quote itself
                    quote = QuoteKanjiToKanas(quote, kanji, kana);
                }
            }
            else if (currentLink.Discriminant == "和英")
            {
                var japanese = currentLink.DatumA;
                var english = currentLink.DatumB;

                if (AorBMode == AorB.A_Mode)
                {
                    // Japanese to English
                    word = japanese;
                    // Quote can retain all of its characters.
                }
                else
                {
                    // English to Japanese
                    word = english;
                    // Quote must be stripped of the word we are trying to study.
                    var separationIndex1 = japanese.IndexOf("(");
                    var separationIndex2 = japanese.IndexOf(")");
                    var kanji = japanese.Substring(0, separationIndex1);
                    var kana = japanese.Substring(separationIndex1 + 1, separationIndex2 - separationIndex1 - 1);
                    quote = QuoteKanjiToNakatens(quote, japanese, kana);
                }
            }
            else
            {
                var ex = new Exception("Invalid discriminant '" + currentLink.Discriminant + 
                    "'. Aborting all the things");
                _logger.Error(ex);
                throw ex;
            }

            this.lbQuote.Text = quote;
            this.lbWord.Text = word;
            this.lbDefinitionOrTranslation.Text = "";

            this.lblIdMastery.Text = 
                "ID:" + currentLink.Id + " " +
                "Mastery:" + (AorBMode == AorB.A_Mode ? currentLink.MasteryAToB : currentLink.MasteryBToA) + " " +
                currentLink.Discriminant;

            btnNext.Visible = true;
            btnMasteryUp.Visible = btnMasteryDown.Visible = false;
            // TODO better logging
            _logger.CloseSection(_location);
        }

        private void DisplayOneAnswerWord(Domain.Link currentLink)
        {
            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            var quote = currentLink.Subsource.Name;

            string word;
            string translation = "";
            if (currentLink.Discriminant == "かな漢字")
            {
                var kanji = currentLink.DatumA;
                var kana = currentLink.DatumB;
                word = kanji + "(" + kana + ")";
                translation = currentLink.ExtraDataA;
            }
            else if (currentLink.Discriminant == "和英")
            {
                word = currentLink.DatumA;
                translation = currentLink.DatumB;
            }
            else
            {
                var ex = new Exception("Invalid discriminant '" + currentLink.Discriminant +
                    "'. Aborting all the things");
                _logger.Error(ex);
                throw ex;
            }

            this.lbQuote.Text = quote;
            this.lbWord.Text = word;
            this.lbDefinitionOrTranslation.Text = translation;

            btnNext.Visible = false;
            btnMasteryUp.Visible = btnMasteryDown.Visible = true;
            // TODO better logging
            _logger.CloseSection(_location);
        }

        private Domain.Link GetOneWord(Domain.Link currentLink)
        {
            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            Domain.Link returnedLink;

            do
            {
                // either mastery A or B
                var AorBRandom = new Random().Next(0, 1);
                if (AorBRandom == 0)
                {
                    AorBMode = AorB.A_Mode;
                    var randomA = new Random().Next(0, _masteryAtoBLinksToStudy.Count - 1);
                    returnedLink = _masteryAtoBLinksToStudy[randomA];
                }
                else
                {
                    AorBMode = AorB.B_Mode;
                    var randomB = new Random().Next(0, _masteryBtoALinksToStudy.Count - 1);
                    returnedLink = _masteryBtoALinksToStudy[randomB];
                }
            }
            while (currentLink != null && currentLink.Id == returnedLink.Id);

            // TODO better logging
            _logger.CloseSection(_location);
            return returnedLink;
        }


        private void LoadAllWords()
        {
            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            // _masteryAtoBLinksToStudy = new Dictionary<int, Domain.Link>();
            // _masteryBtoALinksToStudy = new Dictionary<int, Domain.Link>();
            _masteryAtoBLinksToStudy = new List<Domain.Link>();
            _masteryBtoALinksToStudy = new List<Domain.Link>();

            _dbConnection.Open();
            using (var dbSession = NHibernateHelper.GetCustomSession(_dbConnection))
            {
                var allLinks = dbSession.Query<Domain.Link>().ToList();

                foreach(var oneLink in allLinks)
                {
                    if (oneLink.Disabled == 0)
                    {
                        if (oneLink.MasteryAToB < 100)
                        {
                            // _masteryAtoBLinksToStudy.Add(oneLink.Id.Value, oneLink);
                            _masteryAtoBLinksToStudy.Add(oneLink);
                        }
                        if (oneLink.MasteryBToA < 100)
                        {
                            // _masteryBtoALinksToStudy.Add(oneLink.Id.Value, oneLink);
                            _masteryBtoALinksToStudy.Add(oneLink);
                        }
                    }
                }
                
            }
            _dbConnection.Close();
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



        // TODO REFACTOR THIS IS OLD CRAPPY CODE
        private string QuoteKanjiToNakatens(string p_strQuote, string p_strKanji, string p_strKana)
        {
            string strReturn;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < p_strKanji.Length; i++)
            {
                sb.Append("・");
            }

            if (p_strKanji != p_strKana)
            {
                while (p_strKanji.Substring(p_strKanji.Length - 1) ==
                    p_strKana.Substring(p_strKana.Length - 1))
                {
                    p_strKanji = p_strKanji.Substring(0, p_strKanji.Length - 1);
                    p_strKana = p_strKana.Substring(0, p_strKana.Length - 1);
                }
            }
            strReturn = p_strQuote.Replace(p_strKanji, sb.ToString());

            return strReturn;

        }

        // TODO REFACTOR THIS IS OLD CRAPPY CODE
        private string QuoteKanjiToKanas(string p_strQuote, string p_strKanji, string p_strKana)
        {
            string strReturn;
            if (p_strKanji == p_strKana)
            {
                strReturn = p_strQuote; // No change needed
            }
            else
            {
                while (p_strKanji.Substring(p_strKanji.Length - 1) ==
                    p_strKana.Substring(p_strKana.Length - 1))
                {
                    p_strKanji = p_strKanji.Substring(0, p_strKanji.Length - 1);
                    p_strKana = p_strKana.Substring(0, p_strKana.Length - 1);
                }
                strReturn = p_strQuote.Replace(p_strKanji, p_strKana);
            }

            return strReturn;
        }




        private enum AorB
        {
            A_Mode,
            B_Mode
        }

        private enum Step
        {
            Initializing,

            Question,
            Answer,

            Error
        }

        private void btnMasteryUp_Click(object sender, EventArgs e)
        {
            NextStep();
        }

        private void btnMasteryDown_Click(object sender, EventArgs e)
        {
            NextStep();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NextStep();
        }


    }
}