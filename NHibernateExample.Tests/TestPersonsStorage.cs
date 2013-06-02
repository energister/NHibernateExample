using System.Linq;
using FluentAssertions;
using Xunit;

namespace NHibernateExample.Tests
{
    public class TestPersonsStorage : DatabaseTestBase
    {
        private readonly PersonsStorage _storage;
        
        public TestPersonsStorage()
        {
            CleanUpDatabase();

            _storage = Configurator.ProducePersonsStorage();
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
