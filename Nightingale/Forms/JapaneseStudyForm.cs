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
        private int WORD_ADD_ON_UP = 3; // TODO have in an ini file
        private int WORD_REMOVE_ON_DOWN = 1;

        private FeatherLogger _logger;

        private bool _changesOccurred;

        private string _location;

        private int _numberOfTotalWords;
        private int _numberOfMasteredWords;
        private int _numberOfCurrentWords;

        private List<Domain.Word> _wordsToStudy;

        private Domain.Word CurrentWord;
        private Step CurrentStep;
        private StudyingType CurrentStudyingType;

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

            CreateRightClickMenus();

            _numberOfCurrentWords = Math.Min(_numberOfTotalWords, STARTING_NUMBER_OF_WORDS_LOADED);

            NextStep();
        }

        private void UpdateLabelsOneTimeOnly()
        {
            this.lbTotalWords.Text = "Total words: " + _numberOfTotalWords;
            this.lbNumberOfMastered.Text = "Mastered words: " + _numberOfMasteredWords;
        }

        private void CreateRightClickMenus()
        {
            var menuUp = new ContextMenuStrip();
            var menuItemSuperChangeUp = new ToolStripMenuItem("↑　スーパーチェンジ　↑");
            menuItemSuperChangeUp.Click += new EventHandler(btnSuperChange_Up);
            menuUp.Items.AddRange(new ToolStripItem[] { menuItemSuperChangeUp });
            this.btnMasteryUp.ContextMenuStrip = menuUp;

            var menuDown = new ContextMenuStrip();
            var menuItemSuperChangeDown = new ToolStripMenuItem("↓　スーパーチェンジ　↓");
            menuItemSuperChangeDown.Click += new EventHandler(btnSuperChange_Down);
            menuDown.Items.AddRange(new ToolStripItem[] { menuItemSuperChangeDown });
            this.btnMasteryDown.ContextMenuStrip = menuDown;
        }

        private void NextStep()
        {
            UpdateEveryStepLabels();
            CurrentStep = DetermineNextStep();
            switch (CurrentStep)
            {
                case Step.Question:
                    CurrentWord = GetOneWord(CurrentWord);
                    DisplayOneQuestionWord(CurrentWord);
                    break;
                case Step.Answer:
                    DisplayOneAnswerWord(CurrentWord);
                    break;
                default:
                    var ex = new Exception("Invalid step '" + CurrentStep + "'. Aborting all the things");
                    _logger.Error(ex);
                    throw ex;
            }
        }

        private Domain.Word GetOneWord(Domain.Word currentWord)
        {
            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            Domain.Word returnedWord;

            do
            {
                var maxId = Math.Min((int)_numberOfCurrentWords / 2, _wordsToStudy.Count - 1);
                var randomWordPosition = new Random().Next(0, maxId);
                returnedWord = _wordsToStudy[randomWordPosition];
            }
            while (currentWord != null && currentWord.Id == returnedWord.Id);

            // TODO better logging
            _logger.CloseSection(_location);
            return returnedWord;
        }

        private void DisplayOneQuestionWord(Domain.Word currentWord)
        {
            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            var currentQuote = currentWord.Quote;
            var currentSource = currentQuote.Source;

            // Colored mastery rectangle

            CurrentStudyingType =
                currentWord.ReadingMastery < 100 ? StudyingType.Reading :
                currentWord.KanjiMastery < 100 ? StudyingType.Kanji :
                StudyingType.Translation;

            /*
            if (mastery < 31)
            {
                this.lblIdMastery.BackColor = Color.LightSalmon;
            }
            else if (mastery < 61)
            {
                this.lblIdMastery.BackColor = Color.LightYellow;
            }
            else
            {
                this.lblIdMastery.BackColor = Color.LightGreen;
            }
            */

            if (CurrentStudyingType == StudyingType.Reading)
            {
                this.lblIdMastery.BackColor = Color.LightSalmon;
            }
            else if (CurrentStudyingType == StudyingType.Kanji)
            {
                this.lblIdMastery.BackColor = Color.LightYellow;
            }
            else
            {
                this.lblIdMastery.BackColor = Color.LightGreen;
            }

            var mastery =
                currentWord.ReadingMastery <= 100 ? currentWord.ReadingMastery :
                currentWord.KanjiMastery <= 100 ? currentWord.KanjiMastery :
                currentWord.TranslationMastery;

            this.lblIdMastery.Text =
                "ID:" + currentWord.Id + " " +
                "Lv:" + mastery;

            // Source

            this.lbSource.Text = "「" + currentSource.Text + "」より";

            // Word

            var wordOnTop = 
                CurrentStudyingType == StudyingType.Reading ? currentWord.Kanji :
                CurrentStudyingType == StudyingType.Kanji ? currentWord.Kana :
                "-";

            this.lbWord.Text = wordOnTop;

            // Quote

            var a = currentWord.ReadingMastery;
            var b = currentWord.KanjiMastery;
            var c = currentWord.TranslationMastery;

            var quoteText = currentQuote.Text;
            if (CurrentStudyingType == StudyingType.Kanji)
            {
                quoteText = QuoteKanjiToKanas(quoteText, currentWord.Kanji, currentWord.Kana);
            }
            else if (CurrentStudyingType == StudyingType.Translation)
            {
                quoteText = QuoteKanjiToNakatens(quoteText, currentWord.Kanji, currentWord.Kana);
            }

            this.lbQuote.Text = currentQuote.Character + "「" + quoteText + "」";

            // Translation / definition

            this.lbDefinitionOrTranslation.Text = CurrentStudyingType == StudyingType.Translation ? currentWord.Translation : "";

            
            // Update buttons

            btnNext.Visible = true;
            btnMasteryUp.Visible = btnMasteryDown.Visible = false;

            // TODO better logging
            _logger.CloseSection(_location);
        }

        private void DisplayOneAnswerWord(Domain.Word currentWord)
        {
            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            this.lbQuote.Text = currentWord.Quote.Text;
            this.lbWord.Text = currentWord.Kanji + "【" + currentWord.Kana + "】";
            this.lbDefinitionOrTranslation.Text = currentWord.Translation;

            btnNext.Visible = false;
            btnMasteryUp.Visible = btnMasteryDown.Visible = true;

            // TODO better logging
            _logger.CloseSection(_location);
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

        private void UpdateEveryStepLabels()
        {
            this.lbTotalDisplayedWords.Text = "Displayed words: " + _numberOfCurrentWords;
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
            var eventLocation = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;

            var dialogMessage = "Save before exiting?";

            if (_changesOccurred)
            {
                var result = MessageBox.Show(dialogMessage, "Save?", MessageBoxButtons.YesNoCancel);

                _logger.Info("Result is " + result);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {

                    if (result == DialogResult.Yes)
                    {
                        using (var tx = _dbSession.BeginTransaction())
                        {
                            foreach (var oneWord in _wordsToStudy)
                            {
                                _dbSession.Save(oneWord);
                            }
                            tx.Commit();
                        }
                    }

                    _dbSession.Close();
                    _dbConnection.Close();

                    _logger.CloseSection(eventLocation);
                    _logger.CloseSection(_location);
                }
            }
            else
            {
                _dbSession.Close();
                _dbConnection.Close();

                _logger.CloseSection(eventLocation);
                _logger.CloseSection(_location);
            }
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
            _logger.Info("User pressed Mastery_Up");
            UpPoints();
            NextStep();
        }

        private void UpPoints(bool superChange = false)
        {
            _changesOccurred = true;
            if (CurrentStudyingType == StudyingType.Reading)
            {
                CurrentWord.ReadingMastery = (int)(CurrentWord.ReadingMastery * GlobalObjects.GoodAnswerPrct);
                CurrentWord.ReadingMastery += GlobalObjects.GoodAnswerPoints;
                if (CurrentWord.ReadingMastery > 100 || superChange)
                {
                    CurrentWord.ReadingMastery = 100;
                }
            }
            else if (CurrentStudyingType == StudyingType.Kanji)
            {
                CurrentWord.KanjiMastery = (int)(CurrentWord.KanjiMastery * GlobalObjects.GoodAnswerPrct);
                CurrentWord.KanjiMastery += GlobalObjects.GoodAnswerPoints;
                if (CurrentWord.KanjiMastery > 100 || superChange)
                {
                    CurrentWord.KanjiMastery = 100;
                }
            }
            else
            {
                CurrentWord.TranslationMastery = (int)(CurrentWord.TranslationMastery * GlobalObjects.GoodAnswerPrct);
                CurrentWord.TranslationMastery += GlobalObjects.GoodAnswerPoints;
                if (CurrentWord.TranslationMastery > 100 || superChange)
                {
                    CurrentWord.TranslationMastery = 100;
                }
            }

            _numberOfCurrentWords += WORD_ADD_ON_UP;
        }

        private void DownPoints(bool superChange = false)
        {
            _changesOccurred = true;
            if (CurrentStudyingType == StudyingType.Reading)
            {
                CurrentWord.ReadingMastery = (int)(CurrentWord.ReadingMastery * GlobalObjects.BadAnswerPrct);
                CurrentWord.ReadingMastery += GlobalObjects.BadAnswerPoints;
                if (CurrentWord.ReadingMastery < 0 || superChange)
                {
                    CurrentWord.ReadingMastery = 0;
                }
            }
            else if (CurrentStudyingType == StudyingType.Kanji)
            {
                CurrentWord.KanjiMastery = (int)(CurrentWord.KanjiMastery * GlobalObjects.BadAnswerPrct);
                CurrentWord.KanjiMastery += GlobalObjects.BadAnswerPoints;
                if (CurrentWord.KanjiMastery < 0 || superChange)
                {
                    CurrentWord.KanjiMastery = 0;
                }
            }
            else
            {
                CurrentWord.TranslationMastery = (int)(CurrentWord.TranslationMastery * GlobalObjects.BadAnswerPrct);
                CurrentWord.TranslationMastery += GlobalObjects.BadAnswerPoints;
                if (CurrentWord.TranslationMastery < 0 || superChange)
                {
                    CurrentWord.TranslationMastery = 0;
                }
            }

            _numberOfCurrentWords += WORD_REMOVE_ON_DOWN;
        }

        private void btnMasteryDown_Click(object sender, EventArgs e)
        {
            _logger.Info("User pressed btnMasteryDown_Click");
            DownPoints();
            NextStep();
        }

        private void btnSuperChange_Up(object sender, EventArgs e)
        {
            _logger.Info("User pressed btnSuperChange_Up");
            UpPoints(superChange: true);
            NextStep();
        }

        private void btnSuperChange_Down(object sender, EventArgs e)
        {
            _logger.Info("User pressed btnMasteryDown_Click");
            DownPoints(superChange: true);
            NextStep();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _logger.Info("User pressed Next");
            NextStep();
        }

        private enum Step
        {
            Initializing,

            Question,
            Answer,

            Error
        }

        private enum StudyingType
        {
            Reading,
            Kanji,
            Translation
        }
    }

    
}
