using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using NHibernate;
using Xunit;

namespace NHibernateExample.Tests
{
    public class TestParentsStorage : DatabaseTestBase
    {
        private readonly ParentsStorage _storage;

        public TestParentsStorage()
        {
            CleanUpTable<Parent>();

            _storage = Configurator.ProduceParentStorage();
        }

        [Fact]
        public void SaveLoad()
        {
            var person = new Person() {Name = "Nick", SSN = 123456789};
            _storage.Save(person);

            var savedPerson = _storage.LoadAll().FirstOrDefault();
            savedPerson.Should().NotBeNull();

            savedPerson.Should().NotBeSameAs(person);
            savedPerson.Name.Should().Be(person.Name);
            savedPerson.SSN.Should().Be(person.SSN);
        }
    }
}
