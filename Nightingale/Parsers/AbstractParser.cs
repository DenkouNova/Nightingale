using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SQLite;
using Nightingale.Domain;
using System.Diagnostics;
using System.Threading.Tasks;
using Nightingale.Extensions;
using System.Collections.Generic;

namespace Nightingale.Parsers
{
    public abstract class AbstractParser
    {
        protected FeatherLogger _logger;

        protected string _location; // used for ParseLine and its return

        protected ISet<Source> SourcesToInsert = new HashSet<Source>();

        private Source _currentSource;
        private Quote _currentQuote;
        private Word _currentWord;

        public bool ImportFile(string databasePath, string filePath)
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            var allText = File.ReadAllText(filePath);

            try
            {
                using (var dbConnection = new SQLiteConnection("Data Source = " + databasePath))
                {
                    dbConnection.Open();
                    using (var dbSession = NHibernateHelper.GetCustomSession(dbConnection))
                    {
                        var allLines = allText.Replace("\r\n", "\r").Split('\r');
                        ParseAllLines(allLines);

                        using (var transaction = dbSession.BeginTransaction())
                        {
                            foreach (var oneSource in SourcesToInsert)
                            {
                                _logger.Info("Saving source in database '" + oneSource.Text + "'...");
                                dbSession.Save(oneSource);
                                _logger.Info("Saved.");
                            }
                            transaction.Commit();
                        }
                        dbSession.Close();
                    }
                    dbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _logger.CloseSection(location);
                return false;
            }

            _logger.CloseSection(location);
            return true;
        }

        protected abstract void ParseAllLines(string[] allLines);

        protected abstract Tuple<LineTypeEnum, string> ParseLine(string line, LineTypeEnum lastLineType = LineTypeEnum.Nothing);

        protected void AddNewSource(Source s)
        {
            SourcesToInsert.Add(s);
            _currentSource = s;
            _currentQuote = null;
            _currentWord = null;
        }

        protected void AddNewQuote(Quote q)
        {
            _currentSource.Quotes.Add(q);
            _currentQuote = q;
            _currentWord = null;
        }

        protected void AddNewWord (Word w)
        {
            _currentQuote.Words.Add(w);
            _currentWord = w;
        }

        protected bool LineContainsKana(string oneLine)
        {
            bool containsKana = false;
            var charArray =
                "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわゐゑをんがぎぐげござじずぜぞだぢづでどばびぶべぼぱぴぷぺぽ"
                .ToCharArray();

            for (int i = 0; i < charArray.Length && !containsKana; i++)
                containsKana = oneLine.Contains(charArray[i]);

            return containsKana;
        }

        protected bool LineContainsAlphabetLetters(string oneLine)
        {
            bool containsAlphabetLetters = false;
            var charArray = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            for (int i = 0; i < charArray.Length && !containsAlphabetLetters; i++)
                containsAlphabetLetters = oneLine.Contains(charArray[i]);

            return containsAlphabetLetters;
        }

        protected Tuple<LineTypeEnum, string> ReturnParse(
            LineTypeEnum lineType, string parsedString)
        {
            _logger.Info("Parsed as line type: " + lineType);
            _logger.Info("Returned content: '" + parsedString + "'");
            _logger.CloseSection(_location);

            return new Tuple<LineTypeEnum, string>(lineType, parsedString);
        }

    }


}
