using System;
using System.Text;
using System.Collections.Generic;

namespace Nightingale.Domain
{
    public class Source
    {
        public Source()
        {
            Subsources = new HashSet<Subsource>();
        }

        public Source(string name)
        {
            Subsources = new HashSet<Subsource>();
            Name = name;
        }

        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Disabled { get; set; }

        public virtual Subcategory Subcategory { get; set; }

        public virtual ISet<Subsource> Subsources { get; set; }
    }
}
