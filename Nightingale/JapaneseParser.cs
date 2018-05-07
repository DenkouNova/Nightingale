using System;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Collections.Generic;

namespace Nightingale
{
    public static class JapaneseParser
    {
        public static void DoShit(string databasePath)
        {
            // Just add a new entry in the Category table, for testing.

            // Category
            using (var dbConnection = new SQLiteConnection("Data Source = " + databasePath))
            {
                dbConnection.Open();
                using (var dbSession = NHibernateHelper.GetCustomSession(dbConnection))
                {
                    var oneCategory = new Domain.Category()
                    {
                        Name = "abc",
                        Disabled = 0
                    };

                    var oneSubcategory = new Domain.Subcategory()
                    {
                        Name = "def",
                        Disabled = 0
                    };
                    var anotherSubcategory = new Domain.Subcategory()
                    {
                        Name = "ghi",
                        Disabled = 1
                    };

                    oneCategory.Subcategories.Add(oneSubcategory);
                    oneCategory.Subcategories.Add(anotherSubcategory);

                    using (var transaction = dbSession.BeginTransaction())
                    {
                        dbSession.Save(oneCategory);
                        transaction.Commit();
                    }
                    dbSession.Close();
                }
                dbConnection.Close();
            }
            
        }
    }
}
