using System;
using System.Text;
using System.Collections.Generic;

namespace Nightingale.Domain
{

    public class Word
    {
        protected Word() { }

        public Word(string kanji, string kana, string translation)
        {
            Kanji = kanji;
            Kana = kana;
            Translation = translation;
        }

        public virtual int Id { get; set; }
        public virtual string Kanji { get; set; }
        public virtual string Kana { get; set; }
        public virtual string Translation { get; set; }
        public virtual int ReadingMastery { get; set; }
        public virtual int TranslationMastery { get; set; }
        public virtual int KanjiMastery { get; set; }
        public virtual int Stars { get; set; }
        public virtual string LastStudied { get; set; }
        public virtual int Disabled { get; set; }
        public virtual Quote Quote { get; set; }

        public virtual bool IsMastered { get {
            return ReadingMastery >= 100 &&
            TranslationMastery >= 100 &&
            KanjiMastery >= 100;
        }}
            

        public override string ToString()
        {
            return "Id = " + FeatherStrings.TraceString(Id) +
                ", " + "Kanji = " + FeatherStrings.TraceString(Kanji) +
                ", " + "Kana = " + FeatherStrings.TraceString(Kana) +
                ", " + "Translation = " + FeatherStrings.TraceString(Translation) +
                ", " + "ReadingMastery = " + FeatherStrings.TraceString(ReadingMastery) +
                ", " + "TranslationMastery = " + FeatherStrings.TraceString(TranslationMastery) +
                ", " + "KanjiMastery = " + FeatherStrings.TraceString(KanjiMastery) +
                ", " + "Stars = " + FeatherStrings.TraceString(Stars) +
                ", " + "Text = " + FeatherStrings.TraceString(LastStudied) +
                ", " + "Disabled = " + FeatherStrings.TraceString(Disabled) + " (" + (Disabled > 0) + ")" +
                ", " + "Quote.Id = " + FeatherStrings.TraceString(Quote.Id);
        }

    }
}
