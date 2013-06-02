using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using NHibernate;
using Xunit;

namespace NHibernateExample.Tests
{
    public class TestPersonStorage
    {
        private readonly PersonStorage storage;
        
        public TestPersonStorage()
        {
            var configurator = new NHibernateConfigurator("postgres_test");
            
            CleanUpTable<Person>(configurator.SessionFactory);

            storage = configurator.ProducePersonStorage();
        }

        [Fact]
        public void SaveLoad()
        {
            var person = new Person() {Name = "Nick", SSN = 123456789};
            storage.Save(person);

            var savedPerson = storage.LoadAll().FirstOrDefault();
            savedPerson.Should().NotBeNull();

            savedPerson.Should().NotBeSameAs(person);
            savedPerson.Name.Should().Be(person.Name);
            savedPerson.SSN.Should().Be(person.SSN);
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
