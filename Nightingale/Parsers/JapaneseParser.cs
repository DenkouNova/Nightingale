using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SQLite;
using Nightingale.Domain;
using System.Collections.Generic;

namespace Nightingale.Parsers
{
    public class JapaneseParser : AbstractParser
    {
        private string _location; // used for ParseLine and its return
        private string _allText;

        private string _kanji;
        private string _kana;
        private string _translation;

        public JapaneseParser()
        {
            _logger = GlobalObjects.Logger;
        }

        public bool ImportFile(string databasePath, string filePath)
        {
            _allText = File.ReadAllText(filePath);

            using (var dbConnection = new SQLiteConnection("Data Source = " + databasePath))
            {
                dbConnection.Open();
                using (var dbSession = NHibernateHelper.GetCustomSession(dbConnection))
                {
                    var allLines = _allText.Replace("\r\n", "\r").Split('\r');
                    ParseAllLines(allLines);

                    using (var transaction = dbSession.BeginTransaction())
                    {
                        foreach (var oneCategory in CategoriesToInsert)
                        {
                            _logger.Info("Saving category in database '" + oneCategory.Name + "'...");
                            dbSession.Save(oneCategory);
                            _logger.Info("Saved.");
                        }
                        transaction.Commit();
                    }
                    dbSession.Close();
                }
                dbConnection.Close();
            }

            // TODO return false when error
            return true;
        }





        private void ParseAllLines(string[] allLines)
        {
            var lastLineType = JapaneseParserLineType.Nothing;

            foreach (var oneLine in allLines)
            {
                var result = ParseLine(oneLine, lastLineType);
                var lineType = result.Item1;
                var contents = result.Item2;

                switch (lineType)
                {
                    case JapaneseParserLineType.Nothing:
                    case JapaneseParserLineType.Comment:
                        break;
                    case JapaneseParserLineType.Category:
                        _logger.Info("Adding new category '" + contents + "'");
                        AddNewCategory(new Domain.Category(contents));
                        break;
                    case JapaneseParserLineType.Subcategory:
                        _logger.Info("Adding new subcategory '" + contents + "'");
                        AddNewSubcategory(new Domain.Subcategory(contents));
                        break;
                    case JapaneseParserLineType.Source:
                        _logger.Info("Adding new source '" + contents + "'");
                        AddNewSource(new Domain.Source(contents));
                        break;
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
                }
                lastLineType = lineType;

            }


        }




        public Tuple<JapaneseParserLineType, string> ParseLine(
            string line, JapaneseParserLineType lastLineType = JapaneseParserLineType.Nothing)
        {
            _location = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            _logger.OpenSection(_location);

            if (line.Length == 0)
                return ReturnParse(JapaneseParserLineType.Nothing, null);

            var firstCharacter = line.Substring(0, 1);

            if (firstCharacter == "#")
                return ReturnParse(JapaneseParserLineType.Comment, null);

            if (firstCharacter == ":")
            {
                var contentStart = line.IndexOf("=");
                if (contentStart > -1)
                {
                    var content = line.Substring(contentStart + 1);

                    if (line.IndexOf(":src=", StringComparison.InvariantCultureIgnoreCase) == 0)
                        return ReturnParse(JapaneseParserLineType.Source, content);

                    if (line.IndexOf(":source=", StringComparison.InvariantCultureIgnoreCase) == 0)
                        return ReturnParse(JapaneseParserLineType.Source, content);

                    if (line.IndexOf(":subcat=", StringComparison.InvariantCultureIgnoreCase) == 0)
                        return ReturnParse(JapaneseParserLineType.Subcategory, content);

                    if (line.IndexOf(":subcategory=", StringComparison.InvariantCultureIgnoreCase) == 0)
                        return ReturnParse(JapaneseParserLineType.Subcategory, content);

                    if (line.IndexOf(":cat=", StringComparison.InvariantCultureIgnoreCase) == 0)
                        return ReturnParse(JapaneseParserLineType.Category, content);

                    if (line.IndexOf(":category=", StringComparison.InvariantCultureIgnoreCase) == 0)
                        return ReturnParse(JapaneseParserLineType.Category, content);
                }
            }

            if (line.IndexOf("「") > -1)
                return ReturnParse(JapaneseParserLineType.Subsource, line);

            // Link

            if (lastLineType == JapaneseParserLineType.Translation)
                return ReturnParse(JapaneseParserLineType.Kanji, line);

            if (lastLineType == JapaneseParserLineType.Subsource)
                return ReturnParse(JapaneseParserLineType.Kanji, line);

            if (lastLineType == JapaneseParserLineType.Kanji)
                return ReturnParse(JapaneseParserLineType.Kana, line);

            if (lastLineType == JapaneseParserLineType.Kana)
                return ReturnParse(JapaneseParserLineType.Romaji, line);

            if (lastLineType == JapaneseParserLineType.Romaji)
                return ReturnParse(JapaneseParserLineType.Translation, line);

            if (line.Replace(" ", "").Replace("\t", "").Length == 0)
                return ReturnParse(JapaneseParserLineType.Nothing, null);

            var ex = new Exception("Could not parse line: '" + line + "'");
            _logger.Error(ex);
            throw ex;
        }

        private Tuple<JapaneseParserLineType, string> ReturnParse(
            JapaneseParserLineType lineType, string parsedString)
        {
            _logger.Info("Parsed as line type: " + lineType);
            _logger.Info("Returned content: '" + parsedString + "'");
            _logger.CloseSection(_location);

            return new Tuple<JapaneseParserLineType, string>(lineType, parsedString);
        }


    }
}
