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

        public void Save(PassportEntity passport)
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

        public void SaveFor(int ssn, PassportEntity passport)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    string hql = string.Format("INSERT INTO {0} ({1}, {2}, {3}) " +
                                                "SELECT personWithSsn, :passportNumber, :passportIssued FROM {4} personWithSsn WHERE {5} = :ssn",
                        typeof(PassportEntity).Name,
                        MemberName.Of(() => passport.Person),
                        MemberName.Of(() => passport.Number),
                        MemberName.Of(() => passport.Issued),
                        typeof(PersonEntity).Name,
                        MemberName.Of((PersonEntity person) => person.SSN));
                    
                    session.CreateQuery(hql)
                        .SetInt32("passportNumber", passport.Number)
                        .SetDateTime("passportIssued", passport.Issued)
                        .SetInt32("ssn", ssn)
                        .ExecuteUpdate();
                    
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<PassportEntity> LoadAll()
        {
            using (ISession session = _factory.OpenSession())
            {
                IList<PassportEntity> results = session.QueryOver<PassportEntity>().List();
                return results;
            }
        }

        public IEnumerable<PassportEntity> LoadAllWithRelations()
        {
            using (ISession session = _factory.OpenSession())
            {
                IList<PassportEntity> results = session.QueryOver<PassportEntity>().Fetch(p => p.Person).Eager.List();
                return results;
            }
        }
    }
}
