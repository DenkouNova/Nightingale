using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SQLite;
using System.Collections.Generic;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;

namespace Nightingale.Parsers
{
    public class TakobotoParser : AbstractParser
    {
        FeatherLogger _logger;

        private string _location; // used for ParseLine and its return
        private string _allText;

        private string _kanji;
        private string _kana;
        private string _translation;

        public TakobotoParser()
        {
            _logger = GlobalObjects.Logger;
        }

        public override bool ImportFile(string databasePath, string filePath)
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            _allText = File.ReadAllText(filePath);

            try
            {
                using (var dbConnection = new SQLiteConnection("Data Source = " + databasePath))
                {
                    dbConnection.Open();
                    using (var dbSession = NHibernateHelper.GetCustomSession(dbConnection))
                    {
                        var allLines = _allText.Replace("\r\n", "\r").Split('\r');
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

        private void ParseAllLines(string[] allLines)
        {
            string location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(location);

            var lastLineType = LineTypeEnum.Nothing;

            foreach (var oneLine in allLines)
            {
                var result = ParseLine(oneLine, lastLineType);
                var lineType = result.Item1;
                var contents = result.Item2;

                switch (lineType)
                {
                    case LineTypeEnum.Nothing:
                    case LineTypeEnum.Comment:
                        break;
                    case LineTypeEnum.Source:
                        _logger.Info("Adding new source '" + contents + "'");
                        var source = new Domain.Source(contents);
                        AddNewSource(source);
                        break;
                    case LineTypeEnum.Quote:
                        _logger.Info("Adding new quote '" + contents + "'");
                        var character = contents.Substring(0, contents.IndexOf("「"));
                        var quoteText = FeatherStrings.GetTextBetween(contents, "「", "」");

                        var quote = new Domain.Quote(character, quoteText);
                        AddNewQuote(quote);
                        break;
                        /*
                    case JapaneseParserLineType.Subsource:
                        _logger.Info("Adding new subsource '" + contents + "'");
                        AddNewSubsource(new Domain.Subsource(contents));
                        break;
                    case JapaneseParserLineType.Kanji:
                        if (!String.IsNullOrEmpty(_kanji) ||
                            !String.IsNullOrEmpty(_kana) ||
                            !String.IsNullOrEmpty(_translation))
                        {
                            var ex = new Exception("Kanji is not valid. A word is currently being created. " +
                            "_kanji = '" + _kanji + "', _kana = '" + _kana + "', _translation = '" + _translation + "'");
                            _logger.Error(ex);
                            throw ex;
                        }
                        _kanji = contents;
                        break;
                    case JapaneseParserLineType.Kana:
                        if (String.IsNullOrEmpty(_kanji) ||
                            !String.IsNullOrEmpty(_kana) ||
                            !String.IsNullOrEmpty(_translation))
                        {
                            var ex = new Exception("Kana is not valid. Must be constructing a new word. " +
                            "_kanji = '" + _kanji + "', _kana = '" + _kana + "', _translation = '" + _translation + "'");
                            _logger.Error(ex);
                            throw ex;
                        }
                        _kana = contents;
                        break;
                    case JapaneseParserLineType.Translation:
                        if (String.IsNullOrEmpty(_kanji) ||
                            String.IsNullOrEmpty(_kana) ||
                            !String.IsNullOrEmpty(_translation))
                        {
                            var ex = new Exception("Translation is not valid. Must be constructing a new word. " +
                            "_kanji = '" + _kanji + "', _kana = '" + _kana + "', _translation = '" + _translation + "'");
                            _logger.Error(ex);
                            throw ex;
                        }
                        _translation = contents;
                        // Insert for kana-kanji
                        var word = new Link(_kanji, _kana, _translation);
                        word.Discriminant = "かな漢字";
                        AddNewLink(word);
                        // Insert for 和英
                        var kanaKanji = _kanji + " (" + _kana + ")";
                        word = new Link(kanaKanji, _translation);
                        word.MasteryAToB = 100; // Cancel the Japanese-to-English translation
                        word.Discriminant = "和英";
                        AddNewLink(word);
                        // Revert to "not currently inserting a word" mode
                        _kanji = _kana = _translation = null;
                        break;
                         * */
                }
                lastLineType = lineType;

            }

            _logger.CloseSection(location);
        }

        private Tuple<LineTypeEnum, string> ParseLine(string line, LineTypeEnum lastLineType = LineTypeEnum.Nothing)
        {
            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            _logger.Info("Parsing line '" + line + "'...");

            if (line.Length == 0)
                return ReturnParse(LineTypeEnum.Nothing, null);

            var firstCharacter = line.Substring(0, 1);

            if (firstCharacter == "#")
                return ReturnParse(LineTypeEnum.Comment, null);

            if (firstCharacter == ":")
            {
                var contentStart = line.IndexOf("=");
                if (contentStart > -1)
                {
                    var content = line.Substring(contentStart + 1);

                    if (line.IndexOf(":src=", StringComparison.InvariantCultureIgnoreCase) == 0)
                        return ReturnParse(LineTypeEnum.Source, content);

                    if (line.IndexOf(":WordStyle=", StringComparison.InvariantCultureIgnoreCase) == 0)
                        return ReturnParse(LineTypeEnum.Comment, content);

                    if (line.IndexOf(":cat=", StringComparison.InvariantCultureIgnoreCase) == 0)
                        return ReturnParse(LineTypeEnum.Comment, content);

                    if (line.IndexOf(":subcat=", StringComparison.InvariantCultureIgnoreCase) == 0)
                        return ReturnParse(LineTypeEnum.Comment, content);
                }
            }

            if (line.Contains("「") && line.Contains("」"))
                return ReturnParse(LineTypeEnum.Quote, line);

            if (lastLineType == LineTypeEnum.Quote || lastLineType == LineTypeEnum.Translation)
                return ReturnParse(LineTypeEnum.Kanji, line);

            if (lastLineType == LineTypeEnum.Kanji)
                return ReturnParse(LineTypeEnum.Kana, line);

            if (lastLineType == LineTypeEnum.Kana)
                return ReturnParse(LineTypeEnum.Romaji, line);

            if (lastLineType == LineTypeEnum.Romaji)
                return ReturnParse(LineTypeEnum.Romaji, line);


            // temporary
            return ReturnParse(LineTypeEnum.Comment, null);


            var ex = new Exception("Could not parse line: '" + line + "'");
            _logger.Error(ex);
            throw ex;
        }

        private Tuple<LineTypeEnum, string> ReturnParse(
            LineTypeEnum lineType, string parsedString)
        {
            _logger.Info("Parsed as line type: " + lineType);
            _logger.Info("Returned content: '" + parsedString + "'");
            _logger.CloseSection(_location);

            return new Tuple<LineTypeEnum, string>(lineType, parsedString);
        }




    }
}
