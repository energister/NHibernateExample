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

        public void Save(Person person)
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

        public IEnumerable<Person> LoadAll()
        {
            using (ISession session = _factory.OpenSession())
            {
                IList<Person> results = session.QueryOver<Person>().List();
                return results;
            }
        }
    }
}
