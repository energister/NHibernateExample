using System;
using NHibernate.Mapping.Attributes;

namespace NHibernateExample
{
    [Class(Table = "passport")]
    public class Passport
    {
        [Id(Name = "Id")]
        [Generator(1, Class = "native")]
        public virtual long Id { get; set; }

        //[Property(NotNull = true)]
        [ManyToOne(NotNull = true, Column = "person_id")]
        public virtual Person Person { get; set; }

        [Property(NotNull = true)]
        public virtual int Number { get; set; }

        [Property(NotNull = true)]
        public virtual DateTime Issued { get; set; }
    }
}
