using System;
using System.Web;
using System.Data;
using System.Reflection;

using NHibernate;
using NHibernate.Cfg;

namespace Nightingale
{
    // code partly from http://nhibernate.info/doc/nh/en/index.html#quickstart-playingwithcats

    public sealed class NHibernateHelper
    {
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory sessionFactory;
        private static ISession currentSession = null;
        private static IDbConnection overriddenConnection = null;

        // TODO UNTESTED
        // TODO UNUSED
        static NHibernateHelper()
        {
            Configuration cfg = new Configuration();
            cfg.Configure();

            // Add all hbm.xml files in the assembly
            cfg.AddAssembly(typeof(Nightingale.NHibernateHelper).Assembly);

            sessionFactory = cfg.BuildSessionFactory();
        }

        // TODO UNTESTED
        // TODO UNUSED
        public static ISession GetCurrentSession()
        {
            if (currentSession == null || !currentSession.IsOpen)
            {
                currentSession = sessionFactory.OpenSession();
            }

            return currentSession;
        }

        // TODO UNTESTED
        // TODO UNUSED
        public static ISession GetCustomSession(System.Data.Common.DbConnection mahConnection)
        {
            // TODO DEPRECATED
            return sessionFactory.OpenSession(mahConnection);
        }

        // TODO UNTESTED
        // TODO UNUSED
        public static void CloseSession()
        {
            if (currentSession == null)
            {
                return;
            }

            currentSession.Close();
            currentSession = null;
        }

        // TODO UNTESTED
        // TODO UNUSED
        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }
    }
}