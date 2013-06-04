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

        public void SaveFor(int ssn, Passport passport)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    string hql = string.Format("INSERT INTO {0} ({1}, {2}, {3}) " +
                                                "SELECT personWithSsn, :passportNumber, :passportIssued FROM {4} personWithSsn WHERE {5} = :ssn",
                        typeof(Passport).Name,
                        MemberName.Of(() => passport.Person),
                        MemberName.Of(() => passport.Number),
                        MemberName.Of(() => passport.Issued),
                        typeof (Person),
                        MemberName.Of((Person person) => person.SSN));
                    
                    session.CreateQuery(hql)
                        .SetInt32("passportNumber", passport.Number)
                        .SetDateTime("passportIssued", passport.Issued)
                        .SetInt32("ssn", ssn)
                        .ExecuteUpdate();
                    
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

        public IEnumerable<Passport> LoadAllWithRelations()
        {
            using (ISession session = _factory.OpenSession())
            {
                IList<Passport> results = session.QueryOver<Passport>().Fetch(p => p.Person).Eager.List();
                return results;
            }
        }
    }
}
