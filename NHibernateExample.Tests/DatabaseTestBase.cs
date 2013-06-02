using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace NHibernateExample.Tests
{
    public class DatabaseTestBase
    {
        public NHibernateConfigurator Configurator { get; private set; }

        public DatabaseTestBase()
        {
            Configurator = new NHibernateConfigurator("postgres_test");
        }

        protected void CleanUpDatabase()
        {
            CleanUpTable<Passport>();
            CleanUpTable<Person>();
        }

        private void CleanUpTable<T>()
        {
            CleanUpTable<T>(Configurator.SessionFactory);
        }

        private static void CleanUpTable<T>(ISessionFactory sessionFactory)
        {
            var metadata = sessionFactory.GetClassMetadata(typeof(T)) as NHibernate.Persister.Entity.AbstractEntityPersister;
            string table = metadata.TableName;

            using (ISession session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    string deleteAll = string.Format("DELETE FROM \"{0}\"", table);
                    session.CreateSQLQuery(deleteAll).ExecuteUpdate();

                    transaction.Commit();
                }
            }
        }

    }
}
