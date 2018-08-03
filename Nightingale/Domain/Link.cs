using System;
using System.Text;
using System.Collections.Generic;

namespace Nightingale.Domain
{
    public class Link
    {
        public Link() { }

        public Link(string datumA, string datumB, string extraA = "")
        {
            Name = datumA + " → " + datumB;
            DatumA = datumA;
            DatumB = datumB;
            ExtraDataA = extraA;
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Disabled { get; set; }

        public virtual Subsource Subsource { get; set; }

        public virtual int MasteryAToB { get; set; }
        public virtual int MasteryBToA { get; set; }
        public virtual string LastStudiedDate { get; set; }
        public virtual string Discriminant { get; set; }

        public virtual string DatumA { get; set; }
        public virtual string DatumB { get; set; }

        public virtual string ExtraDataA { get; set; }

    }
}
