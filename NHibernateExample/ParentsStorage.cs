using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace NHibernateExample
{
    public class ParentsStorage
    {
        private readonly ISessionFactory _factory;

        public ParentsStorage(ISessionFactory factory)
        {
            _factory = factory;
        }

        public void Save(Parent parent)
        {
            using (var session = _factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(parent);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<Parent> LoadAll()
        {
            using (ISession session = _factory.OpenSession())
            {
                IList<Parent> results = session.QueryOver<Parent>().List();
                return results;
            }
        }
    }
}
