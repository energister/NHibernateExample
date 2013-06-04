using System.Collections.Generic;
using NHibernate;

namespace NHibernateExample
{
    public class PersonsStorage
    {
        private readonly ISessionFactory _factory;

        public PersonsStorage(ISessionFactory factory)
        {
            _factory = factory;
        }

        public void Save(PersonEntity person)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(person);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<PersonEntity> LoadAll()
        {
            using (ISession session = _factory.OpenSession())
            {
                IList<PersonEntity> results = session.QueryOver<PersonEntity>().List();
                return results;
            }
        }
    }
}
