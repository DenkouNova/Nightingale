using System;
using System.Text;
using System.Collections.Generic;

namespace Nightingale.Domain
{
    public class Category
    {
        public Category()
        {
            Subcategories = new HashSet<Subcategory>();
        }

        public Category(string name)
        {
            Subcategories = new HashSet<Subcategory>();
            Name = name;
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Disabled { get; set; }

        public virtual ISet<Subcategory> Subcategories { get; set; }
    }
}
