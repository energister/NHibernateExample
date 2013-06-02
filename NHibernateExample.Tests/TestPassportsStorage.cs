using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NHibernateExample.Tests
{
    public class TestPassportsStorage : DatabaseTestBase
    {
        private readonly PersonsStorage _personsStorage;
        private readonly PassportsStorage _passportsStorage;

        public TestPassportsStorage()
        {
            CleanUpDatabase();
            
            _personsStorage = Configurator.ProducePersonsStorage();
            _passportsStorage = Configurator.ProducePassportsStorage();
        }

        [Fact]
        public void SaveLoad()
        {
            var john = new Person {Name = "John", SSN = 123454321};
            _personsStorage.Save(john);
            
            var passport = new Passport {Person = john, Number = 98765, Issued = DateTime.Now};
            _passportsStorage.Save(passport);

            var loadedPassport = _passportsStorage.LoadAll().FirstOrDefault();
            loadedPassport.Should().NotBeNull();

            loadedPassport.Should().NotBeSameAs(passport);
            loadedPassport.Number.Should().Be(passport.Number);
            loadedPassport.Issued.Should().BeWithin(1.Days()).Before(passport.Issued);

            var loadedOwner = loadedPassport.Person;
            loadedOwner.Should().NotBeNull();
            loadedOwner.Should().NotBeSameAs(john);
            loadedOwner.Name.Should().Be(john.Name);
        }
    }
}
