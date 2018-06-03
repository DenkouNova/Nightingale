using System;
using System.IO;
using System.Linq;
using System.Text;
using Nightingale;
using NUnit.Framework;
using System.Threading;
using System.Data.SQLite;
using Nightingale.Parsers;
using System.Collections.Generic;

namespace NightingaleUnitTests
{

    [TestFixture]
    [Category("Parsers")]
    public class GivenAJapaneseParser
    {
        private JapaneseParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new JapaneseParser();
        }

        [Test]
        public void GivenALineThatStartsWithColonCategoryOrColonCat_ThenLineIsParsedAsCategory(
            [Values("CaTEgORY", "cAT")] string stringValue)
        {
            var category = "blagoblag";
            var result = _parser.ParseLine(":" + stringValue + "=" + category);
            Assert.AreEqual(JapaneseParserLineType.Category, result.Item1);
            Assert.AreEqual(category, result.Item2);
        }

        [Test]
        public void GivenALineThatStartsWithColonSubcategoryOrColonSubCat_ThenLineIsParsedAsSubcategory(
            [Values("sUbCaTEgORY", "subcaT")] string stringValue)
        {
            var subcategory = "gragragrou";
            var result = _parser.ParseLine(":" + stringValue + "=" + subcategory);
            Assert.AreEqual(JapaneseParserLineType.Subcategory, result.Item1);
            Assert.AreEqual(subcategory, result.Item2);
        }

        [Test]
        public void GivenALineThatStartsWithColonSourceOrColonSrc_ThenLineIsParsedAsSubcategory(
            [Values("sOURCe", "SRc")] string stringValue)
        {
            var source = "blabloub";
            var result = _parser.ParseLine(":" + stringValue + "=" + source);
            Assert.AreEqual(JapaneseParserLineType.Source, result.Item1);
            Assert.AreEqual(source, result.Item2);
        }

        [Test]
        public void GivenALineThatContainsBothKagikakko_ThenLineIsParsedAsSubsource()
        {
            var subSource = "DFAGFAJDHFAHGERJFAD「";
            var result = _parser.ParseLine(subSource);
            Assert.AreEqual(JapaneseParserLineType.Subsource, result.Item1);
            Assert.AreEqual(subSource, result.Item2);
        }

        [Test]
        public void GivenALineThatStartsWithANumberSign_ThenLineIsParsedAsComment()
        {
            var result = _parser.ParseLine("#gfahfahre");
            Assert.AreEqual(JapaneseParserLineType.Comment, result.Item1);
        }

        [Test]
        public void GivenAnEmptyLine_ThenLineIsParsedAsEmpty()
        {
            var result = _parser.ParseLine("");
            Assert.AreEqual(JapaneseParserLineType.Nothing, result.Item1);
        }

        [Test]
        public void GivenALineWithJustSpaces_ThenLineIsParsedAsEmpty()
        {
            var result = _parser.ParseLine("         ");
            Assert.AreEqual(JapaneseParserLineType.Nothing, result.Item1);
        }

        [Test]
        public void GivenALineWithJustTabs_ThenLineIsParsedAsEmpty()
        {
            var result = _parser.ParseLine("\t\t");
            Assert.AreEqual(JapaneseParserLineType.Nothing, result.Item1);
        }

        [Test]
        public void GivenALineWithNormalText_WhenTheLastLineWasASubSourceOrATranslation_ThenLineIsKanji(
            [Values(JapaneseParserLineType.Subsource, JapaneseParserLineType.Translation)] JapaneseParserLineType lastLineType)
        {
            var kanji = "贅沢";
            var result = _parser.ParseLine(kanji, lastLineType);
            Assert.AreEqual(JapaneseParserLineType.Kanji, result.Item1);
            Assert.AreEqual(kanji, result.Item2);
        }

        [Test]
        public void GivenALineWithNormalText_WhenTheLastLineWasKanji_ThenLineIsKana()
        {
            var kana = "ぜいたく";
            var result = _parser.ParseLine(kana, JapaneseParserLineType.Kanji);
            Assert.AreEqual(JapaneseParserLineType.Kana, result.Item1);
            Assert.AreEqual(kana, result.Item2);
        }

        [Test]
        public void GivenALineWithNormalText_WhenTheLastLineWasKana_ThenLineIsRomaji()
        {
            var romaji = "zeitaku";
            var result = _parser.ParseLine(romaji, JapaneseParserLineType.Kana);
            Assert.AreEqual(JapaneseParserLineType.Romaji, result.Item1);
            Assert.AreEqual(romaji, result.Item2);
        }

        [Test]
        public void GivenALineWithNormalText_WhenTheLastLineWasRomaji_ThenLineIsTranslation()
        {
            var translation = "luxury or whatever";
            var result = _parser.ParseLine(translation, JapaneseParserLineType.Romaji);
            Assert.AreEqual(JapaneseParserLineType.Translation, result.Item1);
            Assert.AreEqual(translation, result.Item2);
        }

    }
}
