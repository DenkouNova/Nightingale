using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SQLite;
using System.Collections.Generic;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;

namespace Nightingale.Parsers
{
    public class JwpceParser : AbstractParser
    {
        private string _location; // used for ParseLine and its return

        private string _kanji;
        private string _kana;
        private string _translation;

        public JwpceParser()
        {
            _logger = GlobalObjects.Logger;
        }

        protected override void ParseAllLines(string[] allLines)
        {
        }





    }
}
