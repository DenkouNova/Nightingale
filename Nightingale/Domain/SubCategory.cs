using System;
using System.Text;
using System.Collections.Generic;

namespace Nightingale.Domain
{
    public class Subcategory
    {
        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Disabled { get; set; }

        public virtual Category Category { get; set; }
    }
}
