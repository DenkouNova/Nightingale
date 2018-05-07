using System;
using System.Text;
using System.Collections.Generic;

namespace Nightingale.Domain
{
    public class Subsource
    {
        public Subsource()
        {
            Links = new HashSet<Link>();
        }

        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Disabled { get; set; }

        public virtual Source Source { get; set; }

        public virtual ISet<Link> Links { get; set; }
    }
}
