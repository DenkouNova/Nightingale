using System;
using System.Linq;
using System.Text;
using Nightingale.Domain;
using System.Threading.Tasks;
using Nightingale.Extensions;
using System.Collections.Generic;

namespace Nightingale.Parsers
{
    public abstract class AbstractParser
    {
        protected FeatherLogger _logger;

        protected ISet<Category> CategoriesToInsert = new HashSet<Category>();

        private Category _currentCategory;
        private Subcategory _currentSubcategory;
        private Source _currentSource;
        private Subsource _currentSubsource;

        protected void AddNewCategory(Category c)
        {
            CategoriesToInsert.Add(c);

            _currentCategory = c;            
            _currentSubcategory = null;
            _currentSource = null;
            _currentSubsource = null;
        }

        protected void AddNewSubcategory(Subcategory sc)
        {
            _currentCategory.Subcategories.Add(sc);

            _currentSubcategory = sc;
            _currentSource = null;
            _currentSubsource = null;
        }

        protected void AddNewSource(Source s)
        {
            _currentSubcategory.Sources.Add(s);

            _currentSource = s;
            _currentSubsource = null;
        }

        protected void AddNewSubsource(Subsource ss)
        {
            _currentSource.Subsources.Add(ss);

            _currentSubsource = ss;
        }

        protected void AddNewLink(Link l)
        {
            _currentSubsource.Links.Add(l);
        }


    }


}
