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
    public class JwpceParser : AbstractParser
    {
        private string _location; // used for ParseLine and its return

        public JwpceParser()
        {
            _logger = GlobalObjects.Logger;
        }

        protected override void ParseAllLines(string[] allLines)
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
                    case LineTypeEnum.Kanji:
                    case LineTypeEnum.Kana:
                    case LineTypeEnum.Romaji:
                    case LineTypeEnum.Translation:
                        var ex = new Exception("Line type '" + lineType + "' is not a valid line type for this parser.");
                        _logger.Error(ex);
                        throw ex;
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
                    case LineTypeEnum.JwpceWord:
                        var kanji = contents.Substring(0, contents.IndexOf("【"));
                        var kana = FeatherStrings.GetTextBetween(contents, "【", "】");
                        var translation = contents.Substring(contents.IndexOf("】")+1);

                        var word = new Nightingale.Domain.Word(kanji, kana, translation);
                        AddNewWord(word);
                        break;
                }
                lastLineType = lineType;
            }

            _logger.CloseSection(location);
        }

        protected override Tuple<LineTypeEnum, string> ParseLine(string line, LineTypeEnum lastLineType = LineTypeEnum.Nothing)
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
                    if (line.IndexOf(":WordStyle=", StringComparison.InvariantCultureIgnoreCase) == 0)
                        return ReturnParse(LineTypeEnum.Comment, content);
                }
            }

            if (line.Contains("【") && line.Contains("】"))
                return ReturnParse(LineTypeEnum.JwpceWord, line);

            if (line.Contains("「") && line.Contains("」") && lastLineType != LineTypeEnum.Quote)
                return ReturnParse(LineTypeEnum.Quote, line);

            if (lastLineType != LineTypeEnum.Source)
            {
                return ReturnParse(LineTypeEnum.Source, line);
            }

            var ex = new Exception("Could not parse line: '" + line + "'");
            _logger.Error(ex);
            throw ex;
        }





    }
}
