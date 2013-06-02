using System.Collections.Generic;
using NHibernate;

namespace NHibernateExample
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

        public Person LoadPassportOwner(Passport passport)
        {
            using (ISession session = _factory.OpenSession())
            {
                var person = session.Get<Passport>(passport.Id).Person;
                return session.Get<Person>(person.Id);
            }
        }
    }
}
