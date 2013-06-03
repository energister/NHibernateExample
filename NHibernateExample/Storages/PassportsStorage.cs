using System.Collections.Generic;
using NHibernate;
using NHibernateExample.Entities;

namespace NHibernateExample.Storages
{
    public class PassportsStorage
    {
        private readonly ISessionFactory _factory;

        public PassportsStorage(ISessionFactory factory)
        {
            _factory = factory;
        }

        public void Save(Passport passport)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(passport);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<Passport> LoadAll()
        {
            using (ISession session = _factory.OpenSession())
            {
                IList<Passport> results = session.QueryOver<Passport>().List();
                return results;
            }
        }
    }
}
