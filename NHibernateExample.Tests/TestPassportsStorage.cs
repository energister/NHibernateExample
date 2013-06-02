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

            var savedPassport = _passportsStorage.LoadAll().FirstOrDefault();
            savedPassport.Should().NotBeNull();

            savedPassport.Should().NotBeSameAs(passport);
            savedPassport.Number.Should().Be(passport.Number);
            savedPassport.Issued.Should().BeWithin(1.Days()).Before(passport.Issued);

            var savedPerson = savedPassport.Person;
            savedPerson.Should().NotBeNull(); // NOTE!

            var owner = _passportsStorage.LoadPassportOwner(savedPassport);
            owner.Name.Should().Be(john.Name);
        }
    }
}
