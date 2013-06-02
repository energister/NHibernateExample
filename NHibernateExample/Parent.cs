using System;
using NHibernate.Mapping.Attributes;

namespace NHibernateExample
{
    [Class(Table = "people")]
    public class Parent
    {
        [Id(Name = "Id")]
        [Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        [Property(NotNull = true, Column = "PersonId")]
        [OneToOne]
        public virtual Person Person { get; set; }

        [Property(NotNull = true)]
        public virtual DateTime FirstChildBirthdate { get; set; }
    }
}
