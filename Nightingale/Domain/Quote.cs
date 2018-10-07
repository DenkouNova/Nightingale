using System;
using System.Text;
using System.Collections.Generic;

namespace Nightingale.Domain
{

    public class Quote
    {
        protected Quote()
        {
            Words = new HashSet<Word>();
        }

        public Quote(string character, string text)
        {
            Words = new HashSet<Word>();

            Character = character;
            Text = text;
        }

        public virtual int Id { get; set; }
        public virtual string Character { get; set; }
        public virtual string Text { get; set; }
        public virtual ISet<Word> Words { get; set; }
        public virtual Source Source { get; set; }

        public override string ToString()
        {
            return "Id = " + FeatherStrings.TraceString(Id) +
                ", " + "Text = " + FeatherStrings.TraceString(Text) +
                ", " + "Character = " + FeatherStrings.TraceString(Character) +
                ", " + "Words.Count = " + FeatherStrings.TraceString(Words.Count) +
                ", " + "Source.Id = " + FeatherStrings.TraceString(Source.Id);
        }
    }
}
