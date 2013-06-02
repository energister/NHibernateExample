using NHibernate.Mapping.Attributes;

namespace NHibernateExample
{
    [Class(Table = "people")]
    public class Person
    {
        [Id(Name = "Id")]
        [Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [Property(NotNull = true)]
        public virtual string Name { get; set; }

        [Property(NotNull = true)]
        public virtual int SSN { get; set; }
    }
}
