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
                    const string hqlInsert = "INSERT INTO Passport (Person, Number, Issued) SELECT per, :number, :issued FROM Person per WHERE SSN = :ssn";
                    session.CreateQuery(hqlInsert)
                        .SetInt32("number", passport.Number)
                        .SetDateTime("issued", passport.Issued)
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
