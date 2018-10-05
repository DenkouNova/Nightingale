using System;
using System.Text;
using System.Collections.Generic;

namespace Nightingale.Domain
{

    public class Source
    {
        public Source() { }

        public Source(string text)
        {
            Text = text;
            Quotes = new HashSet<Quote>();
        }

        public virtual int? Id { get; set; }
        public virtual string Text { get; set; }
        public virtual ISet<Quote> Quotes { get; set; }

        public override string ToString()
        {
            return "Id = " + FeatherStrings.TraceString(Id) +
                ", " + "Text = " + FeatherStrings.TraceString(Text) +
                ", " + "Quotes.Count = " + FeatherStrings.TraceString(Quotes.Count);
        }
    }
}
